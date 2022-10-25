using Banking.Models;
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
using System.Windows.Threading;

namespace Banking.Views
{
    /// <summary>
    /// Логика взаимодействия для MessageView.xaml
    /// </summary>
    public partial class MessageView : UserControl
    {
        public static int SelectedMessage = -1;
        private List<Message> messages;
        public MessageView()
        {
            InitializeComponent();
        }
        public MessageView(List<Message> mess)
        {
            InitializeComponent();
            messages = mess;
            partList.ItemsSource = mess;
        }
        private void SetActiveIndex(object sender, EventArgs e)
        {
            var es = (Message)partList.SelectedItem;
            SelectedMessage = es.Id;
        }
    }
}
