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
            ViewComplementInscription.Visibility = Visibility.Hidden;
            ViewNuites.Visibility = Visibility.Hidden;
            ViewBenevole.Visibility = Visibility.Hidden;
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
            ViewBenevole.Visibility = Visibility.Hidden;
            ViewComplementInscription.Visibility = Visibility.Hidden;
            PanFonctionIntervenant.Visibility = Visibility.Hidden;
            ViewNuites.Visibility = Visibility.Hidden;
            //ViewLicencie.Visibility = Visibility.Hidden;
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

        public void GererInscriptionLicencie()
        {
            ViewComplementLicencie.Visibility = Visibility.Visible;
            ViewComplementLicencie.Margin = new Thickness(38, 432, 668, 189);
            ViewComplementInscription.Visibility = Visibility.Hidden;
            ViewBenevole.Visibility = Visibility.Hidden;
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
        /// Permet d'intercepter le click sur le bouton d'enregistrement d'un bénévole.
        /// Cetteméthode va appeler la méthode InscrireBenevole de la Bdd, après avoir mis en forme certains paramètres à envoyer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistreBenevole_Click(object sender, EventArgs e)
        {
            Collection<Int16> IdDatesSelectionnees = new Collection<Int16>();
            Int64? NumeroLicence;
            if (TxtLicenceBenevole.IsMaskFull)
            {
                NumeroLicence = System.Convert.ToInt64(TxtLicenceBenevole.Text);
            }
            else
            {
                NumeroLicence = null;
            }


            foreach (Control UnControle in PanelDispoBenevole.Children)
            {
                if (UnControle.GetType().Name == "CheckBox" && ((CheckBox)UnControle).IsChecked == true)
                {
                    /* Un name de controle est toujours formé come ceci : xxx_Id où id représente l'id dans la table
                     * Donc on splite la chaine et on récupére le deuxième élément qui correspond à l'id de l'élément sélectionné.
                     * on rajoute cet id dans la collection des id des dates sélectionnées
                        
                    */
                    IdDatesSelectionnees.Add(System.Convert.ToInt16((UnControle.Name.Split('_'))[1]));
                }
            }
            UneConnexion.InscrireBenevole(TxtboxNom.Text, TextboxPrenom.Text, TxtboxAdresse1.Text, TxtboxAdresse2.Text != "" ? TxtboxAdresse2.Text : null, Msktextbox_CP.Text, TextboxVille.Text, Msktextbox_Tel.IsMaskFull ? Msktextbox_Tel.Text : null, TextboxMail.Text != "" ? TextboxMail.Text : null, System.Convert.ToDateTime(TxtDateNaissance.Text), NumeroLicence, IdDatesSelectionnees);
        }

        /// <summary>
        /// Cette procédure va appeler la procédure .... qui aura pour but d'enregistrer les éléments 
        /// de l'inscription d'un intervenant, avec éventuellment les nuités à prendre en compte        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistrerIntervenant_Click(object sender, EventArgs e)
        {
            try
            {
                if (RadNuiteOui.IsChecked == true)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    foreach (Control UnControle in PanNuiteIntervenant.Children)
                    {
                        //TODO
                        //if (UnControle.GetType().Name == "ResaNuite" && ((ComposantNuitee)UnControle).GetNuitSelectionnee())
                        //{
                        //    // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                        //    //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                        //    CategoriesSelectionnees.Add(((ComposantNuitee)UnControle).GetChambreSelected());
                        //    HotelsSelectionnes.Add(((ComposantNuitee)UnControle).GetHotelSelected());
                        //    NuitsSelectionnes.Add(((ComposantNuitee)UnControle).IdNuite);
                        //}

                    }
                    if (NuitsSelectionnes.Count == 0)
                    {
                        MessageBox.Show("Si vous avez sélectionné que l'intervenant avait des nuités\n in faut qu'au moins une nuit soit sélectionnée");
                    }
                    else
                    {
                        UneConnexion.InscrireIntervenant(TxtboxNom.Text, TextboxPrenom.Text, TxtboxAdresse1.Text, TxtboxAdresse2.Text != "" ? TxtboxAdresse2.Text : null, Msktextbox_CP.Text, TextboxVille.Text, Msktextbox_Tel.IsMaskFull ? Msktextbox_Tel.Text : null, TextboxMail.Text != "" ? TextboxMail.Text : null, System.Convert.ToInt16(ComboboxComplementInscription.SelectedValue), this.IdStatutSelectionne, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes);
                        MessageBox.Show("Inscription intervenant effectuée");
                    }
                }
                else
                { // inscription sans les nuitées
                    UneConnexion.InscrireIntervenant(TxtboxNom.Text, TextboxPrenom.Text, TxtboxAdresse1.Text, TxtboxAdresse2.Text != "" ? TxtboxAdresse2.Text : null, Msktextbox_CP.Text, TextboxVille.Text, Msktextbox_Tel.IsMaskFull ? Msktextbox_Tel.Text : null, TextboxMail.Text != "" ? TextboxMail.Text : null, System.Convert.ToInt16(ComboboxComplementInscription.SelectedValue), this.IdStatutSelectionne);
                    MessageBox.Show("Inscription intervenant effectuée");

                }


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
