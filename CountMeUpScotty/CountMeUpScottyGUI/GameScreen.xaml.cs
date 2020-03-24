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
            Console.WriteLine("startgame_Click is not implemented");
        }

        // Attribute
        private Player player = null;
    }
}
