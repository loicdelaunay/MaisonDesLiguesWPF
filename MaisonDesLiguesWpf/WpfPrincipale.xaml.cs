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

namespace MaisonDesLiguesWpf
{
    /// <summary>
    /// Logique d'interaction pour WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        public WinLogin()
        {
            InitializeComponent();
        }

        /**
         * controle du textbox tel
        **/
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MaskedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
