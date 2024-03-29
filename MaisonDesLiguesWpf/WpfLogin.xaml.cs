﻿using System;
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
using MahApps.Metro.Controls;

namespace MaisonDesLiguesWpf
{
    /// <summary>
    /// Logique d'interaction pour WpfPrincipale.xaml
    /// </summary>
    public partial class WpfLogin : MetroWindow
    {
        public WpfLogin()
        {
            InitializeComponent();
        }

        internal BaseDeDonnees.Bdd UneConnexion;

        private void BtnLogin_Click(object sendser, RoutedEventArgs e)
        {
            try
            {
                if (RadLoginMaison.IsChecked == false)
                {
                    UneConnexion = new Bdd(TextboxLoginLogin.Text, TextboxLoginMdp.Text, true);
                }
                else
                {
                    UneConnexion = new Bdd(TextboxLoginLogin.Text, TextboxLoginMdp.Text, false);
                }
                this.Hide();
                WinPrincipale Principale = new WinPrincipale();
                Principale.InitBddConnexion(UneConnexion);
                Principale.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
