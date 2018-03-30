using System;
using BaseDeDonnees;
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

namespace MaisonDesLiguesWpf
{
    /// <summary>
    /// Logique d'interaction pour WpfPrincipale.xaml
    /// </summary>
    public partial class WpfPrincipale : Window
    {
        public WpfPrincipale()
        {
            InitializeComponent();
        }

        internal BaseDeDonnees.Bdd UneConnexion;

        private void BtnLogin_Click(object sendser, RoutedEventArgs e)
        {
            try
            {
                if (RadMaison.IsChecked == true)
                {
                    UneConnexion = new Bdd(Login.Text, Password.Text, true);
                }
                else
                {
                    UneConnexion = new Bdd(Login.Text, Password.Text, false);
                }
                this.Hide();
                WinPrincipale Principale = new WinPrincipale();
                Principale.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// gestion de l'activation/désactivation du bouton ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControleValide(object sender, EventArgs e)
        {
            if (RadLycee.IsChecked == false && RadMaison.IsChecked == false)
            {
                BtnLogin.IsEnabled = false;
            }
            else
            {
                if (Login.Text.Length == 0 || Password.Text.Length == 0)
                {
                    BtnLogin.IsEnabled = false;
                }
                else
                {
                    BtnLogin.IsEnabled = true;
                }
            }

        }

        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Login.Text = "";
        }

        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            Login.Text = "Veuillez entrer votre Login !";
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            Password.Text = "";
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            Password.Text = "Veuillez entrer votre mot de passe";
        }
    }
}
