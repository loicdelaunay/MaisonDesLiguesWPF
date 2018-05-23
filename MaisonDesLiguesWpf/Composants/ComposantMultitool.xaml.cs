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

namespace MaisonDesLiguesWpf.Composants
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class ComposantMultitool : UserControl
    {
        public ComposantMultitool()
        {
            InitializeComponent();
        }

        public void ToggleCheckbox()
        {
            //TODO si utile
        }

        /// <summary>
        /// Indique si le checkbox du composant est coché 
        /// </summary>
        /// <returns>Boolean checkbox</returns>
        public bool IsChecked()
        {
            return (bool)checkbox.IsChecked;
        }

        /// <summary>
        /// Ajoute un élément à la liste 1
        /// </summary>
        /// <param name="Element"></param>
        public void AddElementList1(string Element)
        {
            listbox1.Items.Add(Element);
        }

        /// <summary>
        /// Récupère l'élément séléctionné de la liste 1
        /// </summary>
        /// <returns></returns>
        public string GetElementSelectedList1()
        {
            return listbox1.SelectedIndex.ToString();
        }

        /// <summary>
        /// Ajoute un élément à la liste 2
        /// </summary>
        /// <param name="Element"></param>
        public void AddElementList2(string Element)
        {
            listbox2.Items.Add(Element);
        }

        /// <summary>
        /// Récupère l'élément séléctionné de la liste 2
        /// </summary>
        /// <returns></returns>
        public string GetElementSelectedList2()
        {
            return listbox2.SelectedIndex.ToString();
        }
    }
}
