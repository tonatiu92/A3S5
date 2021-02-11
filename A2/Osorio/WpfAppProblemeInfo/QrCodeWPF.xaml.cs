using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppProblemeInfo
{
    /// <summary>
    /// Logique d'interaction pour QrCodeWPF.xaml
    /// </summary>
    public partial class QrCodeWPF : Window
    {
        string text;
        string fichierQR;
        MyImage lu;
        public QrCodeWPF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyImage QR = new MyImage(text.ToUpper(), 'L');
            QR.From_Image_To_File("Monqrcode.bmp");
            MessageBox.Show("QrCode crée! " + text.ToUpper());
            Process.Start("Monqrcode.bmp");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.text = textBox.Text;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.fichierQR = textBox.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lu = new MyImage(fichierQR);
            QRcode Lecture = new QRcode(lu);
            MessageBox.Show("Le message est: " + Lecture.To_String());

        }
    }
}
