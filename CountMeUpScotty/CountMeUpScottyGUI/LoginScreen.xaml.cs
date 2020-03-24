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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("login_Click needs to be implemented!");
            if (nickname.Text == "")
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
    }
}
