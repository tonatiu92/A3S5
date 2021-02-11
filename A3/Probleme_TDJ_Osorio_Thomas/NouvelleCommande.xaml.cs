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

        #region Button and slider

        /// <summary>
        /// Ajouter une boisson à la facture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Ajouter une pizza à la facture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Bouton envoyer la commande au cuisine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Envoyer(object sender, RoutedEventArgs e)
        {
            if (commande.FindAll(x => { return x is Pizza; }).Count > 0)
            {
                this.Facture.DataContext = this;
                string nouvelle = creation.NouvelleCommande(commis, commande, traite);
                MessageBox.Show(nouvelle);
                if ((nouvelle != "Renseignez le nom du commis responsable par la commande") && (nouvelle != "Aucun Livreur Disponible, engagé de nouveau livreur"))
                {
                    float solde = creation.Historique.Values.Last().Referente.Solde;
                    if (creation.Historique.Values.Last().Referente.ReductionMenu())
                    {
                        solde -= creation.Historique.Values.Last().Referente.Solde;
                        MessageBox.Show("Plus de 3 Pizza! réduction offerte de " + solde + " Euros!" );
                    }
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Le client doit au moins commander une pizza");
            }
                

        }

        /// <summary>
        /// Commis selectionner dans la combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Commis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Commis.DataContext = this;
            commis = Commis.SelectedItem.ToString();
           
        }

        /// <summary>
        /// Lecture du slider pizza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtSlider1.Text = Slider1.Value.ToString();
        }

        /// <summary>
        /// Lecture du slider boisson
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.txtBoisson.DataContext = this;
            txtBoisson.Text = Slider2.Value.ToString();
        }

        /// <summary>
        /// Retirer un produit de la facture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion
    }
}
