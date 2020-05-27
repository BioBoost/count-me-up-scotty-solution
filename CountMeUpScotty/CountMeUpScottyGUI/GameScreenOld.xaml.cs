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
    /// Interaction logic for GameScreenOld.xaml
    /// </summary>
    public partial class GameScreenOld : Window
    {
        public GameScreenOld(string nickname)
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
            Console.WriteLine("Creating new game");
            game = new Game(player);
            startgame.Visibility = Visibility.Hidden;
            progressLabel.Text = $"You solved {game.GetCurrentChallengeNumber()} of {game.NumberOfChallenges()} challenges";
            progress.Maximum = game.NumberOfChallenges();
            progress.Value = 0;
            PrepareNextChallenge();
        }

        private void PrepareNextChallenge()
        {
            Console.WriteLine("Preparing next challenge");
            currentChallenge = game.NextChallenge();
            Console.WriteLine(currentChallenge);
            leftValue.Text = currentChallenge.LeftValue().ToString();
            rightValue.Text = currentChallenge.RightValue().ToString();
            solution.Text = "";
            solution.Focus();
        }

        private void solution_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && solution.Text != "")
            {
                int userSolution = Convert.ToInt32(solution.Text);
                currentChallenge.Solve(userSolution);
                progress.Value++;
                if (!game.IsFinished())
                {
                    PrepareNextChallenge();
                }
                else
                {
                    MessageBox.Show($"You finished the game!", "Count Me Up Scotty");
                    solution.IsEnabled = false;
                    progressLabel.Text = $"Your score is {game.Score()}";
                }
            }
        }

        // Attribute
        private Player player = null;
        private Game game = null;
        private SumChallenge currentChallenge = null;
    }
}
