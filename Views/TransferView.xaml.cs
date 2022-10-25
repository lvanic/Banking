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
    /// Логика взаимодействия для TransferView.xaml
    /// </summary>
    public partial class TransferView : UserControl
    {
        public TransferView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using BankController controller = new BankController();
            int bill;
            decimal amount;
            Int32.TryParse(Bill.Text, out bill);
            Decimal.TryParse(MoneyAmount.Text, out amount);
            controller.SendMoney(MainWindow.CurrentClient, bill, amount);
        }
    }
}
