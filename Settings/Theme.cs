using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Banking.Settings
{
    public class Theme
    {
        private List<string> themes = new List<string>();
        public List<string> Themes
        {
            get => themes;
        }
        public Theme()
        {
            ThemeChanged += AppThemeChanged;

            themes.Add("Default");
            themes.Add("Dark");
            //themes.Add("Yellow");

            Name = Properties.Settings.Default.DefaultTheme;
        }

        public event EventHandler ThemeChanged;

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == Name) return;

                name = value;

                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                { Source = new Uri(String.Format($"Themes/{value}.xaml"), UriKind.Relative) });
                ThemeChanged(Application.Current, new EventArgs());
            }
        }

        private void AppThemeChanged(Object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultTheme = Name;
            Properties.Settings.Default.Save();
        }
    }
}
