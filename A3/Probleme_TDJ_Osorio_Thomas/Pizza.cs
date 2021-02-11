using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Pizza : Produit
    {
        string type;

        /// <summary>
        /// Initialise une pizza
        /// </summary>
        /// <param type de produit="nom"></param>
        /// <param prix de la pizza="prix"></param>
        /// <param type de pizza="type"></param>
        /// <param quantité de cette pizza="quantite"></param>
        /// <param taille de la pizza="taille"></param>
        public Pizza( float prix, string type, int quantite = 1, int taille = 1) : base(prix, quantite, taille)
        {
            this.type = type;
        }

        /// <summary>
        /// Constructeur de copie d"une pizza
        /// </summary>
        /// <param produit copié="copie1"></param>
        /// <param pizza copié="a"></param>
        public Pizza(Produit copie1, Pizza a) : base(copie1)
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
        /// Affichage d'une pizza
        /// </summary>
        /// <returns>l'affichage</returns>
        public override string ToString()
        {
            return base.ToString() + " " + type;
        }

        /// <summary>
        /// calcul le prix d'une pizza en respectant la délégation  public delegate float  CalculDuPrix(float val, int taille);
        /// </summary>
        /// <param name="val"></param>
        /// <param name="taille"></param>
        /// <returns></returns>
        public static float PrixPizza(float val, int taille)
        {
            float valfinal = 0f;
            switch (taille)
            {
                case 1:
                    valfinal = val;
                    break;
                case 2:
                    valfinal = val + val/10;
                    break;
                case 3:
                    valfinal = val + val / 2;
                    break;

            }
            return valfinal;
        }
    }

}
