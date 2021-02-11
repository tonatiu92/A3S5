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

        #region TextBox

        /// <summary>
        /// TextBox nom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Nom(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            nom = textbox.Text;
        }

        /// <summary>
        /// TextBox du prénom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Prenom(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            prenom = textbox.Text;
        }

        /// <summary>
        /// TextBox du numéto de rue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Numero_Rue(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            numero_rue = textbox.Text;
        }


        /// <summary>
        /// TextBox du nom de la rue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Rue(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            rue = textbox.Text;
        }

        /// <summary>
        /// TextBox du nom de la ville
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Ville(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            code_postale = textbox.Text;
        }

        /// <summary>
        /// TextBox du code postale.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_CP(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            ville = textbox.Text;
        }
        #endregion

        /// <summary>
        /// Button pour enregistrer dans le fichier client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            string adresse = numero_rue + "," + rue + "," + code_postale + "," + ville;
            MessageBox.Show(creation.NouveauClient(nom, prenom, adresse, numero));
            NouvelleCommande traite = new NouvelleCommande(creation.FichierClient[numero], creation);
            traite.Show();
            this.Close();
        }
    }
}
