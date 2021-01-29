using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Boisson : Produit, INotifyPropertyChanged
    {
        string type;

        public Boisson(string nom, float prix, string type, int quantite = 1, int taille = 33) : base(nom, prix, quantite, taille)
        {
            this.type = type;
        }

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

        public override string ToString()
        {
            return base.ToString() + " " + Type;
        }

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
