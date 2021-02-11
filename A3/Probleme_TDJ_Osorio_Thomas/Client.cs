using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; 

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Client : ICoordonnees,INotifyPropertyChanged, IComparable
    {
        #region attribut
        string nom;
        string prenom;
        string adresse;
        string numero;
        int id;
        DateTime premiere;
        float depense;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;


        #region Constructeurs
        /// <summary>
        /// Constructeur par défault
        /// </summary>
        public Client() { }

        /// <summary>
        /// Initialise un client
        /// </summary>
        /// <param numéro client="id"></param>
        /// <param nom="nom"></param>
        /// <param prénom ="prenom"></param>
        /// <param adresse ="adresse"></param>
        /// <param numéro de téléphone="numero"></param>
        /// <param date de la première commande="premiere"></param>
        public Client(int id, string nom, string prenom, string adresse, string numero, DateTime premiere)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.numero = numero;
            this.premiere = premiere;
            depense = 0f;
        }
        #endregion

        #region Propriété
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
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
        public DateTime Premiere
        {
            get
            {
                return premiere;
            }
            set
            {
                premiere = value;
                OnPropertyChanged("Pemiere");
            }
        }
        public float Depense
        {
            get
            {
                return depense;
            }
            set
            {
                depense = value;
                OnPropertyChanged("Depense"); 
            }
        }
        #endregion


        /// <summary>
        /// Affichage d'un client
        /// </summary>
        /// <returns>l'affiche</returns>
        public override string ToString()
        {
            return id + " " + nom + " " + prenom + " " + adresse + " " + numero + " " + premiere + " " + depense;
        }

        /// <summary>
        /// Implémentation de la méthode CompareTo
        /// </summary>
        /// <param l'objet compare ="obj"></param>
        /// <returns>négatif si this inférieur et positif si supérieur</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Client val = obj as Client;
            if (val != null)
            {
                return this.depense.CompareTo(val.depense);
            }
            else
            {
                throw new ArgumentException("Object is not a Client");
            }
        }

        /// <summary>
        /// Méthode de comparaison selon le nom qui respecte le delegate public delegate int Comparison<in T>(T x, T y);
        /// </summary>
        /// <param premier client="x"></param>
        /// <param seconde client="y"></param>
        /// <returns></returns>
        public static int myCompare(Client x, Client y)
        {
            return y.Nom.CompareTo(x.Nom);
        }

        /// <summary>
        /// Retourne la ville de domicile du client
        /// </summary>
        /// <returns>la ville</returns>
        public string Ville()
        {
            string[] decompose = adresse.Split(',');
            return decompose[3];
        }

        /// <summary>
        /// Méthode de comparaison selon la ville qui respecte le delegate public delegate int Comparison<in T>(T x, T y);
        /// </summary>
        /// <param premier client ="x"></param>
        /// <param second client ="y"></param>
        /// <returns></returns>
        public static int myCompareVille(Client x, Client y)
        {
            return y.Ville().CompareTo(x.Ville());
        }

        /// <summary>
        /// Event de changement de propriété
        /// </summary>
        /// <param nom ="name"></param>
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
