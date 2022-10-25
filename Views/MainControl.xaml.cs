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

namespace Banking.Views
{
    /// <summary>
    /// Логика взаимодействия для MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
            using(BankController controller = new BankController())
            {
                AmountBlock.Text = controller.GetMoney(MainWindow.CurrentClient).ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(BankController controller = new BankController())
            {
                controller.RequestForBill(MainWindow.CurrentClient);
            }
        }
    }
}
