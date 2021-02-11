using System;

namespace WpfAppProblemeInfo
{
    class Pixel
    {
        int R;
        int G;
        int B;

        bool cache;

        /// <summary>
        /// Nouveau Pixel
        /// </summary>
        /// <param canalR="rouge"></param>
        /// <param canalV="vert"></param>
        /// <param canalB="bleu"></param>
        public Pixel(int rouge, int vert, int bleu)
        {
            this.R = rouge;
            this.G = vert;
            this.B = bleu;
            cache = false;
        }
        /// <summary>
        /// Copie un pixel
        /// </summary>
        /// <param Pixel a copie="copie"></param>
        public Pixel(Pixel copie)
        {
            this.R = copie.Rouge;
            this.G = copie.Vert;
            this.B = copie.Bleu;
            cache = true;
        }


        /////////////////////////////////PROPRIETE/////////////////////////

        public int Rouge
        {
            get
            {
                return R;
            }
            set
            {
                R = value;
            }
        }
        public int Bleu
        {
            get
            {
                return B;
            }
            set
            {
                B = value;
            }
        }
        public int Vert
        {
            get
            {
                return G;
            }
            set
            {
                G = value;
            }
        }

        public bool Cache
        {
            get
            {
                return cache;
            }
            set
            {
                cache = value;
            }
        }

        /////////////////////////////////METHODES////////////////////

        /// <summary>
        /// Calcul de la moyenne du pixel
        /// </summary>
        /// <returns>retourne la valeur moyenne entiere</returns>
        public int Moyenne()
        {
            return (R + G + B) / 3;
        }

        /// <summary>
        /// Verifie si 2 pixels sont egaux
        /// </summary>
        /// <param pixel compare="a"></param>
        /// <returns>true or false</returns>
        public bool equal(Pixel a)
        {
            bool flag = false;
            if ((R == a.Rouge) && (G == a.Vert) && (B == a.Bleu))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        ///interpolation d'un pixel
        /// </summary>
        /// <param pixel voisin 1="Coor1"></param>
        /// <param pixel voisin 2="Coor2"></param>
        /// <param pixel voisin 3="Coor3"></param>
        /// <param pixel a modifie="interpole"></param>
        /// <returns></returns>
        public Pixel interpolation(int Coor1, int Coor2, int Coor3, Pixel interpole)
        {

            int Ro = ((Coor1 - Coor2 - Coor3) * this.R + Coor3 * interpole.Rouge) / (Coor1 - Coor2);
            int Gr = ((Coor1 - Coor2 - Coor3) * this.G + Coor3 * interpole.Vert) / (Coor1 - Coor2);
            int Bl = ((Coor1 - Coor2 - Coor3) * this.B + Coor3 * interpole.Bleu) / (Coor1 - Coor2);
            Pixel inter = new Pixel(Ro, Gr, Bl);
            return inter;
        }

        /// <summary>
        /// filtrer un pixel 
        /// </summary>
        /// <param Image a modifie="Image"></param>
        /// <param Matrice de filtrage="Matrice"></param>
        /// <param coordonnee Ligne="i"></param>
        /// <param coordonnee Colonne="j"></param>
        /// <returns></returns>
        public Pixel filtrage(Pixel[,] Image, int[,] Matrice, int i, int j)
        {
            int filtreR = 0;
            int filtreV = 0;
            int filtreB = 0;
            int LeftCornerLine = i - Matrice.GetLength(0) / 2;
            int LeftCornerCol = j - Matrice.GetLength(0) / 2;
            int normalisation = 0;
            for (int k = 0; k < Matrice.GetLength(0); k++)
            {
                for (int l = 0; l < Matrice.GetLength(1); l++)
                {
                    filtreR += Matrice[k, l] * Image[LeftCornerLine + k, LeftCornerCol + l].Rouge;
                    filtreV += Matrice[k, l] * Image[LeftCornerLine + k, LeftCornerCol + l].Vert;
                    filtreB += Matrice[k, l] * Image[LeftCornerLine + k, LeftCornerCol + l].Bleu;
                    normalisation += Matrice[k, l];
                }
            }
            Pixel filtre;
            if (normalisation > 1)
            {
                filtreR /= normalisation;
                filtreB /= normalisation;
                filtreV /= normalisation;
                filtre = new Pixel(Math.Abs(filtreR), Math.Abs(filtreV), Math.Abs(filtreB));
            }
            else
            {
                if (filtreR < 0)
                {
                    filtreR = 0;
                }
                else if (filtreR > 255)
                {
                    filtreR = 255;
                }
                if (filtreV < 0)
                {
                    filtreV = 0;
                }
                else if (filtreV > 255)
                {
                    filtreV = 255;
                }
                if (filtreB < 0)
                {
                    filtreB = 0;
                }
                else if (filtreB > 255)
                {
                    filtreB = 255;
                }
                filtre = new Pixel(filtreR, filtreV, filtreB);
            }



            return filtre;


        }

        /// <summary>
        /// chaine sur les caracteristiques du pixel
        /// </summary>
        /// <returns>string</returns>
        public string To_String()
        {
            string affiche = "";
            affiche += R + " " + G + " " + B;
            return affiche;
        }
    }
}
