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
using CountMeUpScottyLibrary;

namespace CountMeUpScottyGUI
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        public GameScreen(string nickname)
        {
            InitializeComponent();
            CreatePlayer(nickname);
        }

        private void CreatePlayer(string nickname)
        {
            player = new Player(nickname);
            welcome.Text = $"Welcome {nickname}. Hit Start to begin.";
        }

        private void startgame_Click(object sender, RoutedEventArgs e)
        {
            game = new Game(player);
            startgame.Visibility = Visibility.Hidden;
            progressLabel.Text = $"You solved {game.GetCurrentChallengeNumber()} of {game.NumberOfChallenges()} challenges";
            progress.Value = 0;
        }

        // Attribute
        private Player player = null;
        private Game game = null;
    }
}
