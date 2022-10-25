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
    /// Логика взаимодействия для ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
        }
        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if (MainWindow.CurrentClient != null)
            {
                using (BankController controller = new BankController())
                {
                    controller.SendMessageSupport(MainWindow.CurrentClient.Id, MessageText.Text);
                }
            }
            else
            {
                using(SupportController controller = new SupportController())
                {
                    
                    controller.SendMessageToClient(WindowSupport.support, MessageView.SelectedMessage ,MessageText.Text);
                }
            }
        }
    }
}
