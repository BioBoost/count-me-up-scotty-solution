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
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        private bool isGameInProgress = false;
        private Player player = null;
        private Game game = null;
        private SumChallenge currentChallenge = null;

        public GameScreen(string nickname)
        {
            InitializeComponent();
            player = new Player(nickname);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            SolveChallengeInfoScreen screen = new SolveChallengeInfoScreen();
            screen.Show();
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (!isGameInProgress)
            {
                StartNewGame();
            }
            else
            {
                warning.Text = $"{player.Name} you stopped the game prematurely. So no score for you. Sorry.";
                StopCurrentGame();
            }
        }

        private void StartNewGame()
        {
            Console.WriteLine("Starting new game");
            game = new Game(player);
            isGameInProgress = true;
            UpdateStaticGUIControls();
            progressChallenges.Maximum = game.NumberOfChallenges();
            PrepareChallenge();
        }

        private void StopCurrentGame()
        {
            Console.WriteLine("Stopping current game");
            isGameInProgress = false;
            UpdateStaticGUIControls();
        }

        private void UpdateStaticGUIControls()
        {
            if (isGameInProgress)
            {
                userSolution.IsEnabled = true;
                StartStop.Content = "Stop Game";
                info.Visibility = Visibility.Hidden;
                warning.Visibility = Visibility.Hidden;
            }
            else
            {
                userSolution.IsEnabled = false;
                StartStop.Content = "Start Game";
                info.Visibility = Visibility.Visible;
            }
        }

        private void PrepareChallenge()
        {
            if (!game.IsFinished())
            {
                Console.WriteLine("Preparing challenge");
                progressChallenges.Value = game.GetCurrentChallengeNumber();
                currentChallenge = game.NextChallenge();
                leftValue.Text = currentChallenge.LeftValue().ToString();
                rightValue.Text = currentChallenge.RightValue().ToString();
                userSolution.Text = "";
                userSolution.Focus();
            }
            else
            {
                info.Text = $"{player.Name} you have finished the game with a score of {game.Score()}";
                StopCurrentGame();
            }
        }

        private void ProcessUserInput()
        {
            try
            {
                int solution = Convert.ToInt32(userSolution.Text);
                currentChallenge.Solve(solution);
                PrepareChallenge();
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Please input numbers", "Needs to be replaced by something better");
            }
        }

        private void userSolution_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && userSolution.Text != "")
            {
                ProcessUserInput();
            }
        }
    }
}
