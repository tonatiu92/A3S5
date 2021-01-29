using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public abstract class Produit: INotifyPropertyChanged
    {
        string nom;
        float prix;
        int quantite;
        int taille;
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate float  CalculDuPrix(float val, int taille);

        public Produit(string nom, float prix, int quantite = 1, int taille = 1)
        {
            this.nom = nom;
            this.prix = prix;
            this.quantite = quantite;
            this.taille = taille;
        }

        public Produit(Produit copie)
        {
            this.nom = copie.Nom;
            this.prix = copie.prix;
            this.quantite = copie.quantite;
            this.taille = copie.taille;
        }

        #region Propriété
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

        public float Prix
        {
            get
            {
                return prix;
            }
            set
            {
                prix = value;
                OnPropertyChanged("Prix");
            }
        }

        public int Quantite
        {
            get
            {
                return quantite;
            }
            set
            {
                quantite = value;
                OnPropertyChanged("Quantite");
            }
        }
        public int Taille
        {
            get
            {
                return taille;
            }
            set
            {
                taille = value;
                OnPropertyChanged("Taille");
            }
        }
        #endregion

        public override string ToString()
        {
            return nom + " " + prix + " " + quantite + " " + taille;
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void CalculPrix()
        {
            if(this is Boisson)
            {
                CalculDuPrix def = Boisson.PrixBoisson;
                prix = def(prix, taille);

            }
            else
            {
                CalculDuPrix def = Pizza.PrixPizza;
                prix =  def(prix, taille);
            }
            
        }

    }
}
