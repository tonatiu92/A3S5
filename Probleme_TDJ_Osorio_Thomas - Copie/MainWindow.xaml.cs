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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace Probleme_TDJ_Osorio_Thomas
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MaPizzeria creation;
        string numero;
        string numero_commande;
        string numero_livreur;

        public MainWindow()
        {
            creation = new MaPizzeria("commis.csv", "livreur.csv", "commandes.csv", "clients.csv", "menu.csv");
            
            this.DataContext = this;

            InitializeComponent();
            Commande.ItemsSource = creation.Historique.Values;
            Refresh();
        }

        #region Gestion des Events
        private void Refresh()
        {
            creation.NouvelleCommandeAppproved += NouvelleCommande_ApprovedEvent;
            creation.NouveauClientApproved += Nouveau_Client_ApprovedEvent;
            creation.NouveauCommisApproved += Nouveau_Commis_ApprovedEvent;
            creation.SuppressionCommis += Supprimer_Commis_ApprovedEvent;
            creation.NouveauLivreurApproved += Nouveau_Livreur_ApprovedEvent;
            creation.SuppressionLivreur += Supprimer_Livreur_ApprovedEvent;
            creation.SuppressionClient += Supprimer_Client_ApprovedEvent;
        }

        private void NouvelleCommande_ApprovedEvent(object sender, Commande e)
        {
            creation.Historique.Add(e.Numero, e);
            Commande.ItemsSource = null;
            Commande.ItemsSource = creation.Historique.Values;
        }

        private void Nouveau_Client_ApprovedEvent(object sender, Client e)
        {
            creation.FichierClient.Add(e.Numero, e);
        }

        public void Nouveau_Commis_ApprovedEvent(object sender, Commis e)
        {
            creation.EmployeC.Add(e);
        }
        public void Supprimer_Commis_ApprovedEvent(object sender, Commis e)
        {
            creation.EmployeC.Remove(e);
        }
        public void Nouveau_Livreur_ApprovedEvent(object sender, Livreur e)
        {
            creation.EmployeL.Add(e);
        }
        public void Supprimer_Livreur_ApprovedEvent(object sender, Livreur e)
        {
            creation.EmployeL.Remove(e);
        }
        public void Supprimer_Client_ApprovedEvent(object sender, Client e)
        {
            creation.FichierClient.Remove(e.Numero);
        }

        #endregion

        #region Nouvelle Commande
        private void Button_Nouvelle_Commande(object sender, RoutedEventArgs e)
        {
            int number;
            bool entre = Int32.TryParse(numero,out number);
            int index = creation.FichierClient.IndexOfKey(numero);
            if ((index != -1)&&(entre))
            {
                if (MessageBox.Show("L'adresse est: " + creation.FichierClient.Values[index].Adresse, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    MessageBox.Show("Changer l'adresse du client dans le répertoire");
                }
                else
                {
                    NouvelleCommande traite = new NouvelleCommande(creation.FichierClient[numero], creation);
                    traite.Show();
                }
            }
            else if(entre)
            {
                NouveauClient fenetre = new NouveauClient(numero, creation);
                fenetre.Show();
            }
            else
            {
                MessageBox.Show("Veuillez rentrer un numéro valide", "Telephone", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
        private void Button_Afficher_Client(object sender, RoutedEventArgs e)
        {
            string[] lines = new string[creation.FichierClient.Values.Count]; ;
            int i = 0;
            foreach (Client element in creation.FichierClient.Values)
            {
                lines[i] = element.ToString();
                i++;
            }
            File.WriteAllLines("Affichage_Clients.txt", lines);
            Process.Start("Affichage_Clients.txt");
        }
        private void Button_Afficher_Commis(object sender, RoutedEventArgs e)
        {
            string[] lines = new string[creation.EmployeC.Count]; ;
            int i = 0;
            foreach (Commis element in creation.EmployeC)
            {
                lines[i] = element.ToString();
                i++;
            }
            File.WriteAllLines("Affichage_Commis.txt", lines);
            Process.Start("Affichage_Commis.txt");
        }
        private void Button_Afficher_Livreur(object sender, RoutedEventArgs e)
        {
            string[] lines = new string[creation.EmployeL.Count]; ;
            int i = 0;
            foreach (Livreur element in creation.EmployeL)
            {
                lines[i] = element.ToString();
                i++;
            }
            File.WriteAllLines("Affichage_Livreur.txt", lines);
            Process.Start("Affichage_Livreur.txt");
        }
        private void Pret_Click(object sender, RoutedEventArgs e)
        {
            if (Commande.SelectedItem != null)
            {
                string choisi = this.Commande.SelectedItem.ToString();
                creation.RechercheCommande(choisi).Etat = "prête pour livraison";
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une commande");
            }
        }
        private void Rechercher_Commande(object sender, RoutedEventArgs e)
        {
            int number;
            bool entree = Int32.TryParse(numero_commande, out number);
            if (entree)
            {
                MessageBox.Show(creation.Historique[number].ToString());
                Commande.SelectedItem = creation.Historique[number];
            }
            else
            {
                MessageBox.Show("Veuillez rentrer un numéro valide", "Numéro Commande", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Button_Livraison(object sender, RoutedEventArgs e)
        {
            string choisi = this.Commande.SelectedItem.ToString();
            Commande prete = creation.RechercheCommande(choisi);
            creation.EmployeL.Find(x => x.Nom == prete.NomLivreur).Route = true;
            prete.Etat = "en cours de livraison";
        }
        private void Button_paiement(object sender, RoutedEventArgs e)
        {
            int number_commande;
            int number_livraison;
            bool entree = Int32.TryParse(numero_commande, out number_commande);
            entree = Int32.TryParse(numero_livreur, out number_livraison);
            if (entree)
            {
                bool reponse;
                if(MessageBox.Show("Avez vous la facture avec vous?", "Paiement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    reponse = true;
                }
                else
                {
                    reponse = false;
                }
                MessageBox.Show(numero_livreur);
                MessageBox.Show(creation.Paiement(number_commande, numero_livreur, reponse));
                Commande.ItemsSource = null;
                Commande.ItemsSource = creation.Historique.Values;
            }
            else
            {
                MessageBox.Show("Entrée un numéro valide");
            }
        }
        private void F_Client(object sender, RoutedEventArgs e)
        {
            GestionClient page = new GestionClient(creation);
            page.Show();
        }
        private void F_Effectif(object sender, RoutedEventArgs e)
        {
            GestionEffectif fenetre = new GestionEffectif(creation);
            fenetre.Show();
        }

        #region TextBox
        private void Numero_Paiement(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero = textbox.Text;
        }
        private void Numero_Livreur(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero_livreur = textbox.Text;
        }
        private void Recherche_Commande(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero_commande = textbox.Text;
        }
        private void TextBox_TelCommande(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero = textbox.Text;
        }
        #endregion

        private void Afficher_Moyenne(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" La moyenne du solde des commandes est:" + Convert.ToString(creation.MoyenneCommande()), "Statitstiques", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Duree(object sender, RoutedEventArgs e)
        {
            if (Commande.SelectedItem != null)
            {
                string choisi = this.Commande.SelectedItem.ToString();
                Commande select = creation.RechercheCommande(choisi);
                MessageBox.Show("Le délai est de " + Convert.ToString(DateTime.Now - select.Date), "Délai", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une commande");
            }
        }

        private void Affiche_Commande(object sender, RoutedEventArgs e)
        {
            if (Commande.SelectedItem != null)
            {
                string choisi = this.Commande.SelectedItem.ToString();
                Commande select = creation.RechercheCommande(choisi);
                string[] lines = new string[select.Referente.ListeProduits.Count+2];
                lines[0] = "Facture Commande N° " + select.Numero +":" ;
                for(int i =0; i < select.Referente.ListeProduits.Count; i++)
                {
                    lines[i+1] = select.Referente.ListeProduits[i].ToString() + " " + select.Referente.ListeProduits[i].Prix +" Euros";
                }
                lines[lines.Length - 1] = "Prix Total:" + select.Referente.Solde;
                File.WriteAllLines(select.NomClient + ".txt", lines);
                Process.Start(select.NomClient + ".txt");

            }
            else
            {
                MessageBox.Show("Veuillez selectionner une commande");
            }
        }
    }
}
