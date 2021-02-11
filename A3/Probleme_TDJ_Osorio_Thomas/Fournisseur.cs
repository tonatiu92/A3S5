using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Fournisseur : INotifyPropertyChanged, IComparable
    {
        string nom;
        List<Produit> vendu;
        string type_produit;
        float achats;
        public event PropertyChangedEventHandler PropertyChanged;

        public Fournisseur(string nom, List<Produit> vendu, string type)
        {
            this.nom = nom;
            this.vendu = new List<Produit>(vendu);
            this.achats = 0;
            type_produit = type;
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
        public string TypeProduit
        {
            get
            {
                return type_produit;
            }
            set
            {
                type_produit = value;
                OnPropertyChanged("TypeProduit");
            }
        }
        public List<Produit> Vendu
        {
            get
            {
                return vendu;
            }
            set
            {
                vendu = value;
            }
        }

        public float Achats
        {
            get
            {
                return achats;
            }
            set
            {
                achats = value;
                OnPropertyChanged("Achats");
            }
        }
        #endregion      
        
        /// <summary>
        /// Implémentation de CompareTo et de l'interface IComparable
        /// </summary>
        /// <param objet a comparer="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Fournisseur val = (Fournisseur)obj;
            return this.achats.CompareTo(val.Achats);
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
        /// Méthodes qui affiche un fournisseur
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nom + " " + type_produit + " " + achats;
        }
    }
}
