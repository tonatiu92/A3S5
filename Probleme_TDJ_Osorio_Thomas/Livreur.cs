using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_TDJ_Osorio_Thomas
{
    public class Livreur : Effectif
    {
        bool route;
        string transport;
        Commande traite;

        public Livreur(string nom, string prenom, bool etat_conge, string adresse, string numero, bool route, string transport, Commande traite = null) : base(nom, prenom, etat_conge, adresse, numero)
        {
            this.route = route;
            this.transport = transport;
            this.traite = traite;
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
            }
        }
        #endregion

        public override string ToString()
        {
            string affiche = Convert.ToString(traite);
            if((traite == null)&&(!etat_conge))
            {
                affiche = "Disponible";
            }
            return base.ToString() + route + " " + transport +  " " + affiche;
        }
    }
}
