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
    public partial class ScoreboardScreen : Window
    {
        public ScoreboardScreen()
        {
            InitializeComponent();
            scores.Load("scores.dat");
            datagridScoreboard.ItemsSource = scores.GetScores();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
        }

        private Scoreboard scores = new Scoreboard();
    }
}
