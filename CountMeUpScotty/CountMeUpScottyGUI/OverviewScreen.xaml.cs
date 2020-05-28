using CountMeUpScottyLibrary;
using System.Collections.Generic;
using System.Windows;

namespace CountMeUpScottyGUI
{
    public partial class OverviewScreen : Window
    {
        public OverviewScreen(List<SumChallenge> challenges)
        {
            InitializeComponent();
            datagridOverview.ItemsSource = challenges;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
