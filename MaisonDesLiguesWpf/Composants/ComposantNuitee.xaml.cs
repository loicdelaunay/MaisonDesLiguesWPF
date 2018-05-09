using System;
using System.Collections.Generic;
using System.Windows;

namespace MaisonDesLiguesWpf
{
    /// <summary>
    /// Logique d'interaction pour ComposantNuitee.xaml
    /// </summary>
    public partial class ComposantNuitee
    {

        private Boolean isChecked;
        private DateTime laDate;
        private List<String> lesHotels;
        private List<String> lesChambres;

        public ComposantNuitee()
        {
        }

        /// <summary>
        /// Setter laDate
        /// </summary>
        /// <param name="uneDate"></param>
        public void SetLaDate(DateTime uneDate)
        {
            this.laDate = uneDate;
            Lbl_laDate.Content = this.laDate.ToString();
        }

        /// <summary>
        /// Getter de la date
        /// </summary>
        /// <returns></returns>
        public DateTime GetLaDate()
        {
            return this.laDate;
        }

        /// <summary>
        /// Retourner l'état du composant 
        /// </summary>
        /// <returns></returns>
        public Boolean IsChecked()
        {
            return (Boolean)Chbx_laCheckbox.IsChecked;
        }

        /// <summary>
        /// Ajoute un hotel
        /// </summary>
        /// <param name="unHotel"></param>
        public void AddHotel(string unHotel)
        {
            lesHotels.Add(unHotel);
            Combobox_LesHotels.Items.Add(unHotel);
        }

        /// <summary>
        /// Ajoute un hotel
        /// </summary>
        /// <param name="uneChambre"></param>
        public void AddChambre(string uneChambre)
        {
            lesChambres.Add(uneChambre);
            Combobox_LesChambres.Items.Add(uneChambre);
        }

        /// <summary>
        /// Quand l'utilisateur cliquer sur la case à cocher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedClick(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Retourne l'Hotel Séléctionné 
        /// </summary>
        /// <returns></returns>
        public string GetHotelSelected()
        {
            return Combobox_LesHotels.SelectedIndex.ToString();
        }

        /// <summary>
        /// Retourne la chambre Séléctionné 
        /// </summary>
        /// <returns></returns>
        public string GetChambreSelected()
        {
            return Combobox_LesChambres.SelectedIndex.ToString();
        }
    }
}
