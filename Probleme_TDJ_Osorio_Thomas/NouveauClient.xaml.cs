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
    /// Logique d'interaction pour NouveauClient.xaml
    /// </summary>
    public partial class NouveauClient : Window
    {
        string numero;
        string nom;
        string prenom;
        string adresse;
        public NouveauClient(string numero)
        {
            InitializeComponent();
            this.numero = numero;
        }

        private void TextBox_Nom(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            nom = textbox.Text;
        }

        private void TextBox_Prenom(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            prenom = textbox.Text;
        }

        private void TextBox_Adresse(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            adresse = textbox.Text;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            int last = MainWindow.creation.FichierClient.Values[MainWindow.creation.FichierClient.Count - 1].Id;
            Client cree = new Client(last + 1, nom, prenom, adresse, numero, DateTime.Now);
            MainWindow.creation.FichierClient.Add(numero, cree);
            NouvelleCommande traite = new NouvelleCommande(cree);
            traite.Show();
            this.Close();
        }
    }
}
