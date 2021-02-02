using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Facture: INotifyPropertyChanged
    {
        List<Produit> liste_produit;
        float solde;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialisation d'une facture
        /// </summary>
        /// <param liste des produits="lp"></param>
        /// <param valeurs de la facture="solde"></param>
        public Facture(List<Produit> lp, float solde = 0f)
        {
            liste_produit = lp;
            this.solde = CalculSolde();
        }

        /// <summary>
        /// Constructeur par défault d'une facture
        /// </summary>
        public Facture() { }

        #region Propriété
        public List<Produit> ListeProduits
        {
            get
            {
                return liste_produit;
            }
            set
            {
                liste_produit = value;
            }
        }
        public float Solde
        {
            get
            {
                return solde;
            }
            set
            {
                solde = value;
                OnPropertyChanged("Solde");
            }

        }
        #endregion

        /// <summary>
        /// Calcul le solde total de la facture
        /// </summary>
        /// <returns>le solde</returns>
        public float CalculSolde()
        {
            float somme = 0f;
            liste_produit.ForEach(x => somme += x.Prix);
            return somme;

        }

        /// <summary>
        /// Implémentation de l'interface INotifyPropertyChanged
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
    }
}
