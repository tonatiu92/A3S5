using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Livreur : Effectif
    {
        #region attributs
        bool route;
        string transport;
        Commande traite;
        int nbLivraisons;
        #endregion

        /// <summary>
        /// Initialise un livreur
        /// </summary>
        /// <param nom du livreur="nom"></param>
        /// <param prénom du livreur="prenom"></param>
        /// <param en conge="etat_conge"></param>
        /// <param adresse="adresse"></param>
        /// <param numéro de téléphone="numero"></param>
        /// <param en route="route"></param>
        /// <param moyen de transport="transport"></param>
        /// <param commande traité="traite"></param>
        public Livreur(string nom, string prenom, bool etat_conge, string adresse, string numero, bool route, string transport, Commande traite = null) : base(nom, prenom, etat_conge, adresse, numero)
        {
            this.route = route;
            this.transport = transport;
            this.traite = traite;
            this.nbLivraisons = 0;
        }

        #region Propriété
        public bool Route
        {
            get
            {
                return route;
            }
            set
            {
                route = value;
                OnPropertyChanged("Route");
            }
        }

        public string Transport
        {
            get
            {
                return transport;
            }
            set
            {
                transport = value;
                OnPropertyChanged("Transport");
            }
        }

        public Commande Traite
        {
            get
            {
                return traite;
            }
            set
            {
                traite = value;
                OnPropertyChanged("Traite");
            }
        }

        public int NbLivraisons
        {
            get
            {
                return nbLivraisons;
            }
            set
            {
                nbLivraisons = value;
                OnPropertyChanged("NbLivraisons");
            }
        }
        #endregion

        /// <summary>
        /// Affiche un livreur
        /// </summary>
        /// <returns>l'affichage</returns>
        public override string ToString()
        {
            string affiche = Convert.ToString(traite);
            if((!route)&&(!etat_conge))
            {
                affiche = "Disponible";
            }
            return base.ToString() + route + " " + transport +  " " + affiche;
        }

        /// <summary>
        /// Compare les livreurs selon le nombre de livraisons effectués et respecte le delegate public delegate int Comparison<in T>(T x, T y);
        /// </summary>
        /// <param premier livreur="x"></param>
        /// <param deucieme livreur="y"></param>
        /// <returns></returns>
        public static int myCompareNbLivraison(Livreur x, Livreur y)
        {
            return x.NbLivraisons.CompareTo(y.NbLivraisons);
        }
    }
}
