using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Commis : Effectif
    {
        DateTime embauche;
        int nbCommandes;


        /// <summary>
        /// Constructeur basique d'un commis
        /// </summary>
        /// <param nom ="nom"></param>
        /// <param prenom ="prenom"></param>
        /// <param en conge ="etat_conge"></param>
        /// <param adresse ="adresse"></param>
        /// <param date d'embauche ="embauche"></param>
        public Commis(string nom, string prenom, bool etat_conge, string adresse, string numero, DateTime embauche) : base(nom, prenom, etat_conge, adresse, numero)
        {
            this.embauche = embauche;
            this.nbCommandes = 0;
        }

        #region Propriété
        public DateTime Embauche
        {
            get
            {
                return embauche;
            }
            set
            {
                embauche = value;
                OnPropertyChanged("Embauche");
            }
        }
        public int NbCommandes
        {
            get
            {
                return nbCommandes;
            }
            set
            {
                nbCommandes = value;
                OnPropertyChanged("NbCommandes");
            }
        }
        #endregion 

        /// <summary>
        /// Compare les commis selon le nombre de commandes effectués et respecte le delegate public delegate int Comparison<in T>(T x, T y);
        /// </summary>
        /// <param premier commis="x"></param>
        /// <param second commis="y"></param>
        /// <returns></returns>
        public static int myCompareNbCommandes(Commis x, Commis y)
        {
            return x.NbCommandes.CompareTo(y.NbCommandes);
        }

        /// <summary>
        /// Affichage d'un commis
        /// </summary>
        /// <returns>l'affichage</returns>
        public override string ToString()
        {
            return base.ToString() + Convert.ToString(embauche);
        }
    }
}
