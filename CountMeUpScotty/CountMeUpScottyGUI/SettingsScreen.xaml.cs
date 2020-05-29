using CountMeUpScottyLibrary;
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

namespace CountMeUpScottyGUI
{
    /// <summary>
    /// Interaction logic for SettingsScreen.xaml
    /// </summary>
    public partial class SettingsScreen : Window
    {
        private Settings settings = new Settings();

        public SettingsScreen()
        {
            InitializeComponent();

            // Enums as source for combobox
            // https://stackoverflow.com/questions/6145888/how-to-bind-an-enum-to-a-combobox-control-in-wpf
            difficultyCombobox.ItemsSource = Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();

            // Data Binding
            settings.Load();
            this.DataContext = settings;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            settings.Save();
            this.Close();
        }

    }
}
