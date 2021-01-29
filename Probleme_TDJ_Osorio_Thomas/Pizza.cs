using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Pizza : Produit, INotifyPropertyChanged
    {
        string type;

        public Pizza(string nom, float prix, string type, int quantite = 1, int taille = 1) : base(nom, prix, quantite, taille)
        {
            this.type = type;
        }
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

        public override string ToString()
        {
            return base.ToString() + " " + type;
        }

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
