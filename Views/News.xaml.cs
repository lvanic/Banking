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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Banking.Views
{
    /// <summary>
    /// Логика взаимодействия для News.xaml
    /// </summary>
    public partial class News : UserControl
    {
        public News()
        {
            InitializeComponent();
            using(BankController controller = new BankController())
            {
                List<Excange> list = controller.GetExchanges();
                USDBlock.Text = list[0].value.ToString();
                EURBlock.Text = list[1].value.ToString();
                GBPBlock.Text = list[2].value.ToString();
            }
        }
    }
}
