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
        int numero;
        int heure;
        DateTime date;
        string id_client;
        string nom_commis;
        string nom_livreur;
        string etat;
        string etat_solde;
        Facture referente;
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Commande non détaillés
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="heure"></param>
        /// <param name="date"></param>
        /// <param name="nom_client"></param>
        /// <param name="nom_commis"></param>
        /// <param name="nom_livreur"></param>
        /// <param name="etat"></param>
        /// <param name="solde"></param>
        public Commande(int numero, int heure, DateTime date, string nom_client, string nom_commis, string nom_livreur, string etat, string solde)
        {
            this.numero = numero;
            this.heure = heure;
            this.date = date;
            this.id_client = nom_client;
            this.nom_commis = nom_commis;
            this.nom_livreur = nom_livreur;
            this.etat = etat;
            this.etat_solde = solde;
        }

        /// <summary>
        /// Commande detaillé
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="heure"></param>
        /// <param name="date"></param>
        /// <param name="nom_client"></param>
        /// <param name="nom_commis"></param>
        /// <param name="nom_livreur"></param>
        /// <param name="etat"></param>
        /// <param name="solde"></param>
        /// <param name="items"></param>
        public Commande(int numero, int heure, DateTime date, string nom_client, string nom_commis, string nom_livreur, string etat, string solde, Facture items)
        {
            this.numero = numero;
            this.heure = heure;
            this.date = date;
            this.id_client = nom_client;
            this.nom_commis = nom_commis;
            this.nom_livreur = nom_livreur;
            this.etat = etat;
            this.etat_solde = solde;
            this.referente = items;
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
                OnPropertyChanged("Solde");
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
            }
        }
        #endregion

        public override string ToString()
        {
            return numero + " " + heure + " " + date + " " + id_client + " " + nom_commis + " " + nom_livreur + " " + etat + " " + etat_solde;
        }
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
