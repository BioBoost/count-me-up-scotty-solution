using CountMeUpScottyGUI.InfoScreens;
using CountMeUpScottyLibrary;
using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CountMeUpScottyGUI
{
    public partial class GameScreen : Window
    {
        private bool isGameInProgress = false;
        private Player player = null;
        private Game game = null;
        private DispatcherTimer countDownTimer = null;
        private int countDownValue = 0;
        private static int TIME_FOR_SOLVING_CHALLENGE = 12;
        private bool playSound = true;
        private Settings settings = new Settings();

        private SoundPlayer wrongSound = new SoundPlayer(Properties.Resources.wrong);
        private SoundPlayer correctSound = new SoundPlayer(Properties.Resources.correct);

        public GameScreen(string nickname)
        {
            InitializeComponent();
            settings.Load();
            playSound = settings.PlaySounds;
            player = PlayerManager.GetPlayer(nickname);
            CreateCountDownTimer();
            countDownProgress.Maximum = TIME_FOR_SOLVING_CHALLENGE;
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
            game = new Game(player, settings.Difficulty, settings.NumberOfChallenges);
            isGameInProgress = true;
            UpdateStaticGUIControls();
            progressChallenges.Maximum = game.NumberOfChallenges();
            progressChallenges.Value = 0;
            PrepareNextChallenge();
            UpdateCountDownGUI();
            countDownTimer.Start();
        }

        private void CreateCountDownTimer()
        {
            countDownTimer = new DispatcherTimer();
            countDownTimer.Interval = TimeSpan.FromSeconds(1);
            countDownTimer.Tick += CountDownTimer_Tick;
        }

        private void CountDownTimer_Tick(object sender, EventArgs e)
        {
            countDownValue--;
            if (countDownValue <= 0)
            {
                PrepareNextChallenge();
            }
            UpdateCountDownGUI();
        }

        private void StopCurrentGame()
        {
            Console.WriteLine("Stopping current game");
            countDownTimer.Stop();
            isGameInProgress = false;
            UpdateStaticGUIControls();
            game.Finish();
        }

        private void ShowOverviewScreen()
        {
            OverviewScreen overview = new OverviewScreen(game.GetChallenges(), game.GetCurrentScore());
            overview.Show();
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

        private void UpdateCountDownGUI()
        {
            countDownProgress.Value = countDownValue;
            countDownLabel.Text = countDownValue.ToString();
        }

        private void PrepareNextChallenge()
        {
            progressChallenges.Value = game.GetCurrentChallengeNumber();
            if (!game.IsFinished())
            {
                Console.WriteLine("Preparing challenge");
                countDownValue = TIME_FOR_SOLVING_CHALLENGE;
                SumChallenge currentChallenge = game.NextChallenge();
                leftValue.Text = currentChallenge.LeftOperand().ToString();
                rightValue.Text = currentChallenge.RightOperand().ToString();
                userSolution.Text = "";
                userSolution.Focus();
            }
            else
            {
                StopCurrentGame();
                ShowOverviewScreen();
            }
        }

        private void ProcessUserInput()
        {
            try
            {
                int solution = Convert.ToInt32(userSolution.Text);
                if (game.SolveCurrentChallenge(solution) && playSound)
                {
                    correctSound.Play();
                }
                else if (playSound)
                {
                    wrongSound.Play();
                }
                PrepareNextChallenge();
                warning.Visibility = Visibility.Hidden;
            }
            catch (FormatException fe)
            {
                warning.Text = "Please only input valid numbers.";
                warning.Visibility = Visibility.Visible;
                userSolution.Text = "";
                userSolution.Focus();
            }
        }

        private void userSolution_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && userSolution.Text != "")
            {
                ProcessUserInput();
            }
        }

        private void mainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isGameInProgress)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you wish to leave? This will forfeit the current game?", "Count Me Up Scotty", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                StopCurrentGame();
            }
            OpenMainMenu();
        }

        private void OpenMainMenu()
        {
            MainMenuScreen screen = new MainMenuScreen(player.Name);
            screen.Show();
        }
    }
}
