using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Probleme_TDJ_Osorio_Thomas
{
    public abstract class Effectif : ICoordonnees, INotifyPropertyChanged
    {
        string nom;
        string prenom;
        protected bool etat_conge;
        string adresse;
        string numero;
        public event PropertyChangedEventHandler PropertyChanged;

        public Effectif(string nom, string prenom, bool etat_conge, string adresse, string numero)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.etat_conge = etat_conge;
            this.adresse = adresse;
            this.numero = numero;
        }

        #region Propriétés
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

        public string Prenom
        {
            get
            {
                return prenom;
            }
            set
            {
                prenom = value;
                OnPropertyChanged("Prenom");
            }
        }

        public bool Etat_Conge
        {
            get
            {
                return etat_conge;
            }
            set
            {
                etat_conge = value;
                OnPropertyChanged("EtatConge");
            }
        }

        public string Adresse
        {
            get
            {
                return adresse;
            }
            set
            {
                adresse = value;
                OnPropertyChanged("Adresse");
            }
        }
        public string Numero
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
        #endregion

        public override string ToString()
        {
            return nom + " " + prenom + " " + etat_conge + " " + adresse + " " + numero + " ";
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
