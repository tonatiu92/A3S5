using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_TDJ_Osorio_Thomas
{
    class Viande: Produit
    {
        string nom;

        public Viande(float prix, string nom, int quantite = 1, int taille = 1) : base(prix, quantite, taille)
        {
            this.nom = nom;
        }
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
                OnPropertyChanged("Nom");
            }
        }
    }
}
