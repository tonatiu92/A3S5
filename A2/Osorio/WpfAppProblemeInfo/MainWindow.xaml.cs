using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppProblemeInfo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string text;
        double CoeffAgg;
        string CoeffAggWrite;
        MyImage select;
        string dissimule;
        double coeff;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void QRcode_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Générer votre QRcode");
            QrCodeWPF ouverte = new QrCodeWPF();
            ouverte.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(text + " a été crée");
            select = new MyImage(text);
            Process.Start(text);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.text = textBox.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyImage fractale = new MyImage(1000, 1000);
            fractale.From_Image_To_File("fractale.bmp");
            Process.Start("fractale.bmp");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            select.Color_To_Black("NB.bmp");
            Process.Start("NB.bmp");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.CoeffAgg = Convert.ToDouble(CoeffAggWrite);
            double decimales = CoeffAgg - (int)CoeffAgg;
            if (decimales == 0.0)
            {
                select.Agrandir(Convert.ToInt32(CoeffAgg));
                select.From_Image_To_File("Agrandissement.bmp");
                Process.Start("Agrandissement.bmp");
            }
            else
            {
                CoeffAgg *= 2;
                select.Agrandir(Convert.ToInt32(CoeffAgg));
                select.Retrecir(2);
                select.From_Image_To_File("Agrandissement.bmp");
                Process.Start("Agrandissement.bmp");
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.CoeffAggWrite = textBox.Text;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            select.Color_To_Grey("Gris.bmp");
            Process.Start("Gris.bmp");
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.CoeffAggWrite = textBox.Text;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.CoeffAgg = Convert.ToDouble(CoeffAggWrite);
            double decimales = CoeffAgg - (int)CoeffAgg;
            if (decimales == 0.0)
            {
                select.Retrecir(Convert.ToInt32(CoeffAgg));
                select.From_Image_To_File("Agrandissement.bmp");
                Process.Start("Agrandissement.bmp");
            }
            else
            {
                CoeffAgg *= 2;
                select.Retrecir(Convert.ToInt32(CoeffAgg));
                select.Agrandir(2);
                select.From_Image_To_File("Agrandissement.bmp");
                Process.Start("Agrandissement.bmp");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            select.detection_contour(3);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            select.detection_contour(5);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            select.Flou(3);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            select.Flou(5);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            select.Repoussage();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            select.renforcement_bords();
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.dissimule = textBox.Text;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            select = new MyImage(text);
            MyImage cache = new MyImage(dissimule);
            select.dissimuler(cache, "melange.bmp", 0, 0);
            Process.Start("melange.bmp");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            select.decode_im("décodage.bmp");
            MessageBox.Show("Rechargez une image ");
        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {
            TextBox textBox1 = sender as TextBox;
            this.coeff = Convert.ToDouble(textBox1.Text);
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            select.Rotation(coeff);
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            MyImage Histo = new MyImage(select);
            Histo.From_Image_To_File("Histogramme.bmp");
            Process.Start("Histogramme.bmp");
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            select.effet_miroir_V("miroir_veritcal.bmp");
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            select.effet_miroir_H("miroir_horizontal.bmp");
        }

        private void TextBox_TextChanged_5(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.text = textBox.Text;
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Innovation huffmann");
            Innovation ouverte = new Innovation();
            ouverte.Show();
        }
    }
}
