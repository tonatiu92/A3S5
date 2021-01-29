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
        public Client traite;
        List<Produit> commande;
        string commis;

        public NouvelleCommande( Client client)
        {
            this.DataContext = this;
            InitializeComponent();
            traite = client;
            this.Pizza.ItemsSource = MainWindow.creation.Menu.FindAll(x => x is Pizza);
            this.Boisson.DataContext = this;
            this.Boisson.ItemsSource = MainWindow.creation.Menu.FindAll(x => x is Boisson);
            this.Commis.DataContext = this;
            this.Commis.ItemsSource = MainWindow.creation.EmployeC;
            commande = new List<Produit>();
            this.Facture.DataContext = this;
        }
        private void Button_ok(object sender, RoutedEventArgs e)
        {
          
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Nouvelle_Boisson(object sender, RoutedEventArgs e)
        {
            if (Boisson.SelectedItem != null)
            {
                string choisi = Boisson.SelectedItem.ToString();
                MessageBox.Show(choisi);
                Boisson recher = new Boisson((Produit)MainWindow.creation.RechercheMenuBoisson(choisi), MainWindow.creation.RechercheMenuBoisson(choisi));
                if (recher.ToString() == choisi)
                {
                    recher.Taille = Convert.ToInt32(txtBoisson.Text);
                    recher.CalculPrix();
                    Boisson exist = (Boisson) commande.Find(x => x.ToString() == recher.ToString());
                    if (exist is null)
                    {
                        commande.Add(recher);
                        this.Facture.Items.Add(recher);
                    }
                    else
                    {
                        int i = commande.IndexOf(exist);
                        commande[i].Quantite += 1;
                        commande[i].Prix += exist.Prix;
                        this.Facture.Items.Remove(exist);
                        this.Facture.Items.Insert(i, commande[i]);
                    }

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
            if(Pizza.SelectedItem != null)
            {
                string choisi = Pizza.SelectedItem.ToString();
                Pizza recher = new Pizza((Produit)MainWindow.creation.RechercheMenuPizza(choisi), MainWindow.creation.RechercheMenuPizza(choisi));
                if (recher.ToString() == choisi)
                {
                    recher.Taille = Convert.ToInt32(txtSlider1.Text);
                    recher.CalculPrix();
                    Pizza exist = (Pizza)commande.Find(x => x.ToString() == recher.ToString());
                    if (exist is null)
                    {
                        commande.Add(recher);
                        this.Facture.Items.Add(recher);
                    }
                    else
                    {
                        int i = commande.IndexOf(exist);
                        commande[i].Quantite += 1;
                        commande[i].Prix += exist.Prix;
                        this.Facture.Items.Remove(exist);
                        this.Facture.Items.Insert(i, commande[i]);
                    }

                }
                Pizza.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une pizza");
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
            MessageBox.Show(MainWindow.creation.NouvelleCommande(commis, commande, traite));
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
