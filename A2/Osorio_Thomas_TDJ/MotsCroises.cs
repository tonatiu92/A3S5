using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osorio_Thomas_TDJ_Probleme
{
    public class MotsCroises
    {
        char[,] grille;
        List<string> mots;
        bool empty;

        const int TAILLE_MAX = 10;
        const string MESSAGE_ERREUR = "pas possible";
        

        

        public MotsCroises()
        {
            grille = new char[TAILLE_MAX, TAILLE_MAX];
            for (int i = 0; i < TAILLE_MAX; i++)
            {
                for (int j = 0; j < TAILLE_MAX; j++)
                {
                    grille[i, j] = ' ';
                }

            }
            empty = true;
            mots = new List<string>(0);
        }

        public char[,] Grille
        {
            get
            {
                return grille;
            }
        }

        public List<string> Mots
        {
            get
            {
                return mots;
            }
        }

        public bool Empty
        {
            get
            {
                return empty;
            }
        }

        /// <summary>
        /// Rempli la grille
        /// </summary>
        /// <param lettres du mot provenant de la main="mot"></param>
        /// <param  coordonnées selectionnee="select"></param>
        public void remplissage(string mot, Coordonees select)
        {
            int j = 0;
            if (empty)
            {
                if (select.Facon == 1)
                {
                    while (j < mot.Length)
                    {
                        grille[select.Ligne + j, select.Colonne] = mot[j];
                        j++;
                    }
                }
                else
                {
                    while (j != mot.Length)
                    {
                        grille[select.Ligne, select.Colonne + j] = mot[j];
                        j++;
                    }
                }
                empty = false;
            }
            else
            {
                int curseur = 0;
                if (select.Facon == 1)
                {
                    while (curseur != mot.Length)
                    {
                        if (grille[select.Ligne + j, select.Colonne] == ' ')
                        {
                            grille[select.Ligne + j, select.Colonne] = mot[curseur];
                            j++;
                            curseur++;
                        }
                        else 
                        {
                            j++;
                        }
                    }
                }
                else
                {
                    while (curseur != mot.Length)
                    {
                        if (grille[select.Ligne, select.Colonne + j] == ' ')
                        {
                            grille[select.Ligne, select.Colonne + j] = mot[curseur];
                            j++;
                            curseur++;
                        }
                        else 
                        {
                            j++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// regarde si l'alignement est correct
        /// </summary>
        /// <param booléen permettant de verifie une erreur="verifie"></param>
        /// <param mot trouvé="mot"></param>
        /// <param coordonées sélectionné="select"></param>
        /// <returns>renvoie les lettres de la main utilise </returns>
        public string test(bool verifie, string mot, Coordonees select, Dictionnaire Dicotest)
        {
            string utilise = "";
            string mot_hypo = "";
            int j = 0;
            bool touche = false;
            if (!(empty))
            {
                #region verticale
                if (select.Facon == 1)
                {
                    while (j != mot.Length)
                    {
                        if(select.Ligne + j == TAILLE_MAX)
                        {
                            verifie = false;
                        }
                        if (grille[select.Ligne + j, select.Colonne] == ' ')
                        {
                            utilise += mot[j];
                            mot_hypo += mot[j];
                        }
                        else
                        {
                            mot_hypo += grille[select.Ligne + j, select.Colonne] ;
                            touche = true;
                        }
                        if (!(juxtaposition(select.Ligne + j, select.Colonne, select.Facon, grille[select.Ligne + j, select.Colonne], Dicotest)))
                        {
                            verifie = false;
                        }
                        j++;
                    }
                    if (grille[select.Ligne+j, select.Colonne] != ' ')
                    {
                        verifie = false;
                    }
                }
                #endregion
                #region horizontale
                else
                {
                    while (j != mot.Length)
                    {
                        if (select.Colonne + j == TAILLE_MAX)
                        {
                            verifie = false;
                        }
                        else if (grille[select.Ligne, select.Colonne + j] == ' ')
                        {
                            utilise += mot[j];
                            mot_hypo += mot[j];
                        }
                        else
                        {
                            mot_hypo += grille[select.Ligne, select.Colonne+j];
                            touche = true;
                        }
                        if (!(juxtaposition(select.Ligne, select.Colonne+j, select.Facon, grille[select.Ligne, select.Colonne+j], Dicotest)))
                        {
                            verifie = false;
                        }
                        j++;
                    }
                    if(grille[select.Ligne, select.Colonne + j]!= ' ')
                    {
                        verifie = false;
                    }

                }
                #endregion
                if ((mots.Contains(mot)) || !(touche) || (mot_hypo != mot) ) 
                {
                    verifie = false;
                    Console.Write(mot_hypo);
                    Console.WriteLine("impossible");
                }
                else
                {
                    mots.Add(mot);
                    verifie = true;
                }
                if(verifie == false)
                {
                    Console.WriteLine("IMPOSSIBLE D'ECRIRE DE CETTE MANIERE");
                    utilise = MESSAGE_ERREUR;
                }
            }
            else
            {
                if(select.Facon==1)
                {
                    if((select.Ligne+mot.Length) >= TAILLE_MAX)
                    {
                        verifie = false;
                    }
                }
                else if (select.Facon == 0)
                {
                    if ((select.Colonne + mot.Length) >= TAILLE_MAX)
                    {
                        verifie = false;
                    }
                }
                if (verifie)
                {
                    utilise = mot;
                    verifie = true;
                    mots.Add(mot);
                }
                else
                {
                    utilise = MESSAGE_ERREUR;
                }
            }
            return utilise;
        }

        /// <summary>
        /// Permet d'afficher la grille
        /// </summary>
        /// <returns>renvoie la grille</returns>
        public string[,] affichage_grille()
        {
            string[,] affichage = new string[TAILLE_MAX, TAILLE_MAX];
            for (int i = 0; i < TAILLE_MAX; i++)
            {
                for (int l = 0; l < TAILLE_MAX; l++)
                {
                        affichage[i, l] = "| " + grille[i , l ] + " ";

                }
            }
            return affichage;
        }


        /// <summary>
        /// Vérifie si on crée pas des mots sans le vouloir
        /// </summary>
        /// <param Ligne="x"></param>
        /// <param Colonne="y"></param>
        /// <param Facon="h"></param>
        /// <param caractere="lettre"></param>
        /// <param Dicitionnaire="Dico"></param>
        /// <returns>si oui ou non le mot juutaposé existe</returns>
        public bool juxtaposition(int x, int y, int h, char lettre, Dictionnaire Dico)
        {
            bool possible = true;
            int j = 1;
            if(!empty)
            {
                if (h == 1)
                {
                    string prefixe = "";
                    while ((grille[x, y - j] != ' ') && ((y - j) != TAILLE_MAX))
                    {
                        prefixe += grille[x, y - j];
                        j--;
                    }
                    string tmp = InverseCaract(prefixe);
                    prefixe = tmp;
                    string suffixe = "";
                    while ((grille[x, y + j] != ' ') && ((y + j) != TAILLE_MAX))
                    {
                        prefixe += grille[x, y + j];
                        j++;
                    }
                    string mot_test = prefixe + lettre + suffixe;
                    if (mot_test.Length >= 2)
                    {
                        if (!(Dico.Apppatient(mot_test)))
                        {
                            possible = false;
                        }
                    }

                }
                else if (h == 0)
                {
                    string prefixe = "";
                    while ((grille[x - j, y] != ' ') && ((x - j) != TAILLE_MAX))
                    {
                        prefixe += grille[x - j, y];
                        j--;
                    }
                    string tmp = InverseCaract(prefixe);
                    prefixe = tmp;
                    string suffixe = "";
                    while ((grille[x + j, y] != ' ') && ((x + j) != TAILLE_MAX))
                    {
                        prefixe += grille[x + j, y];
                        j++;
                    }
                    string mot_test = prefixe + lettre + suffixe;
                    if (mot_test.Length >= 2)
                    {
                       
                        if (!(Dico.Apppatient(mot_test)))
                        {
                            possible = false;
                        }
                    }
                }
            }
            return possible;
        }

        /// <summary>
        /// Inverse les caractères d'une chaine
        /// </summary>
        /// <param mot utilise="mot"></param>
        /// <returns>le mot inversé</returns>
        public string InverseCaract(string mot)
        {
            string motInverse = null;
            for (int i = mot.Length - 1; i >= 0; i--)
            {
                motInverse = motInverse + mot[i];
            }
            return motInverse;
        }
    }
}
