using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Facture
    {
        List<Produit> liste_produit;
        float solde;

        public Facture(List<Produit> lp, float solde = 0f)
        {
            liste_produit = lp;
            this.solde = CalculSolde();
        }
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
            }

        }

        public float CalculSolde()
        {
            float somme = 0f;
            liste_produit.ForEach(x => somme += x.Prix*(float)x.Quantite);
            return somme;

        }
        #endregion
    }
}
