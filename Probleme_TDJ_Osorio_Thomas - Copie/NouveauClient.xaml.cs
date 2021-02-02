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
        MaPizzeria creation;
        string numero;
        string nom;
        string prenom;
        string numero_rue;
        string rue;
        string code_postale;
        string ville;
        public NouveauClient(string numero, MaPizzeria transmit)
        {
            InitializeComponent();
            this.numero = numero;
            this.creation = transmit;
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

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            string adresse = numero_rue + "," + rue + "," + code_postale + "," + ville;
            MessageBox.Show(adresse);
            MessageBox.Show(creation.NouveauClient(nom, prenom, adresse, numero));
            NouvelleCommande traite = new NouvelleCommande(creation.FichierClient[numero], creation);
            traite.Show();
            this.Close();
        }

        private void TextBox_Numero_Rue(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero_rue = textbox.Text;
        }

        private void TextBox_Rue(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            rue = textbox.Text;
        }

        private void TextBox_Ville(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            code_postale = textbox.Text;
        }

        private void TextBox_CP(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            ville = textbox.Text;
        }
    }
}
