using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Client : ICoordonnees
    {
        string nom;
        string prenom;
        string adresse;
        string numero;
        int id;
        DateTime premiere;

        public Client(int id, string nom, string prenom, string adresse, string numero, DateTime premiere)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.numero = numero;
            this.premiere = premiere;
        }

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
            }
        }
        #endregion

        public override string ToString()
        {
            return id + " " + nom + " " + prenom + " " + adresse + " " + premiere;
        }
    }
}
