using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfAppProblemeInfo
{
    public class Huffman
    {
        Symbole Racine;
        private List<Symbole> List_Symboles = new List<Symbole>();
        public Symbole racine
        {
            get
            {
                return Racine;
            }
            set
            {
                Racine = value;
            }
        }

        /// <summary>
        /// créer un arbre de Huffman
        /// </summary>
        /// <param texte code ="texte"></param>
        public Huffman(string texte)
        {
            for (int i = 0; i < texte.Length; i++)
            {
                Symbole nouveau = new Symbole(texte[i]);
                bool exist = false;
                int index;
                for (int j = 0; j < List_Symboles.Count; j++)
                {
                    if (List_Symboles[j].Symbol == texte[i])
                    {
                        exist = true;
                        index = j;
                        List_Symboles[j].Frequence++;
                    }
                }
                if (!exist)
                {

                    List_Symboles.Add(nouveau);
                }
            }

            while (List_Symboles.Count > 1)
            {
                List<Symbole> FrequenceTri = Tri_insertion(List_Symboles);
                Console.ReadLine();
                if (FrequenceTri.Count >= 2)
                {
                    List<Symbole> Elements = FrequenceTri.Take(2).ToList<Symbole>();////ON PRENDS LES DEUX PREMIERS ELEMENTS DE LA LISTE
                    Symbole parent = new Symbole('*');
                    parent.Frequence = Elements[0].Frequence + Elements[1].Frequence;
                    parent.Gauche = Elements[0];
                    parent.Droite = Elements[1];

                    List_Symboles.Remove(Elements[0]);
                    List_Symboles.Remove(Elements[1]);
                    List_Symboles.Add(parent);
                }

                this.Racine = List_Symboles.FirstOrDefault();

            }

        }

        /// <summary>
        /// Code l'arbre en binaire
        /// </summary>
        /// <param texte source="source"></param>
        /// <returns></returns>
        public List<bool> Codage(string source)
        {
            List<bool> BitsSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Racine.Branche(source[i], new List<bool>());
                BitsSource.AddRange(encodedSymbol);
            }

            return BitsSource;
        }
        /// <summary>
        /// Algortithme de tri par insertion
        /// </summary>
        /// <param list non trie="non_trie"></param>
        /// <returns></returns>
        public List<Symbole> Tri_insertion(List<Symbole> non_trie)
        {
            List<Symbole> trie = non_trie;
            for (int i = 1; i < trie.Count; i++)
            {
                Symbole tmp = new Symbole(trie[i]);
                int j = i;
                while ((j > 0) && (trie[j - 1].Frequence > tmp.Frequence))
                {
                    trie[j] = trie[j - 1];
                    j--;
                }
                trie[j] = tmp;
            }
            return trie;

        }
    }
}


