using System;
using System.Collections.Generic;

namespace WpfAppProblemeInfo
{
    class JPEG
    {
        /// <summary>
        /// CLASS EN CONSTRUCTION NON ACTIVE SUR CE CODE 
        /// </summary>
        Pixel[,] Compression;
        int TailleEOB;
        List<bool> DonneeY;
        List<bool> DonneeCb;
        List<bool> DonneeCr;


        public JPEG(MyImage decompresse)
        {
            sous_echantillonage(decompresse.image);
            DonneeY = new List<bool>();
            DonneeCb = new List<bool>();
            DonneeCr = new List<bool>();
            for (int i = 0; i < Compression.GetLength(0); i += 8)
            {
                for (int j = 0; j < Compression.GetLength(1); j += 8)
                {
                    DCT(i, j);
                    Quantification(i, j);
                    List<int> codeRLELum = new List<int>();
                    List<int> codeRLECb = new List<int>();
                    List<int> codeRLECr = new List<int>();
                    RLE(codeRLELum, 0);
                    RLE(codeRLECb, 1);
                    RLE(codeRLECr, 2);
                    //////RESTE  A RELIER HUFFFMANN A CETTE ALGOTITHME MAIS MANQUE DE TEMPS  
                }
            }



        }
        public string Write()
        {
            string affiche = "";
            for (int i = 0; i < DonneeY.Count; i++)
            {
                if (DonneeY[i])
                {
                    affiche += "1";
                }
                else
                {
                    affiche += "0";
                }
            }
            affiche += "\n";
            for (int i = 0; i < DonneeCb.Count; i++)
            {
                if (DonneeCb[i])
                {
                    affiche += "1";
                }
                else
                {
                    affiche += "0";
                }
            }
            affiche += "\n";
            for (int i = 0; i < DonneeCr.Count; i++)
            {
                if (DonneeCr[i])
                {
                    affiche += "1";
                }
                else
                {
                    affiche += "0";
                }
            }
            return affiche;
        }
        public void sous_echantillonage(Pixel[,] Image)
        {
            Compression = new Pixel[Image.GetLength(0), Image.GetLength(1)];
            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j += 2)
                {
                    if (j + 1 < Image.GetLength(0))
                    {
                        /// Image[i, j].RVB_to_Chrom();                             fonction qui permettait de convertir du RGB au Chromatique mais non présente dans cette version du code pour ne pas interférer
                        int MoyColCb;
                        MoyColCb = (Image[i, j].Vert + Image[i, j + 1].Vert) / 2;
                        Image[i, j].Vert = MoyColCb;
                        Image[i, j + 1].Vert = 0;
                        int MoyColCr;
                        MoyColCr = (Image[i, j].Bleu + Image[i, j + 1].Bleu) / 2;
                        Image[i, j].Bleu = MoyColCr;
                        Image[i, j + 1].Bleu = 0;
                    }

                }
            }
            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    Compression[i, j] = new Pixel(Image[i, j].Rouge, Image[i, j].Vert, Image[i, j].Bleu);
                }
            }
        }
        public void DCT(int debutX, int debutY)
        {
            double Pi = Math.PI;
            Pixel[,] Matrice = new Pixel[8, 8];
            for (int i = debutX; i < 8 + debutX; i++)
            {
                for (int j = debutY; j < 8 + debutY; j++)
                {
                    Matrice[i - debutX, j - debutY] = new Pixel(Compression[i, j]);
                    double SommeLum = 0;
                    double SommeCb = 0;
                    double SommeCr = 0;
                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            SommeLum += Compression[debutX + k, debutY + l].Rouge * Math.Cos(((2 * k + 1) * (i - debutX) * Pi) / 16) * Math.Cos(((2 * l + 1) * (j - debutY) * Pi) / 16);
                            SommeCb += Compression[debutX + k, debutY + l].Rouge * Math.Cos(((2 * k + 1) * (i - debutX) * Pi) / 16) * Math.Cos(((2 * l + 1) * (j - debutY) * Pi) / 16);
                            SommeCr += Compression[debutX + k, debutY + l].Rouge * Math.Cos(((2 * k + 1) * (i - debutX) * Pi) / 16) * Math.Cos(((2 * l + 1) * (j - debutY) * Pi) / 16);
                        }
                    }
                    /* if((i!=0)&&(j!=0))
                     {
                         Matrice[i - debutX, j - debutY].Rouge = Convert.ToInt32( (1 / 4) * SommeLum);
                     }
                     else if ((i != 0) || (j != 0))
                     {
                         Matrice[i - debutX, j - debutY].Rouge = Convert.ToInt32((1 / 4) *(1/Math.Sqrt(2)* SommeLum));
                     }
                     else
                     {
                         Matrice[i - debutX, j - debutY].Rouge = Convert.ToInt32((1 / 8) * SommeLum);
                     }
                     Compression[debutX + i, debutY + j] = Matrice[i - debutX, j - debutY];*/
                    Compression[i - debutX, j - debutY].Rouge = Convert.ToInt32((1 / 4) * SommeLum);
                    Compression[i - debutX, j - debutY].Vert = Convert.ToInt32((1 / 4) * SommeCb);
                    Compression[i - debutX, j - debutY].Bleu = Convert.ToInt32((1 / 4) * SommeCr);
                }

            }


        }
        public void Quantification(int debutX, int debutY)
        {
            for (int i = debutX; i < 8 + debutX; i++)
            {
                for (int j = debutY; j < 8 + debutY; j++)
                {
                    Compression[i, j].Rouge = (Compression[i, j].Rouge + Matrice_Quantification()[i - debutX, j - debutY] / 2) / Matrice_Quantification()[i - debutX, j - debutY];
                    Compression[i, j].Vert = (Compression[i, j].Rouge + Matrice_Quantification()[i - debutX, j - debutY] / 2) / Matrice_Quantification()[i - debutX, j - debutY];
                    Compression[i, j].Bleu = (Compression[i, j].Rouge + Matrice_Quantification()[i - debutX, j - debutY] / 2) / Matrice_Quantification()[i - debutX, j - debutY];
                }
            }
        }
        public void RLE(List<int> codeRLE, int valeur)
        {

            int i = 0;
            int j = 0;
            codeRLE = new List<int>();
            bool montee = true;
            int EOBX = 0;
            int EOBY = 0;
            EOB(EOBX, EOBY, valeur);
            while ((i != EOBX) || (j != EOBY))
            {
                if (valeur == 0)
                {
                    codeRLE.Add(Compression[i, j].Rouge);
                }
                else if (valeur == 1)
                {
                    codeRLE.Add(Compression[i, j].Vert);
                }
                else
                {
                    codeRLE.Add(Compression[i, j].Bleu);
                }
                if (!montee)
                {
                    if ((i != 0) && (j != 0))
                    {
                        i++;
                        j--;
                    }
                    else if (j == 0)
                    {
                        if (valeur == 0)
                        {
                            codeRLE.Add(Compression[i, j].Rouge);
                        }
                        else if (valeur == 1)
                        {
                            codeRLE.Add(Compression[i, j].Vert);
                        }
                        else
                        {
                            codeRLE.Add(Compression[i, j].Bleu);
                        }
                        montee = true;
                        j++;
                    }
                }
                else
                {
                    if ((i != 0) && (j != 0))
                    {
                        i--;
                        j++;
                    }
                    else if (i == 0)
                    {
                        if (valeur == 0)
                        {
                            codeRLE.Add(Compression[i, j].Rouge);
                        }
                        else if (valeur == 1)
                        {
                            codeRLE.Add(Compression[i, j].Vert);
                        }
                        else
                        {
                            codeRLE.Add(Compression[i, j].Bleu);
                        }
                        montee = false;
                        i++;
                    }
                }
            }
            if (valeur == 0)
            {
                codeRLE.Add(Compression[i, j].Rouge);
            }
            else if (valeur == 1)
            {
                codeRLE.Add(Compression[i, j].Vert);
            }
            else
            {
                codeRLE.Add(Compression[i, j].Bleu);
            }
        }
        public void EOB(int EOBX, int EOBY, int valeur)
        {
            this.TailleEOB = 0;
            for (int i = Compression.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = Compression.GetLength(1) - 1; j >= 0; j--)
                {
                    if ((Compression[i, j].Rouge != 0) && (valeur == 0))
                    {
                        EOBX = i;
                        EOBY = j;

                    }
                    else if ((Compression[i, j].Vert != 0) && (valeur == 1))
                    {
                        EOBX = i;
                        EOBY = j;

                    }
                    else if ((Compression[i, j].Bleu != 0) && (valeur == 2))
                    {
                        EOBX = i;
                        EOBY = j;

                    }
                    else
                    {
                        TailleEOB++;
                    }
                }
            }
        }

        public int[,] Matrice_Quantification()
        {
            int[,] Quantificateur = new int[,] { { 16, 11, 10, 16, 24, 40, 51, 61 },
                                                 { 12, 12, 14, 19, 26, 58, 60, 55 },
                                                 { 14, 13, 16, 24, 40, 57, 69, 56 },
                                                 { 14, 17, 22, 29, 51, 87, 80, 62 },
                                                 { 18, 22, 37, 56, 68, 109, 103, 77 },
                                                 { 24, 35, 55, 64, 81, 104, 113, 92 },
                                                 { 49, 64, 78, 87, 103, 121, 120, 101 },
                                                 { 72, 92, 95, 98, 112, 100, 103, 99 } };
            return Quantificateur;
        }

    }
}

