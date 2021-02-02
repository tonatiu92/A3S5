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
    /// Logique d'interaction pour GestionLivreur.xaml
    /// </summary>
    public partial class GestionLivreur : Page
    {
        MaPizzeria creation;
        string nom;
        string prenom;
        string numero;
        string transport;
        string num_rue;
        string rue;
        string code_postale;
        string ville;

        public GestionLivreur(MaPizzeria use)
        {
            creation = use;
            this.DataContext = this;
            InitializeComponent();
            Refresh();
            this.ListLivreur.ItemsSource = creation.EmployeL;
        }
        public void Refresh()
        {
            creation.NouveauLivreurApproved += Nouveau_Livreur_ApprovedEvent;
            creation.SuppressionLivreur += Supprimer_Livreur_ApprovedEvent;
        }
        public void Nouveau_Livreur_ApprovedEvent(object sender, Livreur e)
        {
            ListLivreur.ItemsSource = null;
            ListLivreur.ItemsSource = creation.EmployeL;
        }
        public void Supprimer_Livreur_ApprovedEvent(object sender, Livreur e)
        {
            ListLivreur.ItemsSource = null;
            ListLivreur.ItemsSource = creation.EmployeL;
        }
        private void Nom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            nom = textbox.Text;
        }

        private void Prenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            prenom = textbox.Text;
        }

        private void Numero_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero = textbox.Text;
        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            string adresse = num_rue + "," + rue + "," + code_postale + "," + ville;
            int number;
            bool entree = Int32.TryParse(numero, out number);
            if ((nom != null) && (prenom != null) && (adresse != null) && (numero != null) && (transport != null)&&(entree))
            {
                Livreur cree = new Livreur(nom, prenom, false, adresse, numero, false, transport);
                MessageBox.Show(creation.AjouterLivreur(cree));

            }
            else
            {
                MessageBox.Show("Vous n'avez pas rempli tous les champs");
            }
        }

        private void Rechercher(object sender, RoutedEventArgs e)
        {

            List<Livreur> trouve = new List<Livreur>();
            if (nom != null)
            {
                trouve = creation.EmployeL.FindAll(x => x.Nom == nom);
            }
            if  (trouve.Count < 1)
            {
                trouve = creation.EmployeL.FindAll(x => x.Prenom == prenom);
            }
            if (trouve.Count < 1)
            {
                trouve = creation.EmployeL.FindAll(x => x.Numero == numero);
            }
            if (trouve.Count > 0)
            {
                MessageBox.Show(trouve[0].ToString());
                ListLivreur.SelectedItem = creation.EmployeL[creation.EmployeL.IndexOf(trouve[0])];
            }
            else
            {
                MessageBox.Show("ce livreur n'existe pas ");
            }
        }

        private void Supprimer(object sender, RoutedEventArgs e)
        {
            if (ListLivreur.SelectedItem != null)
            {
                Livreur suppr = creation.EmployeL.Find(x => x.ToString() == ListLivreur.SelectedItem.ToString());
                MessageBox.Show(creation.SupprimeLivreur(suppr));
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un Livreur");
            }
        }

        private void Transport_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            transport = textbox.Text;
        }

        private void Tri_Nom(object sender, RoutedEventArgs e)
        {
            creation.EmployeL.Sort();
            ListLivreur.ItemsSource = null;
            ListLivreur.ItemsSource = creation.EmployeL;
        }

        private void Tri_Ville(object sender, RoutedEventArgs e)
        {
            creation.EmployeL.Sort(Effectif.myCompareVille);
            ListLivreur.ItemsSource = null;
            ListLivreur.ItemsSource = creation.EmployeL;
        }

        private void Tri_nbLivraison(object sender, RoutedEventArgs e)
        {
            creation.EmployeL.Sort(Livreur.myCompareNbLivraison);
            ListLivreur.ItemsSource = null;
            ListLivreur.ItemsSource = creation.EmployeL;
        }

        private void NumRue_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            num_rue = textbox.Text;
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
            ville = textbox.Text;
        }

        private void Modifier_Livreur(object sender, RoutedEventArgs e)
        {
            string adresse = num_rue + "," + rue + "," + code_postale + "," + ville;
            if (ListLivreur.SelectedItem != null)
            {
                string choisi = ListLivreur.SelectedItem.ToString();
                Livreur recherche = creation.RechercheLivreur(ListLivreur.SelectedItem.ToString());
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
                if ((num_rue != "N°") || (rue != "Rue") || (code_postale != "Code Postale") || (ville != "Ville"))
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
                if (transport != "Transport")
                {
                    recherche.Numero = numero;
                    Transport.Text = "Numero";
                }
                ListLivreur.ItemsSource = null;
                ListLivreur.ItemsSource = creation.EmployeL;

            }
            else
            {
                MessageBox.Show("Veuillez selectionner un Livreur");
            }
        }
    }
}
