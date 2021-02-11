using System;
using System.Collections;
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
    /// Logique d'interaction pour GestionClient.xaml
    /// </summary>
    public partial class GestionClient : Window
    {
        #region attribut de la fenêtre
        MaPizzeria creation;
        string numero;
        string nom;
        string prenom;
        string num_rue;
        string rue;
        string code_postale;
        string ville;
        #endregion

        /// <summary>
        /// Initialise la fenêtre
        /// </summary>
        /// <param la pizzeria ="use"></param>
        public GestionClient(MaPizzeria use)
        {
            creation = use;
            this.DataContext = this;
            InitializeComponent();
            Refresh();
            this.ListClient.ItemsSource = creation.FichierClient.Values;
        }

        #region Gestion d'event
        /// <summary>
        /// active les events
        /// </summary>
        public void Refresh()
        {
            creation.SuppressionClient += Supprimer_Client_ApprovedEvent;
        }

        /// <summary>
        /// Mets à jour la liste view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Supprimer_Client_ApprovedEvent(object sender, Client e)
        {
            ListClient.ItemsSource = null;
            ListClient.ItemsSource = creation.FichierClient.Values;
        }
        #endregion

        #region Gestion Boutons
        /// <summary>
        /// Bouton pour rechercher le client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recherche_Client(object sender, RoutedEventArgs e)
        {
            int recherche = creation.FichierClient.Keys.IndexOf(numero);
            bool exist = Int32.TryParse(numero, out recherche);
            {
                Client trouve = creation.FichierClient.Values[recherche];
                if (recherche != -1)
                {
                    MessageBox.Show(trouve.ToString());
                    ListClient.SelectedItem = creation.FichierClient.Values[recherche];
                }
                else
                {
                    MessageBox.Show("ce Client n'existe pas ");
                }
            }
        }

        /// <summary>
        /// Bouton pour supprimer un client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer_Client(object sender, RoutedEventArgs e)
        {
            if (ListClient.SelectedItem != null)
            {
                Client recherche = creation.RechercheClient(ListClient.SelectedItem.ToString());
                MessageBox.Show(creation.SupprimeClient(recherche));
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un Client");
            }
        }

        /// <summary>
        /// Boutons pour trier les montants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tri_Montants(object sender, RoutedEventArgs e)
        {
            List<Client> tri = new List<Client>();
            foreach(Client element in creation.FichierClient.Values)
            {
                tri.Add( element);
            }
            tri.Sort();
           
            ListClient.ItemsSource = null;
            ListClient.ItemsSource = tri;
        }

        /// <summary>
        /// Bouton pour trier selon les noms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tri_Nom(object sender, RoutedEventArgs e)
        {
            List<Client> tri = new List<Client>();
            foreach (Client element in creation.FichierClient.Values)
            {
                tri.Add(element);
            }
            tri.Sort(Client.myCompare);
            ListClient.ItemsSource = null;
            ListClient.ItemsSource = tri;
        }

        /// <summary>
        /// Bouton pour trier selon la ville de domicile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tri_Ville(object sender, RoutedEventArgs e)
        {
            List<Client> tri = new List<Client>();
            foreach (Client element in creation.FichierClient.Values)
            {
                tri.Add(element);
            }
            tri.Sort(Client.myCompareVille);
            ListClient.ItemsSource = null;
            ListClient.ItemsSource = tri;
        }

        /// <summary>
        /// Bouton pour calculer la moyenne des achats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Moyenne_Client(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Le montant moyen des comptes clients est: " + Convert.ToString(creation.MoyenneMontants()), "Statistiques", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        /// <summary>
        /// Bouton pour modifier un client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            string adresse = num_rue + "," + rue + "," + code_postale + "," + ville;
            if (ListClient.SelectedItem != null)
            {
                string choisi = ListClient.SelectedItem.ToString();
                Client recherche = creation.RechercheClient(ListClient.SelectedItem.ToString());
                if (nom != "Nom")
                {
                    recherche.Nom = nom;
                    Nom.Text = "Nom";
                }
                if (prenom != "Prenom")
                {
                    recherche.Prenom = prenom;
                    Prenom.Text = "Prenom";
                }
                if ((num_rue != "N°")|| (rue != "Rue")|| (code_postale != "Code Postale")|| (ville != "Ville"))
                {
                    recherche.Adresse = adresse;
                    Num.Text = "N°";
                    Rue.Text = "Rue";
                    CodePostale.Text = "Code Postale";
                    Ville.Text = "Ville";
                }
                if (numero != "Numero")
                {
                    recherche.Numero = numero;
                    Telephone.Text = "Numero";
                }
                ListClient.ItemsSource = null;
                ListClient.ItemsSource = creation.FichierClient.Values;

            }
            else
            {
                MessageBox.Show("Veuillez selectionner un Client");
            }
        }

        /// <summary>
        /// renvoie le classement des pizzas préférés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Voici les pizzas Préférés \n" + creation.PizzaPref());
        }
        #endregion

        #region Gestion TextBox
        /// <summary>
        /// Gestion textBox du numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Numero_Text(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero = textbox.Text;
        }

        /// <summary>
        /// Gestion TextBox du nom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            nom = textbox.Text;
        }

        /// <summary>
        /// Gestion TextBox du Prenom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Prenom_Text(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            prenom = textbox.Text;
        }

        /// <summary>
        /// Gestion textbox du numero de rue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumRue_Text(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            num_rue = textbox.Text;
        }

        /// <summary>
        /// Gestion textbox rue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rue_Text(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            rue = textbox.Text;
        }

        /// <summary>
        /// Gestion TextBox code postale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void codePostale_Text(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            code_postale = textbox.Text;
        }

        /// <summary>
        /// Gestion TextBox ville
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ville_Text(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            ville = textbox.Text;
        }
        #endregion

       
    }
}
