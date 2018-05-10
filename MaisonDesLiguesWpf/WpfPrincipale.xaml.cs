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
using MaisonDesLiguesWpf;
using System.Collections.ObjectModel;

namespace MaisonDesLiguesWpf
{
    /// <summary>
    /// Logique d'interaction pour WinPrincipale.xaml
    /// </summary>
    public partial class WinPrincipale : MetroWindow
    {
        internal BaseDeDonnees.Bdd UneConnexion;
        private String IdStatutSelectionne = "";

        public WinPrincipale()
        {
            InitializeComponent();
            ViewComplementLicencie.Visibility = Visibility.Hidden;
            ViewComplementInscription.Visibility = Visibility.Hidden;
            ViewNuites.Visibility = Visibility.Hidden;
            ViewBenevole.Visibility = Visibility.Hidden;
            ComposantNuitee nuite = new ComposantNuitee
            {
                Margin = new Thickness(0, 0, 0, 0),
                Name = "test",
                Height = 100,
                Width = 700,
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
            MessageDialogResult result = await this.ShowMessageAsync("MAISON DES LIGUES", "Voulez-vous quitter l'application ?", MessageDialogStyle.AffirmativeAndNegative);
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
            if (sender.GetType() == typeof(RadioButton))
            {
                this.IdStatutSelectionne = ((RadioButton)sender).Name.Split('_')[1];
            }
            BtnComplInscIterven.IsEnabled = VerifBtnEnregistreIntervenant();
        }

        /// <summary>
        /// Méthode privée testant le contrôle combo et la variable IdStatutSelectionne qui contient une valeur
        /// Cette méthode permetra ensuite de définir l'état du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <returns></returns>
        private bool VerifBtnEnregistreIntervenant()
        {
            return (ComboboxComplementInscription.Text != "Choisir" && this.IdStatutSelectionne.Length > 0);
        }

        /// <summary>
        /// Gestion des radio button du type de participant 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadTypeParticipant_Changed(object sender, RoutedEventArgs e)
        {
            ViewComplementLicencie.Visibility = Visibility.Hidden;
            ViewBenevole.Visibility = Visibility.Hidden;
            ViewComplementInscription.Visibility = Visibility.Hidden;
            PanFonctionIntervenant.Visibility = Visibility.Hidden;
            ViewNuites.Visibility = Visibility.Hidden;
            PanFonctionIntervenant.Children.Clear();
            PanFonctionIntervenant.Visibility = Visibility.Hidden;

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
        /// procédure permettant d'afficher l'interface de saisie des disponibilités des bénévoles.
        /// </summary>
        public void GererInscriptionBenevole()
        {
            ViewBenevole.Visibility = Visibility.Visible;
            ViewBenevole.Margin = new Thickness(38, 432, 668, 189);
            PanelDispoBenevole.Children.Clear();
            Utilitaire.CreerDesControles(this, UneConnexion, "VDATEBENEVOLAT01", "ChkDateB_", PanelDispoBenevole, "CheckBox", this.ChkDateBenevole_DataChanged);
            // on va tester si le controle à placer est de type CheckBox afin de lui placer un événement checked_changed
            // Ceci afin de désactiver les boutons si aucune case à cocher du container n'est cochée
            foreach (Control UnControle in PanelDispoBenevole.Children)
            {
                if (UnControle.GetType().Name == "CheckBox")
                {
                    CheckBox UneCheckBox = (CheckBox)UnControle;
                    UneCheckBox.Checked += new System.Windows.RoutedEventHandler(this.ChkDateBenevole_DataChanged);
                    TxtDateNaissance.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ChkDateBenevole_DataChanged);
                    TxtLicenceBenevole.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ChkDateBenevole_DataChanged);
                }
            }
        }

        /// <summary>
        /// Cetet méthode teste les données saisies afin d'activer ou désactiver le bouton d'enregistrement d'un bénévole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkDateBenevole_DataChanged(object sender, EventArgs e)
        {
            BtnEnregistreBenevole.IsEnabled = (TxtDateNaissance.IsMaskFull && TxtLicenceBenevole.IsMaskFull);
        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie des disponibilités des bénévoles.
        /// </summary>
        public void GererInscriptionLicencie()
        {
            ViewComplementLicencie.Visibility = Visibility.Visible;
            ViewComplementLicencie.Margin = new Thickness(38, 432, 668, 189);
        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un intervenant.
        /// </summary>
        private void GererInscriptionIntervenant()
        {
            ViewComplementInscription.Visibility = Visibility.Visible;
            PanFonctionIntervenant.Visibility = Visibility.Visible;
            ViewNuites.Visibility = Visibility.Visible;
            Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanFonctionIntervenant, "RadioButton", this.rdbStatutIntervenant_StateChanged);
            ComboboxComplementInscription.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.rdbStatutIntervenant_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, ComboboxComplementInscription, "VATELIER01");
        }

        /// <summary>
        /// Gestion des radio button du type de participant 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadTypeRepas_Changed(object sender, RoutedEventArgs e)
        {
            ComposantNuitee repas = new ComposantNuitee
            {
                Margin = new Thickness(10, 68, -241, 0),
                Name = "Repas",
                Height = 23,
                Width = 200,
            };
            GrilleRepas.Children.Add(repas);
            switch (((RadioButton)sender).Name)
            {
                case "RadOuiRepasAccompagnant":
                    repas.Visibility = Visibility.Visible;
                    break;
                case "RadNonRepasAccompagnant":
                    repas.Visibility = Visibility.Hidden;
                    break;
                default:
                    throw new Exception("Erreur interne dans l'application");

            }
        }

        /// <summary>
        /// Gestion des radio button du type de participant 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadTypeNuite_Changed(object sender, RoutedEventArgs e)
        {
            ComposantNuitee nuite = new ComposantNuitee
            {
                Margin = new Thickness(10, 60, -241, 0),
                Name = "Nuitée",
                Height = 23,
                Width = 200,
            };
            GrilleRepas.Children.Add(nuite);
            switch (((RadioButton)sender).Name)
            {
                case "RadNuiteOuiLicencie":
                    nuite.Visibility = Visibility.Visible;
                    GrilleNuites.Children.Add(nuite);
                    break;
                case "RadNuiteNonLicencie":
                    nuite.Visibility = Visibility.Hidden;
                    GrilleNuites.Children.Remove(nuite);
                    break;
                default:
                    throw new Exception("Erreur interne dans l'application");

            }
        }

    }
}
