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
using System.IO;
using System.Diagnostics;

namespace Probleme_TDJ_Osorio_Thomas
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public MaPizzeria creation;
        string numero;

        public MainWindow()
        {
            creation = new MaPizzeria("commis.csv", "livreur.csv", "commandes.csv", "clients.csv", "menu.csv");
            
            this.DataContext = this;

            InitializeComponent();
            Commande.ItemsSource = creation.Historique.Values;
            Refresh();
        }

        private void Refresh()
        {
            creation.NouvelleCommandeAppproved += NouvelleCommande_ApprovedEvent;
        }


        private void NouvelleCommande_ApprovedEvent(object sender, Commande e)
        {
            Commande.ItemsSource = null;
            Commande.ItemsSource = creation.Historique.Values;
        }

        private void Button_Nouvelle_Commande(object sender, RoutedEventArgs e)
        {
            int index = creation.FichierClient.IndexOfKey(numero);
            if (index != -1)
            {
                if (MessageBox.Show("L'adresse est: " + creation.FichierClient.Values[index].Adresse, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    MessageBox.Show("Changer l'adresse du client dans le répertoire");
                }
                else
                {
                    NouvelleCommande traite = new NouvelleCommande(creation.FichierClient[numero]);
                    traite.Show();
                }
            }
            else
            {
                NouveauClient fenetre = new NouveauClient(numero);
                fenetre.Show();
            }
        }

        private void TextBox_TelCommande(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero = textbox.Text;
        }

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

        private void Commande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Pret_Click(object sender, RoutedEventArgs e)
        {
            string choisi  = this.Commande.SelectedItem.ToString();
        }
    }
}
