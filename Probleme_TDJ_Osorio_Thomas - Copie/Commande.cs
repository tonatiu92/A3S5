using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Commande: INotifyPropertyChanged
    {
        #region attribut
        int numero;
        int heure;
        DateTime date;
        string id_client;
        string nom_commis;
        string nom_livreur;
        string etat;
        string etat_solde;
        Facture referente;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialise une commande par défault
        /// </summary>
        public Commande() { }
        
        /// <summary>
        /// Initialise une commande
        /// </summary>
        /// <param numero de commande="numero"></param>
        /// <param heure de la commande="heure"></param>
        /// <param date de la commande ="date"></param>
        /// <param nom du client="nom_client"></param>
        /// <param nom du commis="nom_commis"></param>
        /// <param nom livreur="nom_livreur"></param>
        /// <param etat de la commande="etat"></param>
        /// <param solde de la commande="solde"></param>
        /// <param facture delivré = "items"></param>
        public Commande(int numero, int heure, DateTime date, string nom_client, string nom_commis, string nom_livreur, string etat, string solde, Facture items = null)
        {
            this.numero = numero;
            this.heure = heure;
            this.date = date;
            this.id_client = nom_client;
            this.nom_commis = nom_commis;
            this.nom_livreur = nom_livreur;
            this.etat = etat;
            this.etat_solde = solde;
            if (items == null)
            {
                List<Produit> lp = new List<Produit>();
                this.referente = new Facture(lp);
            }
            else
            {
                this.referente = items;
            }
        }
       
        #region Propriété
        public int Numero
        {
            get
            {
                return numero;
            }
            set
            {
                numero = value;
                OnPropertyChanged("Numero");
            }
        }
        public int Heure
        {
            get
            {
                return heure;
            }
            set
            {
                heure = value;
                OnPropertyChanged("Heure");
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public string NomClient
        {
            get
            {
                return id_client;
            }
            set
            {
                id_client = value;
                OnPropertyChanged("NomClient");
            }
        }
        public string NomCommis
        {
            get
            {
                return nom_commis;
            }
            set
            {
                nom_commis = value;
                OnPropertyChanged("NomCommis");
            }
        }
        public string NomLivreur
        {
            get
            {
                return nom_livreur;
            }
            set
            {
                nom_livreur = value;
                OnPropertyChanged("NomLivreur");
            }
        }
        public string Etat
        {
            get
            {
                return etat;
            }
            set
            {
                etat = value;
                OnPropertyChanged("Etat");
            }
        }
        public string EtatSolde
        {
            get
            {
                return etat_solde;
            }
            set
            {
                etat_solde = value;
                OnPropertyChanged("EtatSolde");
            }
        }
        public Facture Referente
        {
            get
            {
                return referente;
            }
            set
            {
                referente = value;
                OnPropertyChanged("Referente");
            }
        }
        #endregion

        /// <summary>
        /// Affichage de la commande
        /// </summary>
        /// <returns>l'affichage</returns>
        public override string ToString()
        {
            return numero + " " + heure + " " + date + " " + id_client + " " + nom_commis + " " + nom_livreur + " " + etat + " " + etat_solde;
        }

        /// <summary>
        /// Implémentation de INotifyPropertyChanged
        /// </summary>
        /// <param nom de la propriété modifiée="name"></param>
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
