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
    public partial class LoginScreen : Window
    {
        private static string ANONYMOUS = "Anonymous";

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (nickname.Text == "" || nickname.Text == ANONYMOUS)
            {
                MessageBox.Show("Please enter a nickname", "Count Me Up Scotty");
                nickname.Focus();
            }
            else
            {
                GameScreen gamescreen = new GameScreen(nickname.Text);
                gamescreen.Show();
                this.Close();
            }
        }

        private void nickname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nickname.Text == ANONYMOUS)
            {
                nickname.Text = "";
            }
        }
    }
}
