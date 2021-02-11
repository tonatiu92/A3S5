using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Osorio_Thomas_TDJ_Probleme
{
    public class Jeu
    {
        
        /// attributs
        Dictionnaire Dico;
        Joueur[] InGame;
        Lettres pioche;
        Random rndNumbers;

        bool end_game;

        const int TAILLE_PIOCHE = 118;
        const int SCORE_INI = 0;
        const int MAX_JOUEUR = 10;
        const int MIN_JOUEUR = 2;

        #region INITIALISATION DU JEU

        /// <summary>
        /// Constructeur de la classe jeu
        /// </summary>
        public Jeu()
        {
            rndNumbers = new Random();
            Dico = new Dictionnaire();
            pioche = new Lettres(TAILLE_PIOCHE,Initialisation_Pioche());
            InGame = Initialisation_Players(rndNumbers);
            end_game = false;
        }

        /// <summary>
        /// Permet de lire le fichier pioche.txt et initialiser la pioche
        /// </summary>
        /// <returns>renvoie la liste des lettres de la pioche</returns>
        public List<Lettre> Initialisation_Pioche()
        {
            StreamReader file = new StreamReader("Pioche.txt");
            string ligne = "";
            List<Lettre> liste = new List<Lettre>();
            while (file.Peek() > 0)
            {
                ligne = file.ReadLine();
                string[] datas = ligne.Split(',');
                Lettre select = new Lettre(Convert.ToChar(datas[0]), Convert.ToInt32(datas[2]), Convert.ToInt32(datas[1]));
                liste.Add(select);
            }
            return liste;
        }

        /// <summary>
        /// permet d'initialiser la liste de joueur et d'initialiser leurs données
        /// </summary>
        /// <param Random pour trouver les lettres="tirage_initial"></param>
        /// <returns>renvoie le tableau de joueur initialise</returns>
        public Joueur[] Initialisation_Players(Random tirage_initial)
        {
            int nbP = 0;
            bool nbPIsValid = false;
            while (!nbPIsValid)
            {
                Console.WriteLine("Combien de Joueurs vont participer? (un entier entre 2 et 10)");
                string saisie = Console.ReadLine();
                if (int.TryParse(saisie, out nbP))
                    nbPIsValid = true;
                else
                {
                    nbPIsValid = false;
                    Console.WriteLine("entrez un entier entre 2 et 10 svp");
                }
            }
            Joueur[] tableau = new Joueur[nbP];
            for (int i = 0; i < nbP; i++)
            {
                Console.WriteLine("Joueur n°" + Convert.ToString(i+1) + ": ");
                Console.Write("NOM: ");
                string nom = Console.ReadLine();
                Console.WriteLine("Le joueur pioche au hasard 6 jetons.");
                tableau[i] = new Joueur(nom, pioche, tirage_initial);
                Console.WriteLine("Voici sa main initial");
                Console.WriteLine(tableau[i].Main.tostring());
            }
            return tableau;
        }

        #endregion

        /// <summary>
        /// Lance une partie
        /// </summary>
        public void partie()
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            TimeSpan interval = date2 - date1;
           
            while ( (interval.Minutes <= 10) && (!(end_game)) )
            {
                play();
                date2 = DateTime.Now;
                interval = date2 - date1;
            }
            Console.WriteLine("FIN DU JEU");
            if(pioche.Collection.Count == 0)
            {
                int vainqueur = InGame[0].Score;
                for (int i =1; i < InGame.Length; i ++)
                {
                    if(vainqueur < InGame[i].Score)
                    {
                        vainqueur = InGame[i].Score;
                    }
                }
                Console.WriteLine("Félicitaions au vainqueur " + InGame[vainqueur].Score);
            }
            else
            {
                Console.WriteLine("Pas de gagnant, temps écoulé");

            }
            

        }
        #region TOUR 
        /// <summary>
        /// Lance un tour
        /// </summary> 
        public void play()
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            TimeSpan interval = date2 - date1;
            bool end_tour = false;
            while ( !(end_tour) )  
            {
                for (int i = 0; i < InGame.Length; i++)
                {
                Console.WriteLine(InGame[i].toString());
                }
                int joueur = 0;
                bool joueurIsValid = false;
                while (!joueurIsValid)
                {
                    Console.WriteLine("Saisissez le joueur (numero supérieur à 1)");
                    string saisie = Console.ReadLine();
                    if ((int.TryParse(saisie, out joueur))&&(Convert.ToInt32(saisie) <= InGame.Length))
                        joueurIsValid = true;
                    else
                    {
                        joueurIsValid = false;
                        Console.WriteLine("entrez un entier entre 1 et le nombre de joueur svp");
                    }
                }
                joueur -= 1;
                InGame[joueur].joue(Dico);
                if(InGame[joueur].main_vide())
                {
                    end_tour = true;
                    if(!(distribution(joueur)))
                    {
                        end_tour = true;
                        end_game = true;
                    }
                }
                date2 = DateTime.Now;
                interval = date2 - date1;
                if (interval.Minutes >= 1)
                {
                    end_tour = true;
                }

            }
            Console.WriteLine("FIN DU TOUR");
            distribution(InGame.Length);
        }

        /// <summary>
        /// Disitribue deux lettres à chaque joueur(celui qui distribue n'en recoit pas 
        /// si personne ne distribue tous les joueurs recoivent)
        /// </summary>
        /// <param le joueur qui ne recevra pas de lettre="distribue"></param>
        /// <returns>verifie si on peut encore distrbuer des lettres de la pioche</returns>
        public bool distribution(int distribue)
        {
            bool effectue = false;
            for (int i = 0; i < InGame.Length; i++)
            {
                if (i != distribue)
                {
                    if (InGame[i].Add_Lettres(2, pioche, rndNumbers))
                    {
                        effectue = true;
                        Console.WriteLine("rajoute 2 lettres");
                    }
                }
            }
            return effectue;
        }
        #endregion

    }
}
