using Banking.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Banking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Theme Theme { get => _theme; }
        public static Language Language { get => _language; }

        private static Theme _theme;
        private static Language _language;

        public App()
        {
            InitializeComponent();
            InitializeApp();
        }

        private static void InitializeApp()
        {
            _theme = new Theme();
            _language = new Language();
        }
    }
}
