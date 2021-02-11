using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace Probleme_TDJ_Osorio_Thomas
{
    public abstract class Effectif : ICoordonnees, INotifyPropertyChanged, IComparable
    {
        #region attributs
        string nom;
        string prenom;
        protected bool etat_conge;
        string adresse;
        string numero;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initialise un objet de type effectif
        /// </summary>
        /// <param nom de la personne="nom"></param>
        /// <param prénom de la personne="prenom"></param>
        /// <param en congé="etat_conge"></param>
        /// <param adresse="adresse"></param>
        /// <param numéro de téléphone="numero"></param>
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
                OnPropertyChanged("Etat_Conge");
               
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
        
        public string TrueString { get; set; } = "true";
        #endregion

        /// <summary>
        /// Affichage d'un objet de type effectif
        /// </summary>
        /// <returns>l'affichage</returns>
        public override string ToString()
        {
            return nom + " " + prenom + " " + etat_conge + " " + adresse + " " + numero + " ";
        }

        /// <summary>
        /// Implémentation de la délégations de INotifyPorpertyChanged
        /// </summary>
        /// <param nom de la propriété="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler( this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Renvoie la ville de domicile de la personne
        /// </summary>
        /// <returns>nom de ville</returns>
        public string Ville()
        {
            string[] decompose = adresse.Split(',');
            return decompose[3];
        }

        /// <summary>
        /// Implémentation de la méthode IComparable
        /// </summary>
        /// <param l'objet à comparer="obj"></param>
        /// <returns>la valeur de la comparaison</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            Effectif val = obj as Effectif;
            if (val != null)
                return this.Nom.CompareTo(val.Nom);
            else
                throw new ArgumentException("Object n'est pas un effectif");
        }

        /// <summary>
        /// Méthode de comparaison selon la ville de domicile respecant la délégation public delegate int Comparison<in T>(T x, T y);
        /// </summary>
        /// <param premiere personne="x"></param>
        /// <param seconde personne="y"></param>
        /// <returns>la valeur de comparaison</returns>
        public static int myCompareVille(Effectif x, Effectif y)
        {
            return y.Ville().CompareTo(x.Ville());
        }

    }
}
