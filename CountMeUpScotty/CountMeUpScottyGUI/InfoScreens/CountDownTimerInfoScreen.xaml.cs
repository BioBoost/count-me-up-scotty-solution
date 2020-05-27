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

namespace CountMeUpScottyGUI.InfoScreens
{
    /// <summary>
    /// Interaction logic for CountDownTimerInfoScreen.xaml
    /// </summary>
    public partial class CountDownTimerInfoScreen : Window
    {
        public CountDownTimerInfoScreen()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PreviousInfoScreen_Click(object sender, RoutedEventArgs e)
        {
            SolveChallengeInfoScreen screen = new SolveChallengeInfoScreen();
            screen.Show();
            this.Close();
        }

        private void NextInfoScreen_Click(object sender, RoutedEventArgs e)
        {
            DifficultyInfoScreen screen = new DifficultyInfoScreen();
            screen.Show();
            this.Close();
        }
    }
}
