using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Osorio_Thomas_TDJ_Probleme

{
    public class Dictionnaire
    { 
        Dictionary<int, List<string>> MonDico;

        const int TAILLE_MAX = 15;
        const int NBLETTRES = 26;
        const int NBMOTSMAX = 10000;
        const double NB_TOTAL_MOTS = 130557;
        double TailleDico;


        public Dictionnaire()
        {
            TailleDico = 0;
            MonDico = new Dictionary<int, List<string>>(); 
            ReadFile();

        }

        /// <summary>
        /// Lit le fichier dictionnaire
        /// </summary>
        public void ReadFile()
        {
            StreamReader file = new StreamReader("Dictionnaire.txt");
            string ligne = "";
            int key = 1;
            while (file.Peek() > 0)
            {

                ligne = file.ReadLine();
                List<string> mots = new List<string>();
                int IdLigne = Convert.ToInt32(ligne[0]);
                string[] data = ligne.Split(' ');
                if ((IdLigne >= 65) && (IdLigne <= 90))
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        TailleDico++;
                        mots.Add(data[i]);
                    }
                    MonDico.Add(key, mots);
                }
                else
                {
                    if (ligne[0] == 49)
                    {
                        key = (Convert.ToInt32(ligne[1]) + ligne[0]) - 87;
                    }
                    else
                    {
                        key += 1;
                    }
                }
            }
            Console.WriteLine("Dictionnaire chargé à 100 %");
        }
        
        /// <summary>
        /// Verifie si le mot appartient bien au dico
        /// </summary>
        /// <param mot trouvé="mot"></param>
        /// <returns></returns>
        public bool Apppatient(string mot)
        {
            bool dedans = false;
            int taille = mot.Length;
            List<string> value = MonDico[taille];
            if (RechDichoRecursif(0 , value.Count , mot))
            {
                dedans = true; 
            }
            return dedans;
        }


        /// <summary>
        /// Recherche dichotomique
        /// </summary>
        /// <param Debut de la liste="debut"></param>
        /// <param fin de la liste="fin"></param>
        /// <param  mot trouve="mot"></param>
        /// <returns></returns>
        public bool RechDichoRecursif(int debut, int fin, string mot)
        {

            int milieu = (debut + fin) / 2;

            if (debut > fin) return false;
            else
                if (mot == MonDico[mot.Length][milieu])
                return true;
            else
                if (MonDico[mot.Length].IndexOf(mot) > milieu)
                return RechDichoRecursif(milieu + 1, fin, mot);
            else
                return RechDichoRecursif(debut, milieu - 1, mot);
        }

        /// <summary>
        /// Affiche le dico
        /// </summary>
        /// <returns></returns>
        public string Tostring()
        {
            string AffichageDico = "";
            for (int i = 2; i <= TAILLE_MAX; i++)
            {
                AffichageDico += Convert.ToString(i);
                Console.WriteLine(Convert.ToString(i));
                if (MonDico.ContainsKey(i))
                {
                    List<string> value = MonDico[i];
                    foreach (string mots in value)
                    {
                        AffichageDico += (mots + " ");

                    }
                }
                AffichageDico += "\n";
            }
            return AffichageDico;

        }
    }
}
