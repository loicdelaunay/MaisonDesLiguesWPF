using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using BaseDeDonnees;

namespace MaisonDesLiguesWpf
{
    /// <summary>
    /// Logique d'interaction pour WinPrincipale.xaml
    /// </summary>
    public partial class WinPrincipale : MetroWindow
    {
        internal BaseDeDonnees.Bdd UneConnexion;

        public WinPrincipale()
        {
            InitializeComponent();
            ViewComplementInscription.Visibility = Visibility.Hidden;
            ViewNuites.Visibility = Visibility.Hidden;
            ViewBenevole.Visibility = Visibility.Hidden;

            new ComposantNuitee
            {
                Margin = new Thickness(400, 400, 200, 200)
            };
        }

        public void InitBddConnexion(Bdd UneConnexionOracle)
        {
            UneConnexion = UneConnexionOracle;
        }

        /// <summary>
        /// Bouton pour quitter l'application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Btn_ExitAsync(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Voulez-vous quitter l'application ?", "APP_NAME", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //if (result == MessageBoxResult.Yes)
            //{
            //    Application.Current.Shutdown();
            //}
            MessageDialogResult result = await this.ShowMessageAsync("MAISON DES LIGUES", "Voulez-vous quitter l'application ?",MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                Application.Current.Shutdown();
            }

            return;
        }

        /// <summary>
        /// permet d'appeler la méthode VerifBtnEnregistreIntervenant qui déterminera le statu du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbStatutIntervenant_StateChanged(object sender, EventArgs e)
        {
            // stocke dans un membre de niveau form l'identifiant du statut sélectionné (voir règle de nommage des noms des controles : prefixe_Id)
            //this.IdStatutSelectionne = ((RadioButton)sender).Name.Split('_')[1];
            //BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }

        /// <summary>
        /// Gestion des radio button du type de participant 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadTypeParticipant_Changed(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "RadBenevole":
                    this.GererInscriptionBenevole();
                    break;
                case "RadLicencie":
                    this.GererInscriptionLicencie();
                    break;
                case "RadIntervenant":
                    this.GererInscriptionIntervenant();
                    break;
                default:
                    throw new Exception("Erreur interne dans l'application");

            }
        }

        /// <summary>
        /// Gestion 
        /// </summary>
        public void GererInscriptionBenevole()
        {
            ViewBenevole.Visibility = Visibility.Visible;
            ViewBenevole.Margin = new Thickness(40, 433, 599, 103);
            ViewComplementInscription.Visibility = Visibility.Hidden;
        }

        public void GererInscriptionLicencie()
        {
            //ViewBenevole.Visibility = Visibility.Hidden;
        }


        private void GererInscriptionIntervenant()
        {
            //ViewComplementInscription.Visibility = Visibility.Visible;
            //ViewBenevole.Visibility = Visibility.Hidden;
            //ViewNuites.Visibility = Visibility.Visible;
            ////ViewLicencie.Visibility = Visibility.Hidden;
            //PanFonctionIntervenant.Visibility = Visibility.Visible;
            //Thickness margin = new Thickness();
            //margin.Top = 264;
            //margin.Left = 23;
            //GrpIntervenant.Margin = margin;
            //Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanFonctionIntervenant, "RadioButton", this.rdbStatutIntervenant_StateChanged);
            //Utilitaire.RemplirComboBox(UneConnexion, ComboboxComplementInscription, "VATELIER01");

            ComboboxComplementInscription.DataContext = "Choisir";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
