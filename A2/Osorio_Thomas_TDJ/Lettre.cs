using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osorio_Thomas_TDJ_Probleme
{
    public class Lettre
    {
        //ATTRIBUTS
        char symbole;
        int poids;
        int frequence;
       
        //CONSTRUCTEURS
        public Lettre(char symbole, int poids, int frequence)
        {
            this.symbole = symbole;
            this.poids = poids;
            this.frequence = frequence;
        }

        public Lettre(char symbole, int poids)
        {
            this.symbole = symbole;
            this.poids = poids;
            this.frequence = 1;
        }

        //Propriété

        public char Symbole
        {
            get
            {
                return symbole;
            }
        }

        public int Poids
        {
            get
            {
                return poids;
            }
        }

        public int Frequence
        {
            get
            {
                return frequence;
            }
            set
            {
                frequence = value;
            }
        }

        /// <summary>
        /// Regarde si il possède le même symbole
        /// </summary>
        /// <param Symbole compare="SymboleCompare"></param>
        /// <returns>renvoie si oui ou non le symbol existe</returns>
        public bool Equal_Symbol(char SymboleCompare)
        {
            bool equal = false;
            if((SymboleCompare == this.symbole)&&(this.frequence!=0))
            {
                equal = true;
            }
            return equal;
        }
    }
}
