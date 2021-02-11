using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WpfAppProblemeInfo
{
    class MyImage
    {
        //ATTRIBUTS
        string bfType;
        int bfSize;
        int biWidth;
        int biHeight;
        static int biBitCount;
        Pixel[,] Image;

        const int TAILLE_HEADER = 54;


        ///////////////////////////////////////////CONSTRUCTEURS///////////////////////////////////////////




        /// <summary>
        /// Constructeur définit à partir d'une image bitmap existente
        /// </summary>
        /// <param fichier selectionné ="myfile"></param>
        public MyImage(string myfile)
        {
            ///TOUS CONVERTIROU CONVERTIR CE QU IL NOUS SERT
            byte[] datas = File.ReadAllBytes(myfile);
            byte[] Taille = new byte[4];
            byte[] largeur = new byte[4];
            byte[] hauteur = new byte[4];
            if ((datas[0] == 66) && (datas[1] == 77))
            {
                bfType = "BM";
            }
            for (int i = 2; i < 6; i++)
            {
                Taille[i - 2] = datas[i];
            }
            this.bfSize = Convertir_Endian_To_Int(Taille);
            for (int i = 18; i < 22; i++)
            {
                largeur[i - 18] = datas[i];
            }
            this.biWidth = Convertir_Endian_To_Int(largeur);

            for (int i = 22; i < 26; i++)
            {
                hauteur[i - 22] = datas[i];
            }
            this.biHeight = Convertir_Endian_To_Int(hauteur);

            biBitCount = 24;
            Image = new Pixel[biHeight, biWidth];
            byte[] rouge = new byte[1];
            byte[] vert = new byte[1];
            byte[] bleu = new byte[1];
            int k = 0;
            int l = 0;
            for (int i = TAILLE_HEADER; i < datas.Length; i += 3)
            {
                if (((i + 2) < datas.Length) && (l != biHeight))
                {
                    bleu[0] = datas[i];
                    vert[0] = datas[i + 1];
                    rouge[0] = datas[i + 2];
                    Pixel lu = new Pixel(Convertir_Endian_To_Int(rouge), Convertir_Endian_To_Int(vert), Convertir_Endian_To_Int(bleu));
                    Image[l, k] = lu;
                    k++;
                    if (k == biWidth)
                    {
                        k = 0;
                        l++;
                        while ((i + 1) % 4 != 0)
                        {
                            i++;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Créer une fractale de MandelBrot
        /// </summary>
        /// <param Largeur="W"></param>
        /// <param Hauteur="H"></param>
        public MyImage(int W, int H)
        {
            this.biWidth = W;
            this.biHeight = H;
            this.bfType = "BM";
            this.bfSize = TAILLE_HEADER + biWidth * biHeight * 3;
            biBitCount = 24;
            Image = new Pixel[biWidth, biHeight];

            Pixel black = new Pixel(0, 0, 0);
            Pixel White = new Pixel(255, 255, 255);
            Pixel Blue = new Pixel(0, 0, 255);
            Pixel Green = new Pixel(0, 255, 0);
            Pixel Red = new Pixel(255, 0, 0);
            int count = 0;
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    double a = (double)(i - (biWidth / 2)) / (biWidth / 4);
                    double b = (double)(j - (biHeight / 2)) / (biHeight / 4);
                    Complexe c = new Complexe(a, b);
                    Complexe z = new Complexe(0, 0);
                    int it = 0;
                    do
                    {
                        it++;
                        z = z.Square();
                        z = z.Add(c);
                        if (z.Norme() > 2.0)
                        {
                            break;
                        }
                    } while (it < 100);
                    if (it == 100)
                    {
                        Image[i, j] = Blue;
                    }
                    else if (it > 75)
                    {
                        Image[i, j] = White;
                        count++;
                    }
                    else if (it > 50)
                    {
                        Image[i, j] = Green;
                        count++;
                    }
                    else if (it > 25)
                    {
                        Image[i, j] = Red;
                        count++;
                    }
                    else
                    {
                        Image[i, j] = black;
                        count = 0;
                    }
                }
            }
        }



        /// <summary>
        /// Créer un QRcode à partir d'un texte écrit
        /// </summary>
        /// <param texte entré="texte"></param>
        /// <param niveau de correction souhaité="correction"></param>
        public MyImage(string texte, char correction)
        {
            QRcode Genere = new QRcode(texte, correction);

            if (Genere.Version == 1)
            {
                this.biWidth = 21;
                this.biHeight = 21;
            }
            if (Genere.Version == 2)
            {
                this.biWidth = 25;
                this.biHeight = 25;
            }
            this.bfType = "BM";
            this.bfSize = TAILLE_HEADER + biWidth * biHeight * 3;
            biBitCount = 24;
            Image = new Pixel[biWidth, biHeight];
            Pixel black = new Pixel(0, 0, 0);
            Pixel White = new Pixel(255, 255, 255);
            ///STEP1
            patternHG(black, White);
            patternHD(black, White);
            patternBG(black, White);
            ///STEP2
            separateur(black, White);
            ///STEP3
            if (Genere.Version == 2)
            {
                Console.WriteLine("kkkkkk");
                Console.ReadLine();
                alignement(black, White);
            }
            //STEP4
            timing_pattern(black, White);



            //STEP5 DARK MODULE
            Image[7, 8] = new Pixel(black);
            //STEP5
            reserved_area(black, White, Genere);
            //STEP6
            placing_data(black, White, Genere);
            //STEP7
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    if (Image[i, j] == null)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            Image[i, j] = new Pixel(0, 0, 0);
                        }
                        else
                        {
                            Image[i, j] = new Pixel(255, 255, 255);
                        }

                    }
                }
            }

        }

        /// <summary>
        /// Créer un Histogramme
        /// </summary>
        /// <param Image ="traite"></param>
        public MyImage(MyImage traite)
        {
            Histogramme Histo = new Histogramme(traite.image);
            this.biWidth = 256 * 8;
            this.bfType = "BM";
            int echelle = 0;
            if (Histo.Max_value() > 1000)
            {
                echelle = Histo.Max_value() / 1000;
                for (int i = 0; i < 256; i++)
                {
                    Histo.HM[i] /= echelle;
                    Histo.HR[i] /= echelle;
                    Histo.HV[i] /= echelle;
                    Histo.HB[i] /= echelle;
                }
            }
            else
            {
                echelle = 1000 / Histo.Max_value();
                for (int i = 0; i < 256; i++)
                {
                    Histo.HM[i] *= echelle;
                    Histo.HR[i] *= echelle;
                    Histo.HV[i] *= echelle;
                    Histo.HB[i] *= echelle;
                }
            }
            this.biHeight = Histo.Max_value();
            this.bfSize = TAILLE_HEADER + biWidth * biHeight * 3;
            Image = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j += 8)
                {
                    ///////ROUGE/////
                    if (i == Histo.HR[j / 8])
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(255, 0, 0);
                        }
                    }
                    else if (((i <= Histo.HR[(j - 1) / 8]) && (i >= Histo.HR[j / 8])) || ((i >= Histo.HR[(j - 1) / 8]) && (i <= Histo.HR[j / 8])))
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(255, 0, 0);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (Image[i, j + k] == null)
                            {
                                Image[i, j + k] = new Pixel(255, 255, 255);
                            }
                        }
                    }
                    /////VERT///
                    if (i == Histo.HV[j / 8])
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(0, 255, 0);
                        }
                    }
                    else if (((i <= Histo.HV[(j - 1) / 8]) && (i >= Histo.HV[j / 8])) || ((i >= Histo.HV[(j - 1) / 8]) && (i <= Histo.HV[j / 8])))
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(0, 255, 0);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (Image[i, j + k] == null)
                            {
                                Image[i, j + k] = new Pixel(255, 255, 255);
                            }
                        }
                    }
                    ////BLEU
                    if (i == Histo.HB[j / 8])
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(0, 0, 255);
                        }
                    }
                    else if (((i <= Histo.HB[(j - 1) / 8]) && (i >= Histo.HB[j / 8])) || ((i >= Histo.HB[(j - 1) / 8]) && (i <= Histo.HB[j / 8])))
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(0, 0, 255);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (Image[i, j + k] == null)
                            {
                                Image[i, j + k] = new Pixel(255, 255, 255);
                            }
                        }
                    }
                    //////NOIR/////
                    ///
                    if (i == Histo.HM[j / 8])
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(0, 0, 0);
                        }
                    }
                    else if (((i <= Histo.HM[(j - 1) / 8]) && (i >= Histo.HM[j / 8])) || ((i >= Histo.HM[(j - 1) / 8]) && (i <= Histo.HM[j / 8])))
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Image[i, j + k] = new Pixel(0, 0, 0);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (Image[i, j + k] == null)
                            {
                                Image[i, j + k] = new Pixel(255, 255, 255);
                            }

                        }
                    }

                }
            }
        }
        /////////////////////////////////////////PROPRIETES///////////////////////////////////////////////

        public Pixel[,] image
        {
            get
            {
                return Image;
            }
            set
            {
                Image = value;
            }
        }

        public int Width
        {
            get
            {
                return biWidth;
            }
        }

        public int Height
        {
            get
            {
                return biHeight;
            }
        }



        ///////////////////////////////////CREATION D'IMAGE////////////////////////////////////////////////////////////
        //METHODES

        /// <summary>
        /// écrit dans un fichier l'objet au format BMP 
        /// </summary>
        /// <param Nom du nouveau fichier="file"></param>
        public void From_Image_To_File(string file)
        {


            int complement = 0;
            if ((biWidth * 3) % 4 != 0)
            {

                while ((biWidth * 3 + complement) % 4 != 0)
                {
                    complement++;
                }
                bfSize += complement * biHeight;
            }
            byte[] datas = new byte[bfSize];
            byte[] Taille = Convertir_Int_To_Endian(bfSize, 4);
            byte[] largeur = Convertir_Int_To_Endian(biWidth, 4);
            byte[] hauteur = Convertir_Int_To_Endian(biHeight, 4);
            byte[] nbBit = Convertir_Int_To_Endian(biBitCount, 4);
            datas[0] = 66;
            datas[1] = 77;

            //HEADER
            for (int i = 2; i < 6; i++)
            {
                datas[i] = Taille[i - 2];
            }
            for (int i = 6; i < 10; i++)
            {
                datas[i] = 0;
            }
            datas[10] = 54;
            for (int i = 11; i < 14; i++)
            {
                datas[i] = 0;
            }

            ///HEADER INFOS
            datas[14] = 40;
            for (int i = 15; i < 18; i++)
            {
                datas[i] = 0;
            }
            for (int i = 18; i < 22; i++)
            {
                datas[i] = largeur[i - 18];
            }
            for (int i = 22; i < 26; i++)
            {
                datas[i] = hauteur[i - 22];
            }
            datas[26] = 1;
            datas[27] = 0;
            for (int i = 28; i < 30; i++)
            {
                datas[i] = nbBit[i - 28];
            }
            for (int i = 30; i < 34; i++)///TYPE DE COMPRESSION
            {
                datas[i] = 0;
            }
            int TailleImage = biHeight * biWidth * 3 + complement * biHeight;
            byte[] biSizeImage = Convertir_Int_To_Endian(TailleImage, 4);
            for (int i = 34; i < 38; i++)
            {
                datas[i] = biSizeImage[i - 34];
            }
            for (int i = 38; i < 54; i++)
            {
                datas[i] = 0;
            }

            ///IMAGE
            int k = TAILLE_HEADER;
            int l = 0;
            int j = 0;
            while (k < datas.Length)
            {
                if (k + 2 < datas.Length)
                {
                    datas[k] = Convert.ToByte(Image[l, j].Bleu);
                    datas[k + 1] = Convert.ToByte(Image[l, j].Vert);
                    datas[k + 2] = Convert.ToByte(Image[l, j].Rouge);
                    k += 3;
                    j++;
                    if (j == biWidth)
                    {
                        int compt = 0;
                        while (compt != complement)
                        {
                            datas[k] = 0;
                            k++;
                            compt++;
                        }
                        j = 0;
                        l++;
                    }
                }
                else if (k + 1 < datas.Length)
                {
                    datas[k] = 0;
                    datas[k + 1] = 0;
                    k++;
                }
                else
                {
                    datas[k] = 0;
                    k++;
                }
            }
            File.WriteAllBytes(file, datas);

        }



        /// <summary>
        /// Convertit un tableau de byte en un int 
        /// </summary>
        /// <param tableau de byte="tab"></param>
        /// <returns>un nombre entier</returns>
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            int valeur = tab[0];
            for (int i = 1; i < tab.Length; i++)
            {
                valeur += tab[i] * Tools.puissance(i, 256);
            }
            return valeur;
        }



        /// <summary>
        /// Convertit un int en tableau de byte
        /// </summary>
        /// <param entier ="val"></param>
        /// <returns>tableau de byte de taille nboctets</returns>
        public byte[] Convertir_Int_To_Endian(int val, int nboctets)
        {
            byte[] b = new byte[nboctets];
            int tmp = val;
            int k = nboctets - 1;
            while (k > 0)
            {
                tmp = val / Tools.BinaryPow(k);
                if (tmp < 1)
                {
                    b[k] = 0;
                    if (k == 1)
                    {
                        b[0] = Convert.ToByte(val % Tools.BinaryPow(k));
                    }
                }
                else if (k != 1)
                {
                    b[k] = Convert.ToByte(tmp);
                    val = val % Tools.BinaryPow(k);


                }
                else if (k == 1)
                {
                    b[k] = Convert.ToByte(tmp);
                    b[0] = Convert.ToByte(val % Tools.BinaryPow(k));

                }
                k--;
            }
            return b;
        }



        /// <summary>
        /// Permet de lire sur la console les données d'une image.
        /// </summary>
        /// <returns>chaine de caracteres </returns>
        public string To_String()
        {
            string affiche = "";
            affiche += "HEADER\n"
                        + bfType + " " + bfSize + "\n"
                        + "INFOS\n" +
                        +biWidth + " " + biHeight + " " + biBitCount + "\n";
            affiche += "IMAGE\n";
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    affiche += Image[i, j].To_String() + " ";
                }
                affiche += "\n";
            }
            return affiche;


        }




        /////////////////////////////TRAITEMENT DE L IMAGE//////////////////////////////////////////
        /// <summary>
        /// Transforme une image bmp en degré de gris
        /// </summary>
        /// <param nom du nouveau fichier="name_out"></param>
        public void Color_To_Grey(string name_out)
        {
            Pixel[,] inter = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    inter[i, j] = new Pixel(Image[i, j]);
                    Image[i, j] = new Pixel(Image[i, j].Moyenne(), Image[i, j].Moyenne(), Image[i, j].Moyenne());
                }
            }
            From_Image_To_File(name_out);
            Image = inter;
        }



        /// <summary>
        /// Transforme une image bmp en noir et blanc
        /// </summary>
        /// <param nom du nouveau fichier="name_out"></param>
        public void Color_To_Black(string name_out)
        {
            Pixel[,] inter = new Pixel[biHeight, biWidth];
            int seuil = 128;
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    inter[i, j] = new Pixel(Image[i, j]);
                    if (Image[i, j].Rouge < seuil)
                    {
                        Image[i, j] = new Pixel(0, 0, 0);
                    }
                    else
                    {
                        Image[i, j] = new Pixel(255, 255, 255);
                    }
                }
            }
            From_Image_To_File(name_out);
            Image = inter;
        }

        /// <summary>
        /// Agrandi une image en utilisant la méthode de l'interpolation bilinéaire
        /// </summary>
        /// <param degré d'aggrandissement="coeff"></param>
        public void Agrandir(int coeff)
        {
            Pixel[,] Matrice = new Pixel[biHeight * coeff, biWidth * coeff];
            for (int i = 0; i < biHeight - 1; i++)
            {
                for (int j = 0; j < biWidth - 1; j++)
                {
                    int x = i * coeff;
                    int y = j * coeff;
                    int x1 = (i + 1) * coeff;
                    int y1 = (j + 1) * coeff;

                    Matrice[x, y] = new Pixel(Image[i, j]);
                    Matrice[x1, y] = new Pixel(Image[i + 1, j]);
                    Matrice[x, y1] = new Pixel(Image[i, j + 1]);
                    Matrice[x1, y1] = new Pixel(Image[i + 1, j + 1]);


                    for (int k = 1; k < coeff; k++)
                    {
                        Matrice[x, y + k] = new Pixel(Matrice[x, y].interpolation(y1, y, k, Matrice[x, y1]));
                        Matrice[x1, y + k] = new Pixel(Matrice[x1, y].interpolation(y1, y, k, Matrice[x1, y1]));

                        Matrice[x + k, y] = new Pixel(Matrice[x, y].interpolation(x1, x, k, Matrice[x1, y]));
                        Matrice[x + k, y1] = new Pixel(Matrice[x, y1].interpolation(x1, x, k, Matrice[x1, y1]));
                        for (int l = 1; l < coeff; l++)
                        {
                            Matrice[x + l, y + k] = new Pixel(Matrice[x, y + k].interpolation(x1, x, l, Matrice[x1, y + k]));
                        }

                    }
                }
            }
            for (int l = 0; l < biHeight * coeff - coeff + 1; l++)
            {
                for (int n = 1; n < coeff; n++)
                {
                    Matrice[l, (biWidth - 1) * coeff + n] = new Pixel(Matrice[l, (biWidth - 1) * coeff]);
                }
            }
            for (int s = 0; s < biWidth * coeff; s++)
            {
                for (int t = 1; t < coeff; t++)
                {
                    Matrice[(biHeight - 1) * coeff + t, s] = new Pixel(Matrice[(biHeight - 1) * coeff, s]);
                }
            }
            this.bfSize = biHeight * biWidth * (coeff * coeff) * 3 + 54;
            this.biHeight *= coeff;
            this.biWidth *= coeff;
            Image = Matrice;
        }

        /// <summary>
        /// Rétrécit une image 
        /// </summary>
        /// <param coefficient de rétrécissement="coeff"></param>
        public void Retrecir(int coeff)
        {
            Pixel[,] Matrice = new Pixel[biHeight / coeff, biWidth / coeff];
            for (int i = 0; i < biHeight / coeff; i++)
            {
                for (int j = 0; j < biWidth / coeff; j++)
                {

                    Matrice[i, j] = new Pixel(Image[i * coeff, j * coeff]);
                }
            }

            this.biHeight /= coeff;
            this.biWidth /= coeff;
            this.bfSize = biHeight * biWidth * 3 + 54;
            Image = Matrice;
        }

        /// <summary>
        /// Effectue une rotation centrale de l'image
        /// </summary>
        /// <param name="angle"></param>
        public void Rotation(double angle)
        {
            Pixel[,] Matrice = new Pixel[biHeight, biWidth];
            Pixel[,] tmp = new Pixel[biHeight, biWidth];
            double angle_cos = Math.Cos(angle * Math.PI / 180);
            double angle_sin = Math.Sin(angle * Math.PI / 180);
            int y0 = (biWidth / 2);
            int x0 = (biHeight / 2);
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    tmp[i, j] = new Pixel(Image[i, j]);
                    int p = Convert.ToInt32(angle_cos * (i - x0) + angle_sin * (j - y0)) + x0;
                    int q = Convert.ToInt32(angle_cos * (j - y0) - angle_sin * (i - x0)) + y0;
                    if ((((p < biHeight) && (p >= 0)) && ((q < biWidth) && (q >= 0))) && (Matrice[p, q] == null))
                    {
                        Matrice[p, q] = new Pixel(Image[i, j]);
                    }
                }
            }
            for (int p = 0; p < biHeight; p++)
            {
                for (int q = 0; q < biWidth; q++)
                {
                    if (Matrice[p, q] == null)
                    {
                        Matrice[p, q] = new Pixel(0, 0, 0);
                    }
                }
            }
            Image = Matrice;
            From_Image_To_File("Rotation1.bmp");
            Process.Start("Rotation1.bmp");
            Image = tmp;

        }

        public void effet_miroir_V(string name_out)
        {
            Pixel[,] tmp = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    tmp[i, j] = new Pixel(Image[i, biWidth - (j + 1)]);
                }
            }
            Image = tmp;
            From_Image_To_File(name_out);
            Process.Start(name_out);

        }



        /// <summary>
        /// Applique un effet miroir horizontale sur une image
        /// </summary>
        /// <param nom du fichier de sortie="name_out"></param>
        public void effet_miroir_H(string name_out)
        {
            Pixel[,] tmp = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    tmp[i, j] = new Pixel(Image[biHeight - (i + 1), j]);

                }
            }
            Image = tmp;
            From_Image_To_File(name_out);
            Process.Start(name_out);
        }



        //////////////////////////////////CONVOLUTION////////////////////////////////////////////////////////////////

        /// <summary>
        /// Algorithme pour la convolution selon le noyau
        /// </summary>
        /// <param dimension du noyau="taille"></param>
        /// <param filtre applicateur ="noyau"></param>
        public void Convolution(int taille, int[,] noyau)
        {

            Pixel[,] Destination = new Pixel[biHeight, biWidth];
            for (int i = taille / 2; i < biHeight - taille / 2; i++)
            {
                for (int j = taille / 2; j < biWidth - taille / 2; j++)
                {
                    Destination[i, j] = new Pixel(Image[i, j].filtrage(Image, noyau, i, j));
                }
            }
            for (int k = 0; k < taille / 2; k++)
            {
                for (int l = 0; l < biWidth; l++)
                {
                    if ((l >= taille / 2) && (l < biWidth - taille / 2))
                    {
                        Destination[k, l] = new Pixel(Destination[taille / 2, l]);
                        Destination[biHeight - (k + 1), l] = new Pixel(Destination[biHeight - 1 - taille / 2, l]); ;
                    }
                    else if (l < biWidth / 2)
                    {
                        Destination[k, l] = new Pixel(Destination[taille / 2, taille / 2]);
                        Destination[biHeight - (k + 1), l] = new Pixel(Destination[biHeight - taille / 2 - 1, taille / 2]);
                    }
                    else
                    {
                        Destination[k, l] = new Pixel(Destination[taille / 2, biWidth - taille / 2 - 1]);
                        Destination[biHeight - (k + 1), l] = new Pixel(Destination[biHeight - taille / 2 - 1, biWidth - taille / 2 - 1]);
                    }
                }
                for (int l = 0; l < biHeight; l++)
                {
                    if ((l >= taille / 2) && (l <= biHeight - 1 - taille / 2))
                    {
                        Destination[l, k] = new Pixel(Destination[l, taille / 2]);
                        Destination[l, biWidth - (k + 1)] = new Pixel(Destination[l, biWidth - 1 - taille / 2]);
                    }
                }

            }

            Image = Destination;

        }

        /// <summary>
        /// filtre laplacien, détection de contour
        /// </summary>
        /// <param taille du filtre="taille"></param>
        public void detection_contour(int taille)
        {

            Pixel[,] tmp = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    tmp[i, j] = new Pixel(Image[i, j]);

                }
            }
            if (taille == 3)
            {
                int[,] noyau = new int[3, 3] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                Convolution(3, noyau);
                From_Image_To_File("detection_Contour3x3.bmp");
                Process.Start("detection_Contour3x3.bmp");
            }
            else
            {
                int[,] noyau = new int[5, 5] { { 0, 0, -1, 0, 0 }, { 0, -1, -2, -1, 0 }, { -1, -2, 16, -2, -1 }, { 0, -1, -2, -1, 0 }, { 0, 0, -1, 0, 0 } };
                Convolution(5, noyau);
                From_Image_To_File("detection_Contour5x5.bmp");
                Process.Start("detection_Contour5x5.bmp");
            }
            Image = tmp;

        }

        /// <summary>
        /// filtre pour renforcer les bords
        /// </summary>
        public void renforcement_bords()
        {
            Pixel[,] tmp = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    tmp[i, j] = new Pixel(Image[i, j]);

                }
            }
            int[,] noyau = new int[3, 3] { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
            Convolution(3, noyau);
            From_Image_To_File("renforcement.bmp");
            Process.Start("renforcement.bmp");
            Image = tmp;

        }

        /// <summary>
        /// filtre gaussien pour rendre l'image flou
        /// </summary>
        /// <param dimension du noyau="taille"></param>
        public void Flou(int taille)
        {
            Pixel[,] tmp = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    tmp[i, j] = new Pixel(Image[i, j]);

                }
            }
            if (taille == 3)
            {
                int[,] noyau = new int[3, 3] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
                Convolution(3, noyau);
                From_Image_To_File("Flou3x3.bmp");
                Process.Start("Flou3x3.bmp");
            }
            else
            {
                int[,] noyau = new int[5, 5] { { 1, 4, 6, 4, 1 }, { 4, 16, 24, 16, 4 }, { 6, 24, 36, 24, 6 }, { 4, 16, 24, 16, 4 }, { 1, 4, 6, 4, 1 } };
                Convolution(5, noyau);
                From_Image_To_File("Flou5x5.bmp");
                Process.Start("Flou5x5.bmp");
            }
            Image = tmp;

        }

        /// <summary>
        /// filtre pour le repoussage
        /// </summary>
        public void Repoussage()
        {
            Pixel[,] tmp = new Pixel[biHeight, biWidth];
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    tmp[i, j] = new Pixel(Image[i, j]);

                }
            }
            int[,] noyau = new int[3, 3] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            Convolution(3, noyau);
            From_Image_To_File("Repoussage.bmp");
            Process.Start("Repoussage.bmp");
            Image = tmp;
        }




        //////////////////////////////////////////STEGANOGRAPHIE//////////////////////////////////////////////////////       


        /// <summary>
        /// dissimule une image dans une autre
        /// </summary>
        /// <param image à cacher="melange"></param>
        /// <param nouveau fichier="name_new"></param>
        /// <param zone X ou on la cache ="HGX"></param>
        /// <param zone Y ou on la cache ="HGY"></param>
        public void dissimuler(MyImage melange, string name_new, int HGX, int HGY)
        {
            if (HGX + melange.Height > this.biHeight)
            {
                Console.WriteLine("Impossible de placer l'image ici, nouvelle coordonée en X?");
                HGX = Convert.ToInt32(Console.ReadLine());
            }
            if (HGY + melange.Width > this.biWidth)
            {
                Console.WriteLine("Impossible de placer l'image ici, nouvelle coordonée en Y?");
                HGY = Convert.ToInt32(Console.ReadLine());
            }

            Pixel[,] newImage = new Pixel[biHeight, biWidth];

            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    if (((i < HGX) || (i >= HGX + melange.Height)) || (((j < HGY) || (j >= HGY + melange.Width))))
                    {
                        newImage[i, j] = new Pixel(Image[i, j]);
                    }
                    else

                    {
                        byte Rouge1 = Convert.ToByte(Image[i, j].Rouge);
                        byte Vert1 = Convert.ToByte(Image[i, j].Vert);
                        byte Bleu1 = Convert.ToByte(Image[i, j].Bleu);

                        byte Rouge2 = Convert.ToByte(melange.image[i - HGX, j - HGY].Rouge);
                        byte Vert2 = Convert.ToByte(melange.image[i - HGX, j - HGY].Vert);
                        byte Bleu2 = Convert.ToByte(melange.image[i - HGX, j - HGY].Bleu);

                        bool[] valR = new bool[] { Tools.GetBit_byte(Rouge1, 7), Tools.GetBit_byte(Rouge1, 6), Tools.GetBit_byte(Rouge1, 5), Tools.GetBit_byte(Rouge1, 4),
                                                              Tools.GetBit_byte(Rouge2, 7), Tools.GetBit_byte(Rouge2, 6), Tools.GetBit_byte(Rouge2, 5), Tools.GetBit_byte(Rouge2, 4) };
                        bool[] valV = new bool[] { Tools.GetBit_byte(Vert1, 7), Tools.GetBit_byte(Vert1, 6), Tools.GetBit_byte(Vert1, 5), Tools.GetBit_byte(Vert1, 4),
                                                              Tools.GetBit_byte(Vert2, 7), Tools.GetBit_byte(Vert2, 6), Tools.GetBit_byte(Vert2, 5), Tools.GetBit_byte(Vert2, 4) };
                        bool[] valB = new bool[] { Tools.GetBit_byte(Bleu1, 7), Tools.GetBit_byte(Bleu1, 6), Tools.GetBit_byte(Bleu1, 5), Tools.GetBit_byte(Bleu1, 4),
                                                              Tools.GetBit_byte(Bleu2, 7), Tools.GetBit_byte(Bleu2, 6), Tools.GetBit_byte(Bleu2, 5), Tools.GetBit_byte(Bleu2, 4) };
                        Image[i, j] = new Pixel(Tools.Bool_to_Int(valR, 7), Tools.Bool_to_Int(valV, 7), Tools.Bool_to_Int(valB, 7));

                        Image[i, j] = new Pixel(Tools.Bool_to_Int(valR, 7), Tools.Bool_to_Int(valV, 7), Tools.Bool_to_Int(valB, 7));
                        Image[i, j].Cache = true;

                    }
                }

            }


            From_Image_To_File(name_new);
        }

        /// <summary>
        /// décode une image 
        /// </summary>
        /// <param nom du fichier à decoder="name"></param>


        public void decode_im(string name)
        {
            Pixel[,] intercache = new Pixel[biHeight, biWidth];
            List<Pixel> cache = new List<Pixel>();
            int countColonne = 0;
            int Colonne = 0;
            int countLigne = 0;
            int Ligne = 0;
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    if (Image[i, j].Cache)
                    {

                        byte Rouge = Convert.ToByte(Image[i, j].Rouge);
                        byte Vert = Convert.ToByte(Image[i, j].Vert);
                        byte Bleu = Convert.ToByte(Image[i, j].Bleu);
                        bool[] valR2 = new bool[] { Tools.GetBit_byte(Rouge, 3), Tools.GetBit_byte(Rouge, 2), Tools.GetBit_byte(Rouge, 1), Tools.GetBit_byte(Rouge, 0), false, false, false, false };
                        bool[] valV2 = new bool[] { Tools.GetBit_byte(Vert, 3), Tools.GetBit_byte(Vert, 2), Tools.GetBit_byte(Vert, 1), Tools.GetBit_byte(Vert, 0), false, false, false, false };
                        bool[] valB2 = new bool[] { Tools.GetBit_byte(Bleu, 3), Tools.GetBit_byte(Bleu, 2), Tools.GetBit_byte(Bleu, 1), Tools.GetBit_byte(Bleu, 0), false, false, false, false };
                        bool[] valR1 = new bool[] { Tools.GetBit_byte(Rouge, 7), Tools.GetBit_byte(Rouge, 6), Tools.GetBit_byte(Rouge, 5), Tools.GetBit_byte(Rouge, 4), false, false, false, false };
                        bool[] valV1 = new bool[] { Tools.GetBit_byte(Vert, 7), Tools.GetBit_byte(Vert, 6), Tools.GetBit_byte(Vert, 5), Tools.GetBit_byte(Vert, 4), false, false, false, false };
                        bool[] valB1 = new bool[] { Tools.GetBit_byte(Bleu, 7), Tools.GetBit_byte(Bleu, 6), Tools.GetBit_byte(Bleu, 5), Tools.GetBit_byte(Bleu, 4), false, false, false, false };

                        Image[i, j] = new Pixel(Tools.Bool_to_Int(valR1, 7), Tools.Bool_to_Int(valV1, 7), Tools.Bool_to_Int(valB1, 7));
                        Pixel decode = new Pixel(Tools.Bool_to_Int(valR2, 7), Tools.Bool_to_Int(valV2, 7), Tools.Bool_to_Int(valB2, 7));
                        cache.Add(decode);
                        countColonne++;
                        if (!Image[i, j + 1].Cache)
                        {
                            if (Colonne == 0)
                            {
                                Colonne = countColonne;
                            }

                            countLigne++;
                        }
                    }
                }

            }
            Ligne = countLigne;
            Console.WriteLine(Colonne + " " + Ligne + " " + cache.Count);
            From_Image_To_File("decode.bmp");
            Process.Start("decode.bmp");
            biWidth = Colonne;
            biHeight = Ligne;
            bfSize = biHeight * biWidth * 3 + 54;
            int Count = 0;
            for (int i = 0; i < biHeight; i++)
            {
                for (int j = 0; j < biWidth; j++)
                {
                    Image[i, j] = cache[Count];
                    Count++;
                }
            }
            From_Image_To_File("image_cache.bmp");
            Process.Start("image_cache.bmp");
        }




        //////////////////////////////////////////////////QRCODE//////////////////////////////////////////////////////      

        /// <summary>
        /// Créer le pattern bas gauche pour le QRcode
        /// </summary>
        /// <param pixel noir="black"></param>
        /// <param pixel blanc="White"></param>
        public void patternBG(Pixel black, Pixel White)
        {

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i == 0) || (i == 6))
                    {
                        Image[i, j] = new Pixel(black);
                    }
                    else if ((i == 1) || (i == 5))
                    {
                        if ((j >= 1) && (j <= 5))
                        {
                            Image[i, j] = new Pixel(White);
                        }
                        else
                        {
                            Image[i, j] = new Pixel(black);
                        }
                    }
                    else if ((i >= 2) && (i <= 4))
                    {
                        if ((j == 1) || (j == 5))
                        {
                            Image[i, j] = new Pixel(White);
                        }
                        else
                        {
                            Image[i, j] = new Pixel(black);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cree le pattern en haut a droite
        /// </summary>
        /// <param pixel noir="black"></param>
        /// <param pixel blanc="White"></param>
        public void patternHD(Pixel black, Pixel White)
        {
            for (int i = biWidth - 7; i < biWidth; i++)
            {
                for (int j = biWidth - 7; j < biWidth; j++)
                {
                    if ((i == biWidth - 7) || (i == biWidth - 1))
                    {
                        Image[i, j] = new Pixel(black);
                    }
                    else if ((i == biWidth - 6) || (i == biWidth - 2))
                    {

                        if ((j >= biWidth - 6) && (j <= biWidth - 2))
                        {
                            Image[i, j] = new Pixel(White);
                        }
                        else
                        {
                            Image[i, j] = new Pixel(black);
                        }
                    }
                    else if ((i >= biWidth - 5) && (i <= biWidth - 3))
                    {
                        if ((j == biWidth - 6) || (j == biWidth - 2))
                        {
                            Image[i, j] = new Pixel(White);
                        }
                        else
                        {
                            Image[i, j] = new Pixel(black);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cree le pattern en haut à gauche
        /// </summary>
        /// <param pixel noir="black"></param>
        /// <param pixel blanc="White"></param>
        public void patternHG(Pixel black, Pixel White)
        {
            for (int i = biWidth - 7; i < biWidth; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i == biWidth - 7) || (i == biWidth - 1))
                    {
                        Image[i, j] = new Pixel(black);
                    }
                    else if ((i == biWidth - 6) || (i == biWidth - 2))
                    {
                        if ((j >= 1) && (j <= 5))
                        {
                            Image[i, j] = new Pixel(White);
                        }
                        else
                        {
                            Image[i, j] = new Pixel(black);
                        }
                    }
                    else if ((i >= biWidth - 5) && (i <= biWidth - 3))
                    {

                        if ((j == 1) || (j == 5))
                        {
                            Image[i, j] = new Pixel(White);
                        }
                        else
                        {
                            Image[i, j] = new Pixel(black);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Separateur QRcode
        /// </summary>
        /// <param pixel noir="black"></param>
        /// <param pixel blanc="White"></param>
        public void separateur(Pixel black, Pixel White)
        {
            for (int i = 0; i < 8; i++)
            {
                Image[i, 7] = new Pixel(White);
                Image[biWidth - (i + 1), biWidth - 8] = new Pixel(White);
                Image[biWidth - (i + 1), 7] = new Pixel(White);
            }

            for (int j = 0; j < 8; j++)
            {
                Image[7, j] = new Pixel(White);
                Image[biWidth - 8, biWidth - (j + 1)] = new Pixel(White);
                Image[biWidth - 8, j] = new Pixel(White);
            }
        }

        /// <summary>
        /// Alignement Version 2
        /// </summary>
        /// <param pixel noir="black"></param>
        /// <param pixel blanc ="White"></param>
        public void alignement(Pixel black, Pixel White)
        {
            for (int i = 4; i <= 8; i++)
            {
                for (int j = 16; j <= 20; j++)
                {
                    if (((i == 6) && (j == 18)) || ((i == 4) || (i == 8)) || ((j == 16) || (j == 20)))
                    {
                        Image[i, j] = new Pixel(black);
                    }
                    else
                    {
                        Image[i, j] = new Pixel(White);
                    }


                }
            }
        }

        /// <summary>
        /// timing_pattern
        /// </summary>
        /// <param pixel noir="black"></param>
        /// <param pixel blanc="White"></param>
        public void timing_pattern(Pixel black, Pixel White)
        {
            for (int i = 8; i < biWidth - 8; i += 2)
            {
                Image[i, 6] = new Pixel(black);
                Image[i + 1, 6] = new Pixel(White);
                Image[biWidth - 7, i] = new Pixel(black);
                Image[biWidth - 7, i + 1] = new Pixel(White);
            }
        }

        /// <summary>
        /// zone reserver au masque
        /// </summary>
        /// <param pixel noir ="black"></param>
        /// <param pixel blanc="White"></param>
        /// <param Qrcode ="Genere"></param>
        public void reserved_area(Pixel black, Pixel White, QRcode Genere)
        {
            int compteur = 0;
            for (int i = 0; i < biWidth; i++)
            {
                if ((((i >= 0) && (i < 9)) || ((i >= biWidth - 8) && (i < biWidth))) && (i != 6))
                {
                    if (Genere.Mask[compteur])
                    {
                        Image[biWidth - 9, i] = black;
                    }
                    else
                    {
                        Image[biWidth - 9, i] = White;
                    }
                    if ((compteur < Genere.Mask.Length - 1) && (i != 8))
                    {
                        compteur++;
                    }
                }

            }
            compteur = 0;
            for (int i = 0; i < biWidth; i++)
            {
                if ((((i >= 0) && (i < 7)) || ((i >= biWidth - 9) && (i < biWidth))) && (i != biWidth - 7))
                {
                    if (Genere.Mask[compteur])
                    {
                        Image[i, 8] = black;
                    }
                    else
                    {
                        Image[i, 8] = White;
                    }
                    if ((compteur < Genere.Mask.Length - 1) && (i != 8))
                    {
                        compteur++;
                    }
                }

            }
        }

        /// <summary>
        /// Algorithme pour place les données
        /// </summary>
        /// <param pixel noir="black"></param>
        /// <param pixel blanc="White"></param>
        /// <param Qrcode="traitement"></param>
        public void placing_data(Pixel black, Pixel White, QRcode traitement)
        {
            int compteur = 0;
            int j = biWidth - 1;
            bool montee = true;
            while ((j >= 0) && (compteur < traitement.Data.Count))
            {
                if (montee)
                {
                    for (int i = 0; i < biWidth; i++)
                    {
                        if (compteur >= traitement.Data.Count)
                        {
                            break;
                        }
                        if ((traitement.Data[compteur]) && (Image[i, j] == null))
                        {
                            if ((i + j) % 2 == 0)
                            {
                                Image[i, j] = new Pixel(White);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(black);
                                compteur++;
                            }
                        }
                        else if ((!traitement.Data[compteur]) && (Image[i, j] == null))
                        {
                            if ((i + j) % 2 == 0)
                            {
                                Image[i, j] = new Pixel(black);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(White);
                                compteur++;
                            }
                        }
                        if (compteur >= traitement.Data.Count)
                        {
                            break;
                        }
                        if ((traitement.Data[compteur]) && (Image[i, j - 1] == null))
                        {
                            if ((i + (j - 1)) % 2 == 0)
                            {
                                Image[i, j - 1] = new Pixel(White);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j - 1] = new Pixel(black);
                                compteur++;
                            }
                        }
                        else if ((!traitement.Data[compteur]) && (Image[i, j - 1] == null))
                        {
                            if ((i + (j - 1)) % 2 == 0)
                            {
                                Image[i, j - 1] = new Pixel(black);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j - 1] = new Pixel(White);
                                compteur++;
                            }
                        }

                    }
                    j -= 2;
                    if (j == 6)
                    {
                        j = 5;
                    }
                    montee = false;
                }
                else
                {
                    for (int i = biWidth - 1; i >= 0; i--)
                    {
                        if (compteur >= traitement.Data.Count)
                        {
                            break;
                        }
                        if ((traitement.Data[compteur]) && (Image[i, j] == null))
                        {
                            if ((i + j) % 2 == 0)
                            {
                                Image[i, j] = new Pixel(White);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(black);
                                compteur++;
                            }

                        }
                        else if ((!traitement.Data[compteur]) && (Image[i, j] == null))
                        {
                            if ((i + j) % 2 == 0)
                            {
                                Image[i, j] = new Pixel(black);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(White);
                                compteur++;
                            }
                        }
                        if (compteur >= traitement.Data.Count)
                        {
                            break;
                        }
                        if ((traitement.Data[compteur]) && (Image[i, j - 1] == null))
                        {
                            if ((i + (j - 1)) % 2 == 0)
                            {
                                Image[i, j - 1] = new Pixel(White);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j - 1] = new Pixel(black);
                                compteur++;
                            }
                        }
                        else if ((!traitement.Data[compteur]) && (Image[i, j - 1] == null))
                        {
                            if ((i + (j - 1)) % 2 == 0)
                            {
                                Image[i, j - 1] = new Pixel(black);
                                compteur++;
                            }
                            else
                            {
                                Image[i, j - 1] = new Pixel(White);
                                compteur++;
                            }
                        }
                    }
                    j -= 2;
                    montee = true;
                }
            }

        }
    }
}
