using Banking.Helpers;
using Banking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Controllers
{
    class SupportController : IDisposable
    {
        public int CreateNewBill(ClientModel client)
        {
            try
            {
                using ApplicationContext db = new ApplicationContext();
                db.Clients.Where(x => x.Id == client.Id).FirstOrDefault().Bills.Add(new BillModel() {Amount = 5 });
                db.Requests.Remove(db.Requests.Where(x => x.From.Id == client.Id).OrderBy(x => x.Id).Last());
                db.SaveChanges();
                return 200;
            }
            catch
            {
                return 400;
            }
        }
        public int SendMessageToClient(SupportModel support, int idMes, string text)
        {
            try
            {
                using ApplicationContext db = new ApplicationContext();
                
                Message mes = db.Messages.Include(c => c.Client).FirstOrDefault(x => x.Id == idMes);
                mes.IsActive = false;
                Message message = new Message()
                {
                    Support = db.Supports.Where(x => x.Id == support.Id).FirstOrDefault(),
                    Client = db.Clients.Where(x => x.Id == mes.Client.Id).FirstOrDefault(),
                    Text = text,
                    DateTime = DateTime.Now
                };
                db.Messages.Add(message);
                db.SaveChanges();
                
                return 200;
            }
            catch
            {
                return 400;
            }
            
        }
        public List<Request> GetRequestsBill(SupportModel support)
        {
            List<Request> list = new List<Request>();
            try
            {
                using ApplicationContext db = new ApplicationContext();
                list = db.Requests.Include(x => x.To).Include(x => x.From).Where(x => x.To.Id == support.Id).ToList();
            }
            catch
            {
                
            }
            return list;
        }
        public int CreateSupport(string pas, string pas2)
        {
            if(pas != pas2)
            {
                return 402;
            }
            using(ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    if (db.Supports.Where(x => x.Password == pas).Count() > 0)
                    {
                        return 401;
                    }
                    SupportModel support = new SupportModel()
                    {
                        Password = pas
                    };
                    db.Supports.Add(support);
                }
                catch
                {
                    SupportModel support = new SupportModel()
                    {
                        Password = pas
                    };
                    db.Supports.Add(support);
                    
                }
                db.SaveChanges();
            }
            
            return 200;
        }
        public SupportModel AuthSupport(string pas)
        {
            SupportModel support = null;
            try
            {
                
                using (ApplicationContext db = new ApplicationContext())
                {
                    support = db.Supports.Where(x => x.Password == pas).First();
                }
            }
            catch
            {
                return null;
            }
            return support;
        }
        public List<Message> GetMessages(SupportModel support)
        {
            List<Message> messages = new List<Message>();
            using(ApplicationContext db = new ApplicationContext())
            {
                messages = db.Messages.Include(x => x.Client).Include(x => x.Support).Where(x => x.Support.Id == support.Id && x.IsActive == true).ToList();   
            }
            
            return messages;
        }
        public void Dispose()
        {
            
        }
    }
}
