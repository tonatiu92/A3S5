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

namespace Probleme_TDJ_Osorio_Thomas
{
    /// <summary>
    /// Logique d'interaction pour GestionFournisseur.xaml
    /// </summary>
    public partial class GestionFournisseur : Window
    {
        MaPizzeria creation;
        string nom;
        string produit;
        string quantite;
       
        public GestionFournisseur(MaPizzeria use)
        {
            creation = use;
            this.DataContext = this;
            InitializeComponent();
            ListFourn.ItemsSource = creation.Partenaire;
            this.Stock.DataContext = this;
            Stock.ItemsSource = creation.Stock;
        }
        #region TextBox
        /// <summary>
        /// Gestion TextBox pour le nom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            nom = textbox.Text;
        }

        /// <summary>
        /// Gestion TextBox pour le nom du produit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Produit_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            produit = textbox.Text;
        }

        /// <summary>
        /// TextBox pour la quantite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quantite_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            quantite = textbox.Text;
        }
        #endregion

        #region Button

        /// <summary>
        /// Bouton pour acheter au fournisseur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Acheter(object sender, RoutedEventArgs e)
        {
            int number;
            bool exist = Int32.TryParse(quantite, out number);
            if(ListFourn.SelectedItem != null)
            {
                string choisi = ListFourn.SelectedItem.ToString();
                MessageBox.Show(creation.Achete(choisi, number));
            }
            else
            {
                MessageBox.Show("Selectionnez un fournisseurs");
            }
            Stock.ItemsSource = null;
            Stock.ItemsSource = creation.Stock;
        }

        /// <summary>
        /// Trier selon le montant d'achats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriAchats(object sender, RoutedEventArgs e)
        {
            creation.Partenaire.Sort();
            ListFourn.ItemsSource = null;
            ListFourn.ItemsSource = creation.Partenaire;

        }

        /// <summary>
        /// Trier selon la quantite en stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriStock(object sender, RoutedEventArgs e)
        {
            creation.Stock.Sort();
            Stock.ItemsSource = null;
            Stock.ItemsSource = creation.Stock;
        }

        /// <summary>
        /// Rechercher un fournisseur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RechercheFournisseur(object sender, RoutedEventArgs e)
        {
            List<Fournisseur> trouve = new List<Fournisseur>();
            if (nom != null)
            {
                trouve = creation.Partenaire.FindAll(x => x.Nom == nom);
            }
            if (trouve.Count < 1)
            {
                trouve = creation.Partenaire.FindAll(x => x.TypeProduit== produit);
            }
            if (trouve.Count >= 1)
            {
                Nom.Text = "Nom";
                Produit.Text = "Produit";
                MessageBox.Show(trouve[0].ToString());
                ListFourn.SelectedItem = creation.Partenaire[creation.Partenaire.IndexOf(trouve[0])];
            }
            else
            {
                MessageBox.Show("ce fournisseur n'existe pas ");
            }
        }
        #endregion
    }
}
