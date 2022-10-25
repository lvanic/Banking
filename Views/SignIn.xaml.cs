using Banking.Controllers;
using Banking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Banking.Views
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : UserControl
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            
            if (RegisterName.Text == "Support")
            {
                using (SupportController controller = new SupportController())
                {
                  
                    WindowSupport.support = controller.AuthSupport(RegisterPassword.Password);
                    SignUp.SetSuccsedS(sender, e);
                }
            }
            else
            {
                using (BankController controller = new BankController())
                {
                    ClientModel client = controller.GetClient(RegisterName.Text, RegisterPassword.Password);
                    if (client != null)
                    {
                        MainWindow.CurrentClient = client;
                        SignUp.SetSuccsed(sender, e);
                    }

                }
            }
        }
    }
}
