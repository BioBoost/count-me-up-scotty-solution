using CountMeUpScottyLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace CountMeUpScottyGUI
{
    public partial class OverviewScreen : Window
    {
        public OverviewScreen(List<SumChallenge> challenges, PlayerScore score)
        {
            InitializeComponent();
            BuildOverview(challenges);
            DisplayScore(score);
        }

        //Thanks at Emma Dossche for the example
        private void BuildOverview(List<SumChallenge> challenges)
        {
            // SOURCE: https://stackoverflow.com/questions/2891736/can-i-dynamically-make-part-of-the-textblock-text-to-different-colour
            // SOURCE: https://docs.microsoft.com/en-us/dotnet/api/system.windows.textdecorations.strikethrough?view=netframework-4.8
            ChallengeField.Text = "";
            SolutionField.Inlines.Clear();

            var brushConverter = new BrushConverter();
            var incorrect = (Brush)brushConverter.ConvertFromString("#dc322f");
            var correct = (Brush)brushConverter.ConvertFromString("#859900");

            foreach (SumChallenge challenge in challenges)
            {
                ChallengeField.Text += challenge.BasicChallengeAsString() + Environment.NewLine;
                if (challenge.IsCorrectlySolved())
                {
                    SolutionField.Inlines.Add(new Run(challenge.Solution + Environment.NewLine) { Foreground = correct });
                }
                else
                {
                    SolutionField.Inlines.Add(new Run(challenge.Attempt.ToString()) { Foreground = incorrect,
                        TextDecorations = TextDecorations.Strikethrough
                    });
                    SolutionField.Inlines.Add(new Run(" → ") { });
                    SolutionField.Inlines.Add(new Run(challenge.Solution + Environment.NewLine) { Foreground = correct });
                }
            }
        }

        private void DisplayScore(PlayerScore score)
        {
            ResultMessage.Text = $"{score.Player.Name} you have finished the game with a score of {score.Score} within a time of {score.ShortTime}";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
