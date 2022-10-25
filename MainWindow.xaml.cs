using Banking.Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Banking.Views;
using System.ComponentModel;
using System.Threading;
using Banking.Models;
using Banking.Helpers;
using System.Windows.Threading;

namespace Banking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BankController controller;
        public static ClientModel CurrentClient;
        public MainWindow()
        {
            using ApplicationContext db = new ApplicationContext();
            InitializeComponent();
            SafeGround.Visibility = Visibility.Visible;
            controller = new BankController();
            SignUp sign = controller.GetWindowRegister();
            sign.Show();

            SignUp.Succsed += (object o, EventArgs e) => 
            { 
                SafeGround.Visibility = Visibility.Hidden;
                WindowDisplay.Children.Add(new MainControl());
            };
            SignUp.SuccsedS += (object o, EventArgs e) =>
            {
                this.Close();
            };
             this.Closed += (object o, EventArgs e) => { if (sign.IsActive) sign.Close(); };
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var el = sender as MenuItem;
            switch (el.Tag.ToString())
            {
                case "0":
                    WindowDisplay.Children.Clear();
                    WindowDisplay.Children.Add(new MainControl());
                    break;

                case "1":
                    WindowDisplay.Children.Clear();
                    WindowDisplay.Children.Add(new News());
                    break;

                case "2":
                    WindowDisplay.Children.Clear();
                    List<Message> mess = new List<Message>();
                    mess = controller.GetMessages(CurrentClient.Id);
                    WindowDisplay.Children.Add(new MessageView(mess));
                    WindowDisplay.Children.Add(new ChatView());
                    break;

                case "3":
                    WindowDisplay.Children.Clear();
                    WindowDisplay.Children.Add(new TransferView());
                    break;
                case "4":
                    WindowDisplay.Children.Clear();
                    WindowDisplay.Children.Add(new Options());
                    break;
            }
        }
    }
}
