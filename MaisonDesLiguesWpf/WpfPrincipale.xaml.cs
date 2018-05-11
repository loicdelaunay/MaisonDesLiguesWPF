using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BaseDeDonnees;
using MaisonDesLiguesWpf;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using MaisonDesLiguesWpf.Composants;

namespace MaisonDesLiguesWpf
{
    /// <summary>
    /// Logique d'interaction pour WinPrincipale.xaml
    /// </summary>
    public partial class WinPrincipale : MetroWindow
    {
        internal BaseDeDonnees.Bdd UneConnexion;
        private String IdStatutSelectionne = "";
        private DataView dvListFiltre;

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
            BtnEnregistreBenevole.IsEnabled = (TxtDateNaissance.IsMaskFull);
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
            ComposantMultitool repas = new ComposantMultitool
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

        /// <summary>
        /// Refresh de la liste des participants dans l'onglet participant
        /// </summary>
        private void refreshListParticipants()
        {
            DataSet dtSet = new DataSet();
            DataTable dtTable = UneConnexion.ObtenirDonnesOracle("PARTICIPANT");
            dtSet.Tables.Add(dtTable);
            listParticipants.DataContext = dtSet.Tables[0];
            dvListFiltre = dtTable.DefaultView;
        }

        /// <summary>
        /// Rafraichir la liste des participants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRefreshList_Click(object sender, RoutedEventArgs e)
        {
            refreshListParticipants();
        }

        /// <summary> 
        /// Permet d'intercepter le click sur le bouton d'enregistrement d'un bénévole. 
        /// Cetteméthode va appeler la méthode InscrireBenevole de la Bdd, après avoir mis en forme certains paramètres à envoyer. 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void BtnEnregistreBenevole_Click(object sender, RoutedEventArgs e)
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
                    //TODO foreach (Control UnControle in PanNuiteIntervenant.Children)
                    //{
                    //    if (UnControle.GetType().Name == "ResaNuite" && ((ComposantNuitee)UnControle).GetNuitSelectionnee())
                    //    {
                    //        // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement  
                    //        //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement(); 
                    //        CategoriesSelectionnees.Add(((ComposantNuitee)UnControle).GetChambreSelected());
                    //        HotelsSelectionnes.Add(((ComposantNuitee)UnControle).GetHotelSelected());
                    //        NuitsSelectionnes.Add(((ComposantNuitee)UnControle).IdNuite);
                    //    }
                    //}
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

        /// <summary>
        /// Change les informations en fonction du participant selectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listParticipants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reloadInfosParticipant();
        }

        /// <summary>
        /// Barre de recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            listParticipants.UnselectAll();
            try{
                dvListFiltre.RowFilter = "NOMPARTICIPANT LIKE '%" + searchbar.Text + "%' OR " + "PRENOMPARTICIPANT LIKE '%" + searchbar.Text + "%'";
                listParticipants.DataContext = dvListFiltre;
            }
            catch { }
        }

        /// <summary>
        /// Bouton d'enegistrement de l'arrivé d'un participant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnregistrerArrive_Click(object sender, RoutedEventArgs e)
        {
            int lengthOfPassword = 24;
            string valid = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890";
            StringBuilder strB = new StringBuilder(100);
            Random random = new Random();
            while (0 < lengthOfPassword--)
            {
                strB.Append(valid[random.Next(valid.Length)]);
            }
            UneConnexion.enregistrerParticipant((listParticipants.SelectedItem as DataRowView).Row["ID"].ToString(), strB.ToString());

            (listParticipants.SelectedItem as DataRowView).Row["DATEENREGISTREMENTARRIVEE"] = DateTime.Now;
            (listParticipants.SelectedItem as DataRowView).Row["CLEWIFI"] = strB.ToString();
            reloadInfosParticipant();
        }

        /// <summary>
        /// Rafraichir les informations du participant
        /// </summary>
        private void reloadInfosParticipant()
        {
            try
            {
                btnEnregistrerArrive.IsEnabled = false;
                imgQRCode.Visibility = Visibility.Hidden;
                codeWifi.Text = "";
                codeWifi.IsEnabled = false;
                btnEnregistrerArrive.Content = "Enregistrer l'arrivée";

                if ((listParticipants.SelectedItem as DataRowView).Row["CLEWIFI"].ToString() != "" && (listParticipants.SelectedItem as DataRowView).Row["DATEENREGISTREMENTARRIVEE"].ToString() != "")
                {
                    btnEnregistrerArrive.IsEnabled = true;
                    Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    string id = (listParticipants.SelectedItem as DataRowView).Row["ID"].ToString();
                    System.Drawing.Image img = qrcode.Draw(id, 2);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms;
                    bi.EndInit();
                    imgQRCode.Source = bi;
                    imgQRCode.Visibility = Visibility.Visible;
                    btnEnregistrerArrive.IsEnabled = false;
                    btnEnregistrerArrive.Content = (listParticipants.SelectedItem as DataRowView).Row["DATEENREGISTREMENTARRIVEE"].ToString();
                    codeWifi.Text = (listParticipants.SelectedItem as DataRowView).Row["CLEWIFI"].ToString();
                }
                else
                {
                    btnEnregistrerArrive.IsEnabled = true;
                }
            }
            catch
            {
            }
        }
    }
}
