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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            InitializeComponent();
            Combobox_LesChambres.Items.Add("Test");
            Combobox_LesChambres.Items.Add("Test2");
            Combobox_LesChambres.Items.Add("Test3");
            Combobox_LesHotels.Items.Add("Test");
            Combobox_LesHotels.Items.Add("Test2");
            Combobox_LesHotels.Items.Add("Test3");
            this.SetLaDate(new DateTime(2011,05,11));
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
    }
}
