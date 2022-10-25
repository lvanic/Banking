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
    /// Логика взаимодействия для Requests.xaml
    /// </summary>
    public partial class Requests : UserControl
    {
        private List<Request> requests = null;
        public Requests()
        {
            InitializeComponent();
        }
        public Requests(List<Request> r)
        {
            InitializeComponent();
            requests = r;
            
            partList.ItemsSource = requests;
        }
        private void AcceptBill(object sender, EventArgs e)
        {
            using SupportController controller = new SupportController();
            var el = (Request)partList.SelectedItem;
            controller.CreateNewBill(el.From);
        }
    }
}
