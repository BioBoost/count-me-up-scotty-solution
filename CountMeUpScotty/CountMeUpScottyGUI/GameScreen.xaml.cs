﻿using CountMeUpScottyGUI.InfoScreens;
using CountMeUpScottyLibrary;
using System;
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

        public GameScreen(string nickname)
        {
            InitializeComponent();
            player = new Player(nickname);
            CreateCountDownTimer();
            countDownProgress.Maximum = TIME_FOR_SOLVING_CHALLENGE;
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
            game = new Game(player, 3);             // Change number of challenges
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
            OverviewScreen overview = new OverviewScreen(game.GetChallenges());
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
                info.Text = $"{player.Name} you have finished the game with a score of {game.GetCurrentScore()}";
                StopCurrentGame();
            }
        }

        private void ProcessUserInput()
        {
            try
            {
                int solution = Convert.ToInt32(userSolution.Text);
                game.SolveCurrentChallenge(solution);
                PrepareNextChallenge();
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Scoreboard_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
