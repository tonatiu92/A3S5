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

namespace Probleme_TDJ_Osorio_Thomas
{
    /// <summary>
    /// Logique d'interaction pour GestionCommis.xaml
    /// </summary>
    public partial class GestionCommis : Page
    {
        #region attribut de la fenêtre
        MaPizzeria creation;
        string nom;
        string prenom;
        string numero;
        string numero_rue;
        string rue;
        string code_postale;
        string ville;
#endregion

        /// <summary>
        /// Initialise une page pour les commis
        /// </summary>
        /// <param pizzeria="use"></param>
        public GestionCommis(MaPizzeria use)
        {
            creation = use;
            this.DataContext = this;
            InitializeComponent();
            Refresh();
            this.ListCommis.ItemsSource = creation.EmployeC;
        }

        #region Gestion d'event
        /// <summary>
        /// lance les différents events
        /// </summary>
        public void Refresh()
        {
            creation.NouveauCommisApproved += Nouveau_Commis_ApprovedEvent;
            creation.SuppressionCommis += Supprimer_Commis_ApprovedEvent;
        }

        /// <summary>
        /// Actualise la liste view après ajout d'un commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Nouveau_Commis_ApprovedEvent(object sender, Commis e)
        {
            ListCommis.ItemsSource = null;
            ListCommis.ItemsSource = creation.EmployeC;
        }

        /// <summary>
        /// Actualse la liste view après suppression d'un commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Supprimer_Commis_ApprovedEvent(object sender, Commis e)
        {
            ListCommis.ItemsSource = null;
            ListCommis.ItemsSource = creation.EmployeC;
        }
        #endregion

        #region Gestion des Boutons
        /// <summary>
        /// Ajoute un nouveau commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter(object sender, RoutedEventArgs e)
        {
            int number;
            string adresse = numero_rue + "," + rue + "," + code_postale + "," + ville;
            bool test = Int32.TryParse(numero, out number);
            if((nom != null)&&(prenom != null) && (adresse != null) && (numero != null)&&(test))
            {
                Commis cree = new Commis(nom, prenom, false, adresse, numero, DateTime.Now);
                MessageBox.Show(creation.AjouterCommis(cree));

            }
            else
            {
                MessageBox.Show("Vous n'avez pas rempli tous les champs");
            }
        }
       
        /// <summary>
        /// Bouton pour rechercher un commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rechercher(object sender, RoutedEventArgs e)
        {
            List<Commis> trouve = new List<Commis>();
            if(nom != null)
            {
                trouve = creation.EmployeC.FindAll(x => x.Nom == nom);
            }
            if (trouve.Count < 1)
            {
                trouve = creation.EmployeC.FindAll(x => x.Prenom == prenom);
            }
            if (trouve.Count < 1)
            {
                 trouve = creation.EmployeC.FindAll(x => x.Numero == numero);
            }
            if (trouve.Count >= 1)
            {
                MessageBox.Show(trouve[0].ToString());
                ListCommis.SelectedItem = creation.EmployeC[creation.EmployeC.IndexOf(trouve[0])];
            }
            else
            {
                MessageBox.Show("ce commis n'existe pas ");
            }
        }

        /// <summary>
        /// Bouton pour supprimer un commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            if(ListCommis.SelectedItem != null) {
                Commis suppr = creation.EmployeC.Find(x => x.ToString() == ListCommis.SelectedItem.ToString());
                MessageBox.Show(creation.SupprimeCommis(suppr));
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un commis");
            }
        }

        /// <summary>
        /// Bouton pour trier les commis par nom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tri_Nom(object sender, RoutedEventArgs e)
        {
            creation.EmployeC.Sort();
            ListCommis.ItemsSource = null;
            ListCommis.ItemsSource = creation.EmployeC;
        }

        /// <summary>
        /// Bouton pour trier les commis par ville de domicile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tri_Ville(object sender, RoutedEventArgs e)
        {
            creation.EmployeC.Sort(Commis.myCompareVille);
            ListCommis.ItemsSource = null;
            ListCommis.ItemsSource = creation.EmployeC;
        }

        /// <summary>
        /// Bouton pour trier les commis par nombre de commandes réalisés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tri_NbCommandes(object sender, RoutedEventArgs e)
        {
            creation.EmployeC.Sort(Commis.myCompareNbCommandes);
            ListCommis.ItemsSource = null;
            ListCommis.ItemsSource = creation.EmployeC;
        }

        /// <summary>
        /// Bouton pour modifier un commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Commis(object sender, RoutedEventArgs e)
        {
            string adresse = numero_rue + "," + rue + "," + code_postale + "," + ville;
            if (ListCommis.SelectedItem != null)
            {
                string choisi = ListCommis.SelectedItem.ToString();
                Commis recherche = creation.RechercheCommis(ListCommis.SelectedItem.ToString());
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
                if ((numero_rue != "N°") || (rue != "Rue") || (code_postale != "Code Postale") || (ville != "Ville"))
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
                ListCommis.ItemsSource = null;
                ListCommis.ItemsSource = creation.EmployeC;

            }
            else
            {
                MessageBox.Show("Veuillez selectionner un Commis");
            }
        }
        #endregion

        #region Gestion TextBox
        private void Nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            nom = textbox.Text;
        }
        private void Numero_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero = textbox.Text;
        }
        private void Prenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            prenom = textbox.Text;
        }
        private void NumRue_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero_rue = textbox.Text;
        }

        private void Rue_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            rue = textbox.Text;
        }

        private void CodePostale_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            code_postale = textbox.Text;
        }

        private void Ville_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            ville= textbox.Text;
        }
        #endregion 
    }
}
