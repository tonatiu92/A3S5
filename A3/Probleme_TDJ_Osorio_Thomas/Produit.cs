using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public  class Produit: INotifyPropertyChanged, IComparable
    {
        #region attributs
        float prix;
        int quantite;
        int taille;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Delegate pour calculer le pix d'un produit
        /// </summary>
        /// <param valeur du produit="val"></param>
        /// <param taille du produit="taille"></param>
        /// <returns></returns>
        public delegate float  CalculDuPrix(float val, int taille);

        /// <summary>
        /// Initialise un produit
        /// </summary>
        /// <param nom du produit="nom"></param>
        /// <param prix du produit="prix"></param>
        /// <param quantité du produit="quantite"></param>
        /// <param taille du produit="taille"></param>
        public Produit( float prix, int quantite = 1, int taille = 1)
        {
            this.prix = prix;
            this.quantite = quantite;
            this.taille = taille;
        }

        /// <summary>
        /// Constructeur de copie d'un produit
        /// </summary>
        /// <param produit copié="copie"></param>
        public Produit(Produit copie)
        {
            this.prix = copie.prix;
            this.quantite = copie.quantite;
            this.taille = copie.taille;
        }

        #region Propriété

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

        /// <summary>
        /// Affichage d'un produit
        /// </summary>
        /// <returns>l'affichage</returns>
        public override string ToString()
        {
            return  prix/quantite + " unitaire   x" + quantite + " " + taille;
        }

        /// <summary>
        /// Implémentation de INotifyPropertyChanged
        /// </summary>
        /// <param nom de la propriété="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Calcul le prix final d'un produit
        /// </summary>
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

        /// <summary>
        /// Implémenation de IComparable
        /// </summary>
        /// <param objet à comparer="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Produit val = (Produit)obj;
            return this.Quantite.CompareTo(val.Quantite);
        }

    }
}
