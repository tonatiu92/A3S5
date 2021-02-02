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
    /// Logique d'interaction pour NouvelleCommande.xaml
    /// </summary>
    public partial class NouvelleCommande : Window
    {
        MaPizzeria creation;
        Client traite;
        List<Produit> commande;
        string commis;

        public NouvelleCommande( Client client, MaPizzeria use)
        {
            this.DataContext = this;
            InitializeComponent();
            traite = client;
            this.creation = use;
            this.Pizza.ItemsSource = creation.Menu.FindAll(x => x is Pizza);
            this.Boisson.DataContext = this;
            this.Boisson.ItemsSource = creation.Menu.FindAll(x => x is Boisson);
            this.Commis.DataContext = this;
            this.Commis.ItemsSource = creation.EmployeC;
            commande = new List<Produit>();
            this.Facture.DataContext = this;
        }
        private void Button_Nouvelle_Boisson(object sender, RoutedEventArgs e)
        {
            if (Boisson.SelectedItem != null)
            {
                string choisi = Boisson.SelectedItem.ToString();
                Boisson cree = creation.AjouteBoisson(choisi, txtBoisson.Text, commande);
                if (cree.Quantite == 1)
                {
                    this.Facture.Items.Add(commande.Last());
                }
                else if(cree.Quantite > 1)
                {
                    int i = commande.IndexOf(cree);
                    this.Facture.Items.Remove(cree);
                    this.Facture.Items.Insert(i, commande[i]);
                }
                Boisson.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une boisson");
            }
        }

        private void Button_Nouvelle_Pizza(object sender, RoutedEventArgs e)
        {
            if (Pizza.SelectedItem != null)
            {
                string choisi = Pizza.SelectedItem.ToString();
                Pizza cree = creation.AjoutePizza(choisi, txtSlider1.Text, commande);
                if (cree.Quantite == 1)
                {
                    this.Facture.Items.Add(commande.Last());
                }
                else if (cree.Quantite > 1)
                {
                    int i = commande.IndexOf(cree);
                    this.Facture.Items.Remove(cree);
                    this.Facture.Items.Insert(i, commande[i]);
                }
                Boisson.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une boisson");
            }
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Facture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Pizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Boisson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Envoyer(object sender, RoutedEventArgs e)
        {
            this.Facture.DataContext = this;
            MessageBox.Show(creation.NouvelleCommande(commis, commande, traite));
            this.Close();

        }

        private void Commis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Commis.DataContext = this;
            commis = Commis.SelectedItem.ToString();
           
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSlider1.Text = Slider1.Value.ToString();
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.txtBoisson.DataContext = this;
            txtBoisson.Text = Slider2.Value.ToString();
        }

        private void Button_Retirer(object sender, RoutedEventArgs e)
        {
            string choisi = this.Facture.SelectedItem.ToString();
            Produit select = commande.Find(x => x.ToString() == choisi);
            if(select.Quantite == 1)
            {
                commande.Remove(select);
                this.Facture.Items.Remove(select);
            }
            else
            {
                int i = commande.IndexOf(select);
                commande[i].Quantite -= 1;
                this.Facture.Items.Remove(select);
                this.Facture.Items.Insert(i, commande[i]);

            }
        }
    }
}
