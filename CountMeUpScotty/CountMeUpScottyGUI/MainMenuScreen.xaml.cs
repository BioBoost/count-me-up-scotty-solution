using CountMeUpScottyGUI.InfoScreens;
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
    /// Interaction logic for MainMenuScreen.xaml
    /// </summary>
    public partial class MainMenuScreen : Window
    {
        private string nickname = "";

        public MainMenuScreen(string nickname)
        {
            InitializeComponent();
            this.nickname = nickname;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            GameScreen screen = new GameScreen(nickname);
            screen.Show();
            this.Close();
        }

        private void scoreboard_Click(object sender, RoutedEventArgs e)
        {
            ScoreboardScreen screen = new ScoreboardScreen();
            screen.Show();
            this.Close();
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            SolveChallengeInfoScreen screen = new SolveChallengeInfoScreen();
            screen.Show();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
