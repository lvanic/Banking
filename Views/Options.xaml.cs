using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для Options.xaml
    /// </summary>
    public partial class Options : UserControl
    {
        public Options()
        {
            InitializeComponent();
            App.Theme.ThemeChanged += ThemeChanged;
            App.Language.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language.Name;
            string currTheme = App.Theme.Name;

            foreach (var lang in App.Language.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }

            foreach (var theme in App.Theme.Themes)
            {
                MenuItem menuThem = new MenuItem();
                menuThem.Header = theme;
                menuThem.Tag = theme;
                menuThem.IsChecked = theme == currTheme;
                menuThem.Click += ChangeThemeClick;
                menuTheme.Items.Add(menuThem);
            }
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language.Name;

            foreach (MenuItem menuItem in menuLanguage.Items)
            {
                CultureInfo cultureInfo = menuItem.Tag as CultureInfo;
                menuItem.IsChecked = cultureInfo?.Equals(currLang) ?? false;
            }
        }
        private void ThemeChanged(Object sender, EventArgs e)
        {
            string currTheme = App.Theme.Name;

            foreach (MenuItem menuItem in menuTheme.Items)
            {
                string theme = menuItem.Tag as string;
                menuItem.IsChecked = theme?.Equals(currTheme) ?? false;
            }
        }
        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            CultureInfo lang = menuItem.Tag as CultureInfo;
            App.Language.Name = lang;
        }
        private void ChangeThemeClick(Object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string theme = menuItem.Tag as string;
            App.Theme.Name = theme;

        }
    }
}
