using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppProblemeInfo
{
    /// <summary>
    /// Logique d'interaction pour Innovation.xaml
    /// </summary>
    public partial class Innovation : Window
    {
        string texte;
        public Innovation()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.texte = textBox.Text;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Huffman codage = new Huffman(texte);
            List<bool> encoded = codage.Codage(texte);
            string affiche = "";
            for (int i = 0; i < encoded.Count; i++)
            {
                if (encoded[i])
                {
                    affiche += "1";
                }
                else
                {
                    affiche += "0";
                }
            }
            MessageBox.Show("le code pour " + texte + " est: " + affiche);
        }
    }
}
