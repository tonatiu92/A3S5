using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osorio_Thomas_TDJ_Probleme
{
    public class Lettres
    {
        //Méthodes
        List<Lettre> collection;
        int NbLettre;


        //Constructeurs
        public Lettres(int NbLettres, List<Lettre> jeu)
        {
            this.NbLettre = NbLettres;
            collection = jeu;
        }

        //Propriétés
        public int NbLettres
        {
            get
            {
                return NbLettre;
            }
            set
            {
                NbLettre = value;
            }
        }

        public List<Lettre> Collection
        {
            get
            {
                return collection;
            }
        }




        /// <summary>
        /// ajoute et retire une lettre de la collection
        /// </summary>
        /// <param name="ajoute"></param>
        /// <param name="poids_ajoute"></param>
        public void Add(char ajoute, int poids_ajoute)
        {
            if (Symbole_Exist(ajoute))
            {
                collection[index_traite(ajoute)].Frequence += 1;
                NbLettre++;
            }
            else
            {
                Lettre cree = new Lettre(ajoute, poids_ajoute, 1);
                collection.Add(cree);
                NbLettre++;
            }
        }


        /// <summary>
        /// Enleve une lettre de la collection de lettres
        /// </summary>
        /// <param caractère supprimer="enleve"></param>
        public void Remove(char enleve)
        {
            collection[index_traite(enleve)].Frequence -= 1;
            if (collection[index_traite(enleve)].Frequence == 0)
            {
                collection.Remove(collection[index_traite(enleve)]);  ///enelvepas
            }
        }

        /// <summary>
        /// Verifie si le symbole est présent dans la collection
        /// </summary>
        /// <param caractere tirer au hasasrd="aleatoire"></param>
        /// <returns>renvoie une booléen </returns>
        public bool Symbole_Exist(char aleatoire)
        {
            bool contain = false;
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Equal_Symbol(aleatoire))     //////A CORRIGER AVEC .CONTAINS
                {
                    contain = true;
                }
            }
            return contain;
        }

        /// <summary>
        /// index de la collection en cours de traitement
        /// </summary>
        /// <param caractere pioche="aleatoire"></param>
        /// <returns>l'index de type int de la collection</returns>
        public int index_traite(char aleatoire)
        {
            int index = 0;
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Equal_Symbol(aleatoire))
                {
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// Affiche la liste
        /// </summary>
        /// <returns>renvoie une chaine de caractere de la liste de lettres</returns>
        public string tostring()
        {
            string liste = " ";
            for(int i = 0; i < collection.Count; i ++)
            {
                int j = 0;
                while (j != collection[i].Frequence)
                {
                    liste += Convert.ToString(collection[i].Symbole) + " ";
                    j++;
                }
            }
            return liste;
        }

    }
}
