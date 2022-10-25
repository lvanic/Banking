using Banking.Controllers;
using Banking.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Banking.Views
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public static event EventHandler Succsed;
        public static event EventHandler SuccsedS;
        BankController controller;
        public SignUp()
        {
            Succsed += (object o, EventArgs e) => this.Close();
            SuccsedS += (object o, EventArgs e) => this.Close();
            SuccsedS += (object o, EventArgs e) => new WindowSupport().Show();
            InitializeComponent();
        }
        public static void SetSuccsed(object sender, RoutedEventArgs e)
        {
            Succsed?.Invoke(sender, e);
        }
        public static void SetSuccsedS(object sender, RoutedEventArgs e)
        {
            SuccsedS?.Invoke(sender, e);
        }
        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            using(controller = new BankController())
            {
                if(SecretCode.Text == "12345")
                {
                    using(SupportController controller = new SupportController())
                    {
                        int request = controller.CreateSupport(RegisterPassword.Password, RegisterPasswordSecond.Password);
                        if (request != 200)
                        {
                            RegisterErrors.Text = Errors.errors.GetValueOrDefault(request);
                        }
                        else
                        {
                            WindowSupport.support = controller.AuthSupport(RegisterPassword.Password);
                            MessageBox.Show(Errors.errors.GetValueOrDefault(request));
                            SuccsedS?.Invoke(sender, e);
                        }
                    }
                }
                else if(SecretCode.Text != "")
                {
                    RegisterErrors.Text = "Incorrect secret code";
                }
                else
                {
                    int request = controller.RegisterNewAccount(RegisterName.Text, RegisterPassword.Password, RegisterPasswordSecond.Password);
                    if (request != 200)
                    {
                        RegisterErrors.Text = Errors.errors.GetValueOrDefault(request);
                    }
                    else
                    {
                        MainWindow.CurrentClient = controller.GetClient(RegisterName.Text, RegisterPassword.Password);
                        MessageBox.Show(Errors.errors.GetValueOrDefault(request));
                        Succsed?.Invoke(sender, e);
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (controller = new BankController())
            {
                EnterWindow.Children.Clear();
                EnterWindow.Children.Add(controller.GetWindowAuth());
            }
        }
    }
}
