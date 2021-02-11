using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osorio_Thomas_TDJ_Probleme
{
    public class Coordonees
    {

        int colonne;
        int ligne;
        int facon;

        public Coordonees(int x, int y, int h)
        {
            this.colonne = x;
            this.ligne = y;
            this.facon = h;
        }

        public int Colonne
        {
            get
            {
                return colonne;
            }
        }
        public int Ligne
        {
            get
            {
                return ligne;
            }
        }
        public int Facon
        {
            get
            {
                return facon;
            }
        }

    }
    /// permet d'initialiser plus rapidement les coordonées des lettres et de les manipuler si besoin pour la version 1;
}
