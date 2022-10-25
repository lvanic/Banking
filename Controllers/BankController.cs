using Banking.Helpers;
using Banking.Models;
using Banking.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Banking.Controllers
{
    public class BankController : IDisposable//TODO: fill methods
    {
        public static ClientModel client;
        public async Task<int> RegisterNewAccountAsync(string name, string password1, string password2)
        {
            int result = await Task.Run(() => RegisterNewAccount(name, password1, password2));
            return result;
        }
        public int RegisterNewAccount(string name, string password1, string password2)
        {
            if (password1 != password2)
                return 400;
            using (ApplicationContext db = new ApplicationContext())
            {

                if (db.Clients.Where(x => x.Name == name).Count() > 0)
                    return 403;
                else
                {
                    db.Clients.Add(new ClientModel() { Name = name, Password = password1, isAccepted = true });
                    db.SaveChanges();
                }
               
                return 200;
            }
        }
        public List<Message> GetMessages(int id)
        {
            List<Message> result = null;
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Message> messages = db.Messages.Where(x => x.Client.Id == id).ToList();
                ClientModel c = db.Clients.Where(x => x.Id == id).First();
                result = db.Messages.Include(x => x.Support).Where(x => x.Client.Id == id).ToList();     
            }
            return result;
        }

        public int SendMessageSupport(int idFrom, string text)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    
                    Random rnd = new Random();
                    int idTo = rnd.Next() % db.Supports.Count() + 1;
                    Message message = new Message()
                    {
                        DateTime = DateTime.Now,
                        Client = db.Clients.Where(x => x.Id == idFrom).First(),
                        Support = db.Supports.Where(x => x.Id == idTo).First(),
                        Text = text,
                        IsActive = true
                    };
                    db.Messages.Add(message);
                    
                    db.SaveChanges();
                    List<Message> support = db.Messages.Where(x => x.Client.Id == idFrom).ToList();
                }
                return 200;
            }
            catch
            {
                return 400;
            }
        }
        public decimal GetMoney(ClientModel client)
        {
            if (client == null)
            {
                return 0;
            }
            try
            {
                using ApplicationContext db = new ApplicationContext();
                return db.Clients.Include(x => x.Bills).Where(x => x.Id == client.Id).FirstOrDefault().Bills.Sum(x => x.Amount);
            }
            catch
            {
                return 0;
            }
        }
        public bool RequestForBill(ClientModel client)
        {
            
            using(ApplicationContext db = new ApplicationContext())
            {
                Random rnd = new Random();
                int idTo = rnd.Next() % db.Supports.Count() + 1;
                Request request = new Request()
                {
                    From = db.Clients.Where(x => x.Id == client.Id).FirstOrDefault(),
                    To = db.Supports.Where(x => x.Id == idTo).First(),
                };
                db.Requests.Add(request);
                db.SaveChanges();
            }
            return true;
        }
        public int SendMoney(ClientModel client, int idBill, decimal amount)
        {
            try
            {
                using ApplicationContext db = new ApplicationContext();
                decimal amountC = (db.Clients.Include(x => x.Bills).Where(x => x.Id == client.Id).FirstOrDefault().Bills.OrderByDescending(x => x.Amount).FirstOrDefault().Amount -= amount);
                if (amountC < 0)
                    return 400;

                db.Bills.Where(x => x.Number == idBill).FirstOrDefault().Amount += amount;
                db.SaveChanges();
                return 200;
            }
            catch
            {
                return 400;
            }
        }
        public ClientModel GetClient(string name, string password)
        {
            try
            {
                if (client is null)
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        client = db.Clients.Where(x => x.Name == name && x.Password == password).First();
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return client;
        }

        public int AcceptLoan(int idTo, int idFrom)
        {
            try
            {
                if (idTo != client.Id)
                    throw new Exception();

                using(ApplicationContext db = new ApplicationContext())
                {
                    Loan loan = db.Loans.Where(x => x.To.Id == idTo && x.From.Id == idFrom).First();
                    loan.IsAccepted = true;
                    loan.DateTimefrom = DateTime.Now;
                }
                return 200;
            }
            catch
            {
                return 400;
            }
        }
        public int SendLoan(int idFrom, int idTo, decimal amount, float percent, DateTime date)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan dif = date.Subtract(dateTime);
                decimal endAmount = amount * Convert.ToDecimal(Math.Pow((1 + percent), (dif.Days / 30)));
                using (ApplicationContext db = new ApplicationContext())
                {
                    ClientModel to = db.Clients.Where(x => x.Id == idTo).First();
                    ClientModel from = db.Clients.Where(x => x.Id == idFrom).First();
                    Loan loan = new Loan()
                    {
                        From = from,
                        Amount = amount,
                        DateTimefrom = dateTime,
                        DateTimeto = date,
                        To = to,
                        Percent = percent,
                        IsAccepted = false
                    };
                    
                    db.Loans.Add(loan);
                }
                return 200;
            }
            catch
            {
                return 400;
            }
        }

        public SignUp GetWindowRegister()
        {
            return new SignUp();
        }

        public SignIn GetWindowAuth()
        {
            return new SignIn();
        }
        public List<Excange> GetExchanges()
        {
            List<Excange> list = new List<Excange>();
            try
            {
                string BynToUsd = null, BynToEur = null, BynToGbp = null;
                

                WebRequest request = WebRequest.Create("https://currate.ru/api/?get=rates&pairs=USDBYN,EURBYN,GBPBYN&key=de20ede706dd6edf80dd2f2238f81993");
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            int index1 = line.IndexOf("USDBYN") + 9;
                            int index2 = line.IndexOf("EURBYN") + 9;
                            int index3 = line.IndexOf("GBPBYN") + 9;
                            for (int i = index1; i < index1 + 7; i++)
                            {
                                BynToUsd += line[i];
                            }
                            for (int i = index2; i < index2 + 7; i++)
                            {
                                BynToEur += line[i];
                            }
                            for (int i = index3; i < index3 + 7; i++)
                            {
                                BynToGbp += line[i];
                            }
                        }
                    }
                }

                list.Add(new Excange()
                {
                    nameFirst = "USD",
                    nameSecond = "BYN",
                    value = BynToUsd
                });

                list.Add(new Excange()
                {
                    nameFirst = "EUR",
                    nameSecond = "BYN",
                    value = BynToEur
                });

                list.Add(new Excange()
                {
                    nameFirst = "GBP",
                    nameSecond = "BYN",
                    value = BynToGbp
                });
            }
            catch 
            {

            }

            return list;
        }

        public void Dispose()
        {
        }
    }
}
