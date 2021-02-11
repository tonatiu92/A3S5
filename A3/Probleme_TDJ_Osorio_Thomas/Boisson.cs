using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Boisson : Produit
    {
        string type;

        /// <summary>
        /// Initialise la boisson
        /// </summary>
        /// <param type du produit="nom"></param>
        /// <param le prix="prix"></param>
        /// <param type de boisson ="type"></param>
        /// <param quantité des produits="quantite"></param>
        /// <param taille de la boisson="taille"></param>
        public Boisson( float prix, string type, int quantite = 1, int taille = 33) : base(prix, quantite, taille)
        {
            this.type = type;
        }

        /// <summary>
        /// Constructeur de copie
        /// </summary>
        /// <param le produit copié="copie1"></param>
        /// <param la boisson copié="a"></param>
        public Boisson(Produit copie1, Boisson a) : base(copie1)
        {
            this.type = a.Type;
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged("Type");

            }
        }

        /// <summary>
        /// Affiche la boisson
        /// </summary>
        /// <returns>l'affichage</returns>
        public override string ToString()
        {
            return base.ToString() + " " + Type;
        }

        /// <summary>
        /// Calcul le prix d'une boisson respectant la délégation public delegate float  CalculDuPrix(float val, int taille);
        /// </summary>
        /// <param la valeur initial="val"></param>
        /// <param la taille de la boisson="taille"></param>
        /// <returns></returns>
        public static float PrixBoisson(float val, int taille)
        {
            float valfinal = 0f;
            switch (taille)
            {
                case 33:
                    valfinal = val;
                    break;
                case 100:
                    valfinal = val + val/5;
                    break;

            }
            return valfinal;
        }



    }
}
