using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Xceed.Utils;
using BaseDeDonnees;
using Xceed.Wpf;
using System.Net.Mail;
using MahApps.Metro.Controls.Dialogs;

namespace MaisonDesLiguesWpf
{
    internal abstract class Utilitaire
    {

        /// <summary>
        /// Cette méthode permet de renseigner les propriétés des contrôles à créer. C'est une partie commune aux 
        /// 3 types de participants : intervenant, licencié, bénévole
        /// </summary>
        /// <param name="UnePage">le formulaire concerné</param>  
        /// <param name="UnContainer">le panel ou le groupbox dans lequel on va placer les controles</param> 
        /// <param name="UnControleAPlacer"> le controle en cours de création</param>
        /// <param name="UnPrefixe">les noms des controles sont standard : NomControle_XX
        ///                                         ou XX estl'id de l'enregistrement récupéré dans la vue qui
        ///                                         sert de siurce de données</param> 
        /// <param name="UneLigne">un enregistrement de la vue, celle pour laquelle on crée le contrôle</param> 
        /// <param name="i"> Un compteur qui sert à positionner en hauteur le controle</param>   
        /// <param name="callback"> Le pointeur de fonction. En fait le pointeur sur la fonction qui réagira à l'événement déclencheur </param>
        private static void AffecterControle(Window UnePage, Panel UnContainer, ButtonBase UnControleAPlacer, String UnPrefixe, DataRow UneLigne, Int16 i, Action<object, EventArgs> callback)
        {
            UnControleAPlacer.Name = UnPrefixe + UneLigne[0];
            UnControleAPlacer.Width = 320;
            UnControleAPlacer.Content = UneLigne[1].ToString();
            Thickness margin = UnControleAPlacer.Margin;
            margin.Top = 5 + (10 * i);
            margin.Left = 13;
            UnControleAPlacer.Visibility = Visibility.Visible;
            System.Type UnType = UnePage.GetType();
            UnContainer.Children.Add(UnControleAPlacer);

        }
        /// <summary>
        /// Créé une combobox dans un container avec le nom passé en paramètre
        /// </summary>
        /// <param name="UnContainer">panel ou groupbox</param> 
        /// <param name="unNom">nom de la groupbox à créer</param> 
        /// <param name="UnTop">positionnement haut dans le container  </param> 
        /// <param name="UnLeft">positionnement bas dans le container </param> 
        public static void CreerCombo(StackPanel UnContainer, String unNom, Int16 UnTop, Int16 UnLeft)
        {
            CheckBox UneCheckBox = new CheckBox();
            UneCheckBox.Name = unNom;
            Thickness margin = UneCheckBox.Margin;
            margin.Top = UnTop;
            margin.Left = UnLeft;
            UneCheckBox.Margin = margin;
            UneCheckBox.Visibility = Visibility.Visible;
            UnContainer.Children.Add(UneCheckBox);
        }
        /// <summary>
        /// Cette méthode crée des controles de type chckbox ou radio button dans un controle de type panel.
        /// Elle va chercher les données dans la base de données et crée autant de controles (les uns au dessous des autres
        /// qu'il y a de lignes renvoyées par la base de données.
        /// </summary>
        /// <param name="UneForme">Le formulaire concerné</param> 
        /// <param name="UneConnexion">L'objet connexion à utiliser pour la connexion à la BD</param> 
        /// <param name="pUneTable">Le nom de la source de données qui va fournir les données. Il s'agit en fait d'une vue de type
        /// VXXXXOn ou XXXX représente le nom de la tabl à partir de laquelle la vue est créée. n représente un numéro de séquence</param>  
        /// <param name="pPrefixe">les noms des controles sont standard : NomControle_XX
        ///                                         ou XX estl'id de l'enregistrement récupéré dans la vue qui
        ///                                         sert de source de données</param>
        /// <param name="UnPanel">panel ou groupbox dans lequel on va créer les controles</param>
        /// <param name="unTypeControle">type de controle à créer : checkbox ou radiobutton</param>
        /// <param name="callback"> Le pointeur de fonction. En fait le pointeur sur la fonction qui réagira à l'événement déclencheur </param>
        public static void CreerDesControles(Window UneForme, Bdd UneConnexion, String pUneTable, String pPrefixe, Panel UnPanel, String unTypeControle, Action<object, EventArgs> callback)
        {
            DataTable UneTable = UneConnexion.ObtenirDonnesOracle(pUneTable);
            // on va récupérer les statuts dans un datatable puis on va parcourir les lignes(rows) de ce datatable pour 
            // construire dynamiquement les boutons radio pour le statut de l'intervenant dans son atelier


            Int16 i = 0;
            foreach (DataRow UneLigne in UneTable.Rows)
            {
                //object UnControle = Activator.CreateInstance(object unobjet, unTypeControle);
                //UnControle=Convert.ChangeType(UnControle, TypeC);

                if (unTypeControle == "CheckBox")
                {
                    CheckBox UnControle = new CheckBox();
                    AffecterControle(UneForme, UnPanel, UnControle, pPrefixe, UneLigne, i++, callback);

                }
                else if (unTypeControle == "RadioButton")
                {
                    RadioButton UnControle = new RadioButton();
                    AffecterControle(UneForme, UnPanel, UnControle, pPrefixe, UneLigne, i++, callback);
                    UnControle.Checked += new RoutedEventHandler(callback);
                }
                i++;
            }
            UnPanel.Height = 20 * i + 5;
        }
        /// <summary>
        /// méthode permettant de remplir une combobox à partir d'une source de données
        /// </summary>
        /// <param name="UneConnexion">L'objet connexion à utiliser pour la connexion à la BD</param>
        /// <param name="UneCombo"> La combobox que l'on doit remplir</param>
        /// <param name="UneSource">Le nom de la source de données qui va fournir les données. Il s'agit en fait d'une vue de type
        /// VXXXXOn ou XXXX représente le nom de la tabl à partir de laquelle la vue est créée. n représente un numéro de séquence</param>
        public static void RemplirComboBox(Bdd UneConnexion, ComboBox UneCombo, String UneSource)
        {

            UneCombo.ItemsSource = UneConnexion.ObtenirDonnesOracle(UneSource).Rows;
            UneCombo.DisplayMemberPath = ".[" + "libelle" + "]";
            UneCombo.SelectedValuePath = ".[" + "id" + "]";
        }
        /// <summary>
        /// Cette fonction va compter le nombre de controles types CheckBox qui sont cochées contenus dans la collection controls
        /// du container passé en paramètre
        /// </summary>
        /// <param name="UnContainer"> le container sur lequel on va compter les controles de type checkbox qui sont checked</param>
        /// <returns>nombre  de checkbox cochées</returns>
        internal static int CompteChecked(StackPanel UnContainer)
        {
            Int16 i = 0;
            foreach (Control UnControle in UnContainer.Children)
            {
                if (UnControle.GetType().Name == "CheckBox" && ((CheckBox)UnControle).IsChecked == true)
                {
                    i++;
                }
            }
            return i;
        }

        /// <summary>
        /// Envoi du mail.
        /// </summary>
        /// <param name="mail">Mail destinataire</param>
        /// <param name="prenom">Prénom du destinataire</param>
        /// <param name="nom">Nom du destinataire</param>
        public static void SendMail(string mail, string prenom, string nom)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("mdlgroupe1@gmail.com");
                message.To.Add(new MailAddress(mail));
                message.CC.Add("mdlgroupe1@gmail.com");
                message.Subject = "Inscription Maison des Ligues";
                message.Body = "Bonjour " + prenom + " " + nom + "," +
                    "\n" +
                    "Merci de vous être inscrit" +
                    "\n" +
                    "\n" + "Ce message fait suite à la réussite de votre inscription à travers notre application" +
                    "\n" + "Maison des ligues \n" +
                    "Bien à vous, le groupe 1.\n" +
                    "\n" +
                    "-------------------------------------------------------------------------------------------------\n" +
                    "Ceci est un message automatique.\n" +
                    "Merci de ne pas y répondre.\n";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("mdlgroupe1@gmail.com", "Guilulo83");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
