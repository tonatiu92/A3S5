using System.Collections.Generic;

namespace WpfAppProblemeInfo
{
    public class Symbole
    {
        char symbol;
        int frequence;
        Symbole droite;
        Symbole gauche;
        public char Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
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
        public Symbole Droite
        {
            get
            {
                return droite;
            }
            set
            {
                droite = value;
            }
        }
        public Symbole Gauche
        {
            get
            {
                return gauche;
            }
            set
            {
                gauche = value;
            }
        }

        /// <summary>
        /// Constructeur créant un symbole
        /// </summary>
        /// <param caractere="lu"></param>
        public Symbole(char lu)
        {
            Symbol = lu;
            frequence = 1;
        }
        /// <summary>
        /// Constructeur de copie
        /// </summary>
        /// <param Symbole copie="copie"></param>
        public Symbole(Symbole copie)
        {
            Symbol = copie.Symbol;
            frequence = copie.Frequence;
            if (copie.Droite != null)
            {
                droite = copie.Droite;
            }
            if (copie.Gauche != null)
            {
                gauche = copie.Gauche;
            }
        }
        /// <summary>
        /// branche d'un arbre de Huffman
        /// </summary>
        /// <param symbole="symbol"></param>
        /// <param code binaire="code"></param>
        /// <returns>code binaire</returns>
        public List<bool> Branche(char symbol, List<bool> code)
        {

            if (Droite == null && Gauche == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    return code;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> G = null;
                List<bool> D = null;
                if (Droite != null)
                {
                    List<bool> BrancheDroite = new List<bool>();
                    BrancheDroite.AddRange(code);
                    BrancheDroite.Add(true);
                    D = Droite.Branche(symbol, BrancheDroite);
                }
                if (Gauche != null)
                {
                    List<bool> BrancheGauche = new List<bool>();
                    BrancheGauche.AddRange(code);
                    BrancheGauche.Add(false);

                    G = Gauche.Branche(symbol, BrancheGauche);
                }

                if (G != null)
                {
                    return G;
                }
                else
                {
                    return D;
                }
            }
        }
    }
}