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
    /// Logique d'interaction pour GestionEffectif.xaml
    /// </summary>
    public partial class GestionEffectif : Window
    {
        MaPizzeria creation;
        public GestionEffectif(MaPizzeria creation)
        {
            InitializeComponent();
            this.creation = creation;
        }

        private void Page_Commis(object sender, RoutedEventArgs e)
        {
            Main.Content = new GestionCommis(creation);
        }

        private void Page_Livreur(object sender, RoutedEventArgs e)
        {
            Main.Content = new GestionLivreur(creation);
        }
    }
}
