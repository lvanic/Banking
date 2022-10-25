using Banking.Controllers;
using Banking.Helpers;
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
using System.Windows.Shapes;

namespace Banking.Views
{
    /// <summary>
    /// Логика взаимодействия для WindowSupport.xaml
    /// </summary>
    public partial class WindowSupport : Window
    {
        public static SupportModel support;
        public WindowSupport()
        {
            InitializeComponent();
            CurId.Content = "Id " + support.Id.ToString();
            List<Request> re;
            using (SupportController controller = new SupportController())
            {
                re = controller.GetRequestsBill(support);
            }
            WindowDisplay.Children.Add(new Requests(re));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var el = sender as MenuItem;
            switch (el.Tag.ToString())
            {
                case "0":
                    WindowDisplay.Children.Clear();
                    List<Request> re ;
                    using (SupportController controller = new SupportController())
                    {
                        re = controller.GetRequestsBill(support);
                    }
                    WindowDisplay.Children.Add(new Requests(re));
                    break;

                case "1":
                    WindowDisplay.Children.Clear();
                    List<Message> messages = new List<Message>();
                    using(SupportController controller = new SupportController())
                    {
                        messages = controller.GetMessages(support);
                    }
                    
                    WindowDisplay.Children.Add(new MessageView(messages));
                    WindowDisplay.Children.Add(new ChatView());
                    break;
            }
        }
    }
}
