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
            }
        }
        #endregion 

        public override string ToString()
        {
            return base.ToString() + Convert.ToString(embauche);
        }
    }
}
