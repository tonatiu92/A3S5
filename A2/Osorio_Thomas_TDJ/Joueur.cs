using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osorio_Thomas_TDJ_Probleme
{
    public class Joueur
    {
        ///ATTRIBUTS
        string nom;
        int score;
        Lettres main;
        static int nbJoueurs;
        MotsCroises grille;

        ///CONSTANTES
        const int TIRAGE_INITIAL = 6;
        const int TIRE_JETON = 1;
        const string MESSAGE_ERREUR = "pas possible";

        ///CONSTRUCTEURS
        public Joueur(string nom, Lettres obtenu, Random r)
        {
            this.nom = nom;
            this.score = 0;
            List<Lettre> initial = new List<Lettre>(0);
            main = new Lettres(0, initial);
            if (Add_Lettres(TIRAGE_INITIAL, obtenu, r))
            {
                grille = new MotsCroises();
            }
            nbJoueurs++;
        }

        ///PROPRIETES
        public Lettres Main
        {
            get
            {
                return main;
            }
        }
        public int Nbjoueurs
        {
            get
            {
                return nbJoueurs;
            }
        }
        public MotsCroises Grille
        {
            get
            {
                return grille;
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        


       /// <summary>
       /// réalise l'action de joueur d'un joueur
       /// </summary>
       /// <param Dicitonnaire du jeu="Dicojoueur"></param>
        public void joue(Dictionnaire Dicojoueur)
        {
            bool verification = true;
            int nbEssai = 3;
            string mot = "";
            bool motIsValid = false;
            while (!(motIsValid) && (nbEssai > 0) )
            {
                Console.WriteLine("Saisissez un nouveau mot trouvé, vous avez "+ nbEssai + " essais");
                string saisie = Console.ReadLine();
                if (!(Dicojoueur.Apppatient(saisie)) )
                {
                    verification = false;
                    Console.WriteLine("mot inexistant");
                    nbEssai--;
                }
                else
                {
                    motIsValid = true;
                    mot = saisie;
                    nbEssai = 0;
                }
            }

            if (verification)
            {
                Console.WriteLine("Saisissez la poisition x,y,h (colonne, ligne, 0 si horizontale et 1 si verticale)");
                int x = Convert.ToInt32(Console.ReadLine())-1;
                int y = Convert.ToInt32(Console.ReadLine())-1;
                int h = Convert.ToInt32(Console.ReadLine());
                Coordonees selection = new Coordonees(x, y, h);
                string DeLaMain = grille.test(verification, mot, selection, Dicojoueur);
                for (int i = 0; i < DeLaMain.Length; i++)
                {
                    if (!(main.Symbole_Exist(DeLaMain[i])))
                    {
                        verification = false;

                    }
                }
                if ((DeLaMain != MESSAGE_ERREUR)&&(verification))
                {
                    grille.remplissage(DeLaMain, selection);
                    Calcul_score(mot);
                    OteLettre(DeLaMain);
                }
                
            }
        }



        /// <summary>
        /// Additionne des lettres à la main du joueur
        /// </summary>
        /// <param nombre de lettres ajoutees="nb"></param>
        /// <param lieu d'où proviennent les lettres="pioche"></param>
        /// <param permet de piocher aléatoirement="r"></param>
        /// <returns>verifie si on peut additionner des lettres</returns>
        public bool Add_Lettres(int nb, Lettres pioche, Random r)
        {
            bool possible = true;
            if (pioche.NbLettres == 0)
            {
                possible = false;

            }
            else
            {
                for (int i = 0; i < nb; i++)
                {
                    bool existe = false;
                    while (existe == false)
                    {
                        char select = Convert.ToChar(r.Next(65, 90));
                        if (pioche.Symbole_Exist(select))
                        {
                            Lettre choisi = pioche.Collection[pioche.index_traite(select)];
                            main.Add(select, choisi.Poids);
                            pioche.Remove(select);
                            existe = true;
                        }
                    }
                }
            }
            return possible;

        }

        /// <summary>
        /// Renvoie l'etat d'un joueur dans la console;
        /// </summary>
        /// <returns>retourne la grille</returns>
        public string toString()
        {
            string etat = "";
            string[,] tableau = grille.affichage_grille();
            for (int i = 0; i < tableau.GetLength(0); i++)
            {
                for (int j = 0; j < tableau.GetLength(1); j++)
                {
                    etat += tableau[i, j];
                }
                etat += "\n";
            }
            etat += nom + "  score: " + Convert.ToString(score) + "\n" + main.tostring();
            return etat;
        }

        /// <summary>
        /// enleve les lettres de la main
        /// </summary>
        /// <param les lettres du mots étant dans la main="mot"></param>
        public void OteLettre(string mot)
        {
            for (int i=0; i < mot.Length; i++)
            {
                main.Remove(mot[i]);
            }
        }
        
        /// <summary>
        /// Calcul le score du joueur
        /// </summary>                              ///Demander comment calculer le score
        /// <param mot trouvé ="mot"></param>
        public void Calcul_score(string mot)
        {
            for (int i = 0; i < mot.Length; i++)
            {
                char element = mot[i];
                switch (element)
                {
                    case 'K':
                        score += 5;
                        break;
                    case 'W':
                        score += 5;
                        break;
                    case 'X':
                        score += 5;
                        break;
                    case 'Y':
                        score += 5;
                        break;
                    case 'Z':
                        score += 5;
                        break;
                }
                 
            }
            if(mot.Length > 4)
            {
                score += mot.Length;
            }
        }




        /// <summary>
        /// regarde si la main est vide
        /// </summary>
        /// <returns>renvoie si oui ou non la main est vide</returns>
        public bool main_vide()
        {
            bool vide = false;
            if(main.Collection.Count == 0)
            {
                vide = true;
            }
            return vide;
        }
        
    }
}
