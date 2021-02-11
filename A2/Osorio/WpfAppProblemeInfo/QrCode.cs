using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfAppProblemeInfo
{
    class QRcode
    {
        List<bool> QRdata;

        string code;
        int version;
        int SizeBit;
        char correction;
        int EC;
        bool[] mode;
        bool[] nbCaract;
        bool[] LastLetter;
        bool[] Terminator;
        const int NBCARMAX1 = 25;
        const int NBCARMAX2 = 47;
        const int NBDATABIT = 11;
        const int nbBitCount = 9;
        bool[] TOFILL1 = { true, true, true, false, true, true, false, false };
        bool[] TOFIIL2 = { false, false, false, true, false, false, false, true };

        /// <summary>
        /// Générateur de QR CODE à partir d'un texte en majuscule
        /// </summary>
        /// <param code d'entrée="texte"></param>
        /// <param type de correction="Correction"></param>
        public QRcode(string texte, char Correction)
        {
            QRdata = new List<bool>();


            this.code = texte;

            ///STEP 1 CHOOSING CORRECTION LEVEL

            this.correction = Correction;

            /// STEP 2 on étudie la version que l'on choisit
            if (texte.Length <= NBCARMAX1)
            {
                this.version = 1;
                EC = 7;

            }
            else if ((texte.Length > NBCARMAX1) && (texte.Length <= NBCARMAX2))
            {
                this.version = 2;
                EC = 10;
            }


            //STEP 3 on définit le mode
            // on définit le mode alphanum
            mode = new bool[] { false, false, true, false };
            AddToData(mode, QRdata);

            //STEP 4 caract Count indicator
            //on compte le nombre de caract.
            nbCaract = new bool[nbBitCount];
            nbCaract = Tools.int_to_bit(texte.Length, nbBitCount);
            AddToData(nbCaract, QRdata);


            ///Lecture de la chaine de caractères

            //STEP 5 on lit les données de la chaines
            if (texte.Length % 2 == 0)//paire
            {
                int k = 0;
                for (int i = 0; i < texte.Length / 2; i++)
                {
                    bool[] lettres = new bool[NBDATABIT];
                    lettres = Tools.int_to_bit(paire_caract(texte[k], texte[k + 1]), NBDATABIT);
                    for (int j = 0; j < NBDATABIT; j++)
                    {
                        QRdata.Add(lettres[j]);
                    }
                    k += 2;
                }
                LastLetter = null;
            }
            else //impaire
            {
                int k = 0;
                for (int i = 0; i < texte.Length / 2; i++)
                {
                    bool[] lettres = new bool[NBDATABIT];
                    lettres = Tools.int_to_bit(paire_caract(texte[k], texte[k + 1]), NBDATABIT);
                    for (int j = 0; j < NBDATABIT; j++)
                    {
                        QRdata.Add(lettres[j]);
                    }
                    k += 2;
                }
                bool[] lettresf = new bool[6];
                LastLetter = new bool[6];
                lettresf = Tools.int_to_bit(lecture_char(texte.Last()), 6);
                for (int j = 0; j < 6; j++)
                {


                    LastLetter[j] = lettresf[j];
                    QRdata.Add(lettresf[j]);

                }


            }


            //STEP 6 TERMINAISON


            this.SizeBit = NbBitQR(this.version, this.correction);

            if ((SizeBit - QRdata.Count) >= 4)
            {
                Terminator = new bool[] { false, false, false, false };
            }
            else
            {
                Terminator = new bool[SizeBit - QRdata.Count];
                for (int i = 0; i < SizeBit - QRdata.Count; i++)
                {
                    Terminator[i] = false;
                }
            }
            AddToData(Terminator, QRdata);



            //STEP7 MULTIPLE DE 8

            while (QRdata.Count % 8 != 0)
            {
                QRdata.Add(false);
            }

            //STEP 8 ON REMPLIT JUSQU'AU MAX

            int NbByteToFill = (SizeBit - QRdata.Count) / 8;

            for (int i = 0; i < NbByteToFill; i++)
            {
                if (i % 2 == 0)
                {
                    AddToData(TOFILL1, QRdata);
                }
                else
                {
                    AddToData(TOFIIL2, QRdata);
                }
            }
            ///STEP9 CORRECTION

            correction_erreur();

        }

        /// <summary>
        /// Lecteur de QR Code à partir d'une image bmp;
        /// </summary>
        /// <param nom du fichier de l'image="Scanner"></param>
        public QRcode(MyImage Scanner)
        {
            //  Scanner.From_Image_To_File("inter1.bmp");
            // Process.Start("inter1.bmp");
            bool alignement = true;
            if (Scanner.image.GetLength(0) == 25)
            {
                alignement = alignement_check(Scanner.image);
                this.version = 2;
                this.EC = 10;
            }
            else
            {
                this.version = 1;
                this.EC = 7;
            }
            if ((Pattern_Coin(Scanner.image)) && (Separator(Scanner.image)) && (timing_pattern(Scanner.image)) && (alignement))
            {
                lecture_mask(Scanner.image);
                Scanner.image[7, 8] = new Pixel(150, 150, 150);
                this.QRdata = lecture_Data(Scanner.image);
                bool[] correction = new bool[8 * EC];
                Console.WriteLine(QRdata.Count);
                for (int i = QRdata.Count - 1; i >= QRdata.Count - 8 * EC; i--)
                {
                    correction[i - QRdata.Count + 8 * EC] = QRdata[i];
                }
                byte[] erreur = new byte[EC];
                int j = 0;
                int coeff = 0;
                bool[] octet = new bool[8];
                for (int i = 0; i <= correction.Length; i++)
                {
                    if (((i % 8) == 0) && (i != 0))
                    {
                        j = 0;
                        erreur[coeff] = Convert.ToByte(Tools.Bool_to_Int(octet, 7));
                        coeff++;
                    }
                    if (i != correction.Length)
                    {
                        octet[j] = correction[i];
                        j++;
                    }

                }
                byte[] Message = new byte[(QRdata.Count - 8 * EC) / 8];
                j = 0;
                coeff = 0;
                for (int i = 0; i <= QRdata.Count - 8 * EC; i++)
                {
                    if (((i % 8) == 0) && (i != 0))
                    {
                        j = 0;
                        Message[coeff] = Convert.ToByte(Tools.Bool_to_Int(octet, 7));
                        Console.WriteLine(Message[coeff]);
                        coeff++;
                    }
                    if (i != QRdata.Count - 8 * EC)
                    {
                        octet[j] = QRdata[i];
                        j++;
                    }
                }
                byte[] MessageCorrige = ReedSolomonAlgorithm.Decode(Message, erreur, ErrorCorrectionCodeType.QRCode);
                List<bool> MessageFinal = new List<bool>();
                for (int i = 0; i < MessageCorrige.Length; i++)
                {
                    bool[] mot = Tools.int_to_bit(Convert.ToInt32(MessageCorrige[i]), 8);
                    AddToData(mot, MessageFinal);
                }

                nbCaract = new bool[nbBitCount];
                for (int i = 4; i < 13; i++)
                {
                    nbCaract[i - 4] = MessageFinal[i];
                    Console.WriteLine(i);
                }
                int Taille = Tools.Bool_to_Int(nbCaract, 8);
                Console.WriteLine("coucouefef" + Taille);
                if (Taille % 2 == 0)
                {
                    int[] tabpaire = new int[Taille / 2];
                    int count = 0;
                    int index = 0;
                    bool[] paire = new bool[11];
                    for (int i = 13; i <= (Taille / 2) * 11 + 13; i++)
                    {
                        if (((i - 13) % 11 == 0) && (i != 13))
                        {
                            index = 0;
                            tabpaire[count] = Tools.Bool_to_Int(paire, 10);
                            count++;
                        }
                        if (i != (Taille / 2) * 11 + 13)
                        {
                            paire[index] = MessageFinal[i];
                            index++;
                        }
                    }
                    int k = 0;
                    code = "";
                    for (int i = 0; i < tabpaire.Length; i++)
                    {
                        int a = tabpaire[i] / 45;
                        int b = tabpaire[i] % 45;
                        code += lecture_int_char(a);
                        code += lecture_int_char(b);
                        k += 2;
                    }
                }
                else
                {
                    int[] tabpaire = new int[Taille / 2 + 1];
                    int count = 0;
                    int index = 0;
                    bool[] paire = new bool[11];
                    for (int i = 13; i <= ((Taille / 2) * 11 + 13); i++)
                    {
                        if (((i - 13) % 11 == 0) && (i != 13))
                        {
                            index = 0;
                            tabpaire[count] = Tools.Bool_to_Int(paire, 10);
                            count++;
                        }
                        if (i != (Taille / 2) * 11 + 13)
                        {
                            paire[index] = MessageFinal[i];
                            index++;
                        }

                    }
                    bool[] lastLetter = new bool[6];
                    index = 0;
                    for (int i = ((Taille / 2) * 11 + 13); i < ((Taille / 2) * 11 + 19); i++)
                    {
                        if (MessageFinal[i])
                        {
                            Console.Write("1");
                        }
                        else
                        {
                            Console.Write("0");
                        }
                        Console.WriteLine();
                        lastLetter[index] = MessageFinal[i];
                        index++;
                    }
                    tabpaire[tabpaire.Length - 1] = Tools.Bool_to_Int(lastLetter, 5);
                    int k = 0;
                    code = "";
                    for (int i = 0; i < tabpaire.Length - 1; i++)
                    {
                        Console.WriteLine(tabpaire[i]);
                        int a = tabpaire[i] / 45;
                        int b = tabpaire[i] % 45;
                        code += lecture_int_char(a);
                        code += lecture_int_char(b);
                        k += 2;
                    }
                    code += lecture_int_char(tabpaire[tabpaire.Length - 1] % 45);
                }
            }

        }


        ///////////////////////////////////////////////PROPRIETES////////////////////
        public List<bool> Data
        {
            get
            {
                return QRdata;
            }
        }

        public int Version
        {
            get
            {
                return version;
            }
        }


        /// <summary>
        /// Trouve le caractere correspondant à une valeur int
        /// </summary>
        /// <param entier lu="valeur"></param>
        /// <returns></returns>
        public char lecture_int_char(int valeur)
        {
            char caract = ' ';
            switch (valeur)
            {
                case 0:
                    caract = '0';
                    break;
                case 1:
                    caract = '1';
                    break;
                case 2:
                    caract = '2';
                    break;
                case 3:
                    caract = '3';
                    break;
                case 4:
                    caract = '4';
                    break;
                case 5:
                    caract = '5';
                    break;
                case 6:
                    caract = '6';
                    break;
                case 7:
                    caract = '7';
                    break;
                case 8:
                    caract = '8';
                    break;
                case 9:
                    caract = '9';
                    break;
                case 10:
                    caract = 'A';
                    break;
                case 11:
                    caract = 'B';
                    break;
                case 12:
                    caract = 'C';
                    break;
                case 13:
                    caract = 'D';
                    break;
                case 14:
                    caract = 'E';
                    break;
                case 15:
                    caract = 'F';
                    break;
                case 16:
                    caract = 'G';
                    break;
                case 17:
                    caract = 'H';
                    break;
                case 18:
                    caract = 'I';
                    break;
                case 19:
                    caract = 'J';
                    break;
                case 20:
                    caract = 'K';
                    break;
                case 21:
                    caract = 'L';
                    break;
                case 22:
                    caract = 'M';
                    break;
                case 23:
                    caract = 'N';
                    break;
                case 24:
                    caract = 'O';
                    break;
                case 25:
                    caract = 'P';
                    break;
                case 26:
                    caract = 'Q';
                    break;
                case 27:
                    caract = 'R';
                    break;
                case 28:
                    caract = 'S';
                    break;
                case 29:
                    caract = 'T';
                    break;
                case 30:
                    caract = 'U';
                    break;
                case 31:
                    caract = 'V';
                    break;
                case 32:
                    caract = 'W';
                    break;
                case 33:
                    caract = 'X';
                    break;
                case 34:
                    caract = 'Y';
                    break;
                case 35:
                    caract = 'Z';
                    break;
                case 36:
                    caract = ' ';
                    break;
                case 37:
                    caract = '$';
                    break;
                case 38:
                    caract = '%';
                    break;
                case 39:
                    caract = '*';
                    break;
                case 40:
                    caract = '+';
                    break;
                case 41:
                    caract = '-';
                    break;
                case 42:
                    caract = '.';
                    break;
                case 43:
                    caract = '/';
                    break;
                case 44:
                    caract = ':';
                    break;
                default:
                    Console.WriteLine("VEUILLEZ VERIFIER QUE VOUS AVEZ BIEN RENTRER UN NOMBRE ENTRE 0 ET 9, DES MAJUSCULES ou les signes $%*+-./:");
                    break;

            }
            return caract;
        }

        /// <summary>
        /// Trouve la valeur entière d'un caractère
        /// </summary>
        /// <param  caractere d'entrée="caract"></param>
        /// <returns></returns>
        public int lecture_char(char caract)
        {
            int val = 0;

            switch (caract)
            {
                case '0':
                    val = 0;
                    break;
                case '1':
                    val = 1;
                    break;
                case '2':
                    val = 2;
                    break;
                case '3':
                    val = 3;
                    break;
                case '4':
                    val = 4;
                    break;
                case '5':
                    val = 5;
                    break;
                case '6':
                    val = 6;
                    break;
                case '7':
                    val = 7;
                    break;
                case '8':
                    val = 8;
                    break;
                case '9':
                    val = 9;
                    break;
                case 'A':
                    val = 10;
                    break;
                case 'B':
                    val = 11;
                    break;
                case 'C':
                    val = 12;
                    break;
                case 'D':
                    val = 13;
                    break;
                case 'E':
                    val = 14;
                    break;
                case 'F':
                    val = 15;
                    break;
                case 'G':
                    val = 16;
                    break;
                case 'H':
                    val = 17;
                    break;
                case 'I':
                    val = 18;
                    break;
                case 'J':
                    val = 19;
                    break;
                case 'K':
                    val = 20;
                    break;
                case 'L':
                    val = 21;
                    break;
                case 'M':
                    val = 22;
                    break;
                case 'N':
                    val = 23;
                    break;
                case 'O':
                    val = 24;
                    break;
                case 'P':
                    val = 25;
                    break;
                case 'Q':
                    val = 26;
                    break;
                case 'R':
                    val = 27;
                    break;
                case 'S':
                    val = 28;
                    break;
                case 'T':
                    val = 29;
                    break;
                case 'U':
                    val = 30;
                    break;
                case 'V':
                    val = 31;
                    break;
                case 'W':
                    val = 32;
                    break;
                case 'X':
                    val = 33;
                    break;
                case 'Y':
                    val = 34;
                    break;
                case 'Z':
                    val = 35;
                    break;
                case ' ':
                    val = 36;
                    break;
                case '$':
                    val = 37;
                    break;
                case '%':
                    val = 38;
                    break;
                case '*':
                    val = 39;
                    break;
                case '+':
                    val = 40;
                    break;
                case '-':
                    val = 41;
                    break;
                case '.':
                    val = 42;
                    break;
                case '/':
                    val = 43;
                    break;
                case ':':
                    val = 44;
                    break;
                default:
                    Console.WriteLine("VEUILLEZ VERIFIER QUE VOUS AVEZ BIEN RENTRER UN NOMBRE ENTRE 0 ET 9, DES MAJUSCULES ou les signes $%*+-./:");
                    break;

            }


            return val;
        }

        /// <summary>
        /// retourne le masque de type 0;
        /// </summary>
        public bool[] Mask { get; } = new bool[] { true, true, true, false, true, true, true, true, true, false, false, false, true, false, false };

        /// <summary>
        /// Renvoie le nombre de bits de données selon la correction (dans notre cas ce sera toujours L)
        /// </summary>
        /// <param Version du QR="version"></param>
        /// <param Type de correction="correction"></param>
        /// <returns></returns>
        public int NbBitQR(int version, char correction)
        {
            int Nb = 0;
            if (version == 1)
            {
                switch (correction)
                {
                    case 'L':
                        Nb = 19 * 8;
                        this.EC = 7;
                        break;
                    case 'M':
                        Nb = 16 * 8;
                        this.EC = 10;
                        break;
                    case 'Q':
                        Nb = 13 * 8;
                        this.EC = 13;
                        break;
                    case 'H':
                        Nb = 9 * 8;
                        this.EC = 17;
                        break;
                    default:
                        Console.WriteLine("error of correciton");
                        break;
                }
            }
            if (version == 2)
            {
                switch (correction)
                {
                    case 'L':
                        Nb = 34 * 8;
                        this.EC = 10;
                        break;
                    case 'M':
                        Nb = 28 * 8;
                        this.EC = 16;
                        break;
                    case 'Q':
                        Nb = 22 * 8;
                        this.EC = 22;
                        break;
                    case 'H':
                        Nb = 16 * 8;
                        this.EC = 28;
                        break;
                    default:
                        Console.WriteLine("error of correciton");
                        break;
                }
            }
            return Nb;
        }



        /// <summary>
        /// Renvoie la valeur entiere d'une paire de caractere
        /// </summary>
        /// <param premier indice="cara1"></param>
        /// <param deuxieme indice="cara2"></param>
        /// <returns></returns>
        public int paire_caract(char cara1, char cara2)
        {
            int val1 = lecture_char(cara1);
            int val2 = lecture_char(cara2);

            return Tools.puissance(1, 45) * val1 + Tools.puissance(0, 45) * val2;
        }

        /// <summary>
        /// Ajoute un tableau de booléen à la liste
        /// </summary>
        /// <param tableau de booléen="tab"></param>
        /// <param list a augmente="toAdd"></param>
        public void AddToData(bool[] tab, List<bool> toAdd)
        {
            foreach (bool element in tab)
            {
                toAdd.Add(element);
            }
        }

        /// <summary>
        /// Realise la correction d'erreur ENCODE
        /// </summary>
        public void correction_erreur()
        {
            byte[] Message = new byte[QRdata.Count / 8];
            int j = 0;
            int coeff = 0;
            bool[] octet = new bool[8];

            for (int i = 0; i <= QRdata.Count; i++)
            {
                if (((i % 8) == 0) && (i != 0))
                {
                    j = 0;
                    Message[coeff] = Convert.ToByte(Tools.Bool_to_Int(octet, 7));
                    Console.WriteLine(Message[coeff]);
                    coeff++;
                }
                if (i != QRdata.Count)
                {
                    octet[j] = QRdata[i];
                    j++;
                }
            }
            byte[] Correction = ReedSolomonAlgorithm.Encode(Message, EC, ErrorCorrectionCodeType.QRCode);
            for (int i = 0; i < Correction.Length; i++)
            {
                Console.WriteLine("erreur  " + Correction[i]);
                AddToData(Tools.int_to_bit(Convert.ToInt32(Correction[i]), 8), QRdata);
            }

        }

        /// <summary>
        /// Analyse l'image pour savoir si les patterns sont présents
        /// </summary>
        /// <param Image="Image"></param>
        /// <returns>true or false</returns>
        public bool Pattern_Coin(Pixel[,] Image)
        {
            bool ok = true;
            Pixel black = new Pixel(0, 0, 0);
            Pixel White = new Pixel(255, 255, 255);
            Pixel grey = new Pixel(150, 150, 150);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i == 0) || (i == 6))
                    {
                        if (!(Image[i, j].equal(black)))
                        {
                            ok = false;

                        }
                        else
                        {
                            Image[i, j] = new Pixel(grey);
                        }
                    }
                    else if ((i == 1) || (i == 5))
                    {
                        if ((j >= 1) && (j <= 5))
                        {
                            if (!(Image[i, j].equal(White)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                        else
                        {
                            if ((!(Image[i, j].equal(black))))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                    }
                    else if ((i >= 2) && (i <= 4))
                    {
                        if ((j == 1) || (j == 5))
                        {
                            if (!(Image[i, j].equal(White)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                        else
                        {
                            if (!(Image[i, j].equal(black)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                    }

                }

            }
            for (int i = Image.GetLength(0) - 7; i < Image.GetLength(0); i++)
            {
                for (int j = Image.GetLength(0) - 7; j < Image.GetLength(0); j++)
                {
                    if ((i == Image.GetLength(0) - 7) || (i == Image.GetLength(0) - 1))
                    {
                        if (!(Image[i, j].equal(black)))
                        {
                            ok = false;
                        }
                        else
                        {
                            Image[i, j] = new Pixel(grey);
                        }
                    }
                    else if ((i == Image.GetLength(0) - 6) || (i == Image.GetLength(0) - 2))
                    {

                        if ((j >= Image.GetLength(0) - 6) && (j <= Image.GetLength(0) - 2))
                        {
                            if (!(Image[i, j].equal(White)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                        else
                        {
                            if (!(Image[i, j].equal(black)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                    }
                    else if ((i >= Image.GetLength(0) - 5) && (i <= Image.GetLength(0) - 3))
                    {
                        if ((j == Image.GetLength(0) - 6) || (j == Image.GetLength(0) - 2))
                        {
                            if (!(Image[i, j].equal(White)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                        else
                        {
                            if (!(Image[i, j].equal(black)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                    }
                }
            }
            for (int i = Image.GetLength(0) - 7; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if ((i == Image.GetLength(0) - 7) || (i == Image.GetLength(0) - 1))
                    {
                        if (!(Image[i, j].equal(black)))
                        {
                            ok = false;
                        }
                        else
                        {
                            Image[i, j] = new Pixel(grey);
                        }
                    }
                    else if ((i == Image.GetLength(0) - 6) || (i == Image.GetLength(0) - 2))
                    {
                        if ((j >= 1) && (j <= 5))
                        {
                            if (!(Image[i, j].equal(White)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                        else
                        {
                            if (!(Image[i, j].equal(black)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                    }
                    else if ((i >= Image.GetLength(0) - 5) && (i <= Image.GetLength(0) - 3))
                    {

                        if ((j == 1) || (j == 5))
                        {
                            if (!(Image[i, j].equal(White)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                        else
                        {
                            if (!(Image[i, j].equal(black)))
                            {
                                ok = false;
                            }
                            else
                            {
                                Image[i, j] = new Pixel(grey);
                            }
                        }
                    }
                }
            }
            return ok;
        }

        /// <summary>
        /// Analyse l'image pour savoir si les séparateurs sont présents
        /// </summary>
        /// <param Image ="Image"></param>
        /// <returns>true or false</returns>
        public bool Separator(Pixel[,] Image)
        {
            bool ok = true;
            Pixel black = new Pixel(0, 0, 0);
            Pixel White = new Pixel(255, 255, 255);
            Pixel grey = new Pixel(150, 150, 150);
            for (int i = 0; i < 8; i++)
            {
                if (!(Image[i, 7].equal(White)))
                {
                    ok = false;
                }
                else
                {
                    Image[i, 7] = new Pixel(grey);
                }
                if (!(Image[Image.GetLength(0) - (i + 1), Image.GetLength(0) - 8].equal(White)))
                {
                    ok = false;
                }
                else
                {
                    Image[Image.GetLength(0) - (i + 1), Image.GetLength(0) - 8] = new Pixel(grey);
                }

                if (!(Image[Image.GetLength(0) - (i + 1), 7].equal(White)))
                {
                    ok = false;
                }
                else
                {
                    Image[Image.GetLength(0) - (i + 1), 7] = new Pixel(grey);
                }
            }
            for (int j = 0; j < 7; j++)
            {
                if ((!(Image[7, j].equal(White))) && (j != 7))
                {
                    ok = false;
                }
                else
                {
                    Image[7, j] = new Pixel(grey);
                }

                if (!(Image[Image.GetLength(0) - 8, Image.GetLength(0) - (j + 1)].equal(White)))
                {
                    ok = false;
                }
                else
                {
                    Image[Image.GetLength(0) - 8, Image.GetLength(0) - (j + 1)] = new Pixel(grey);
                }
                if (!(Image[Image.GetLength(0) - 8, j].equal(White)))
                {
                    ok = false;
                }
                else
                {
                    Image[Image.GetLength(0) - 8, j] = new Pixel(grey);
                }
            }
            return ok;
        }

        /// <summary>
        /// Analyse l'image pour savoir si les timing pattern sont présents
        /// </summary>
        /// <param Image="Image"></param>
        /// <returns>true or false</returns>
        public bool timing_pattern(Pixel[,] Image)
        {
            Pixel black = new Pixel(0, 0, 0);
            Pixel White = new Pixel(255, 255, 255);
            Pixel grey = new Pixel(150, 150, 150);
            bool ok = true;
            for (int i = 8; i < Image.GetLength(0) - 8; i += 2)
            {
                if (!(Image[i, 6].equal(black)))
                {
                    ok = false;
                }
                else
                {
                    Image[i, 6] = new Pixel(grey);
                }
                if ((!(Image[i + 1, 6].equal(White))) && (i != Image.GetLength(0) - 9))
                {
                    ok = false;
                }
                else
                {
                    Image[i + 1, 6] = new Pixel(grey);
                }
                if (!(Image[Image.GetLength(0) - 7, i].equal(black)))
                {
                    ok = false;
                }
                else
                {
                    Image[Image.GetLength(0) - 7, i] = new Pixel(grey);
                }
                if ((!(Image[Image.GetLength(0) - 7, i + 1].equal(White))) && (i != Image.GetLength(0) - 9))
                {
                    ok = false;
                }
                else
                {
                    Image[Image.GetLength(0) - 7, i + 1] = new Pixel(grey);
                }

            }
            return ok;
        }

        /// <summary>
        /// Verifie l'alignement pour les images supérieur à la version 1
        /// </summary>
        /// <param Image="Image"></param>
        /// <returns>true or false</returns>
        public bool alignement_check(Pixel[,] Image)
        {
            bool ok = true;
            Pixel black = new Pixel(0, 0, 0);
            Pixel White = new Pixel(255, 255, 255);
            Pixel grey = new Pixel(150, 150, 150);
            for (int i = 4; i <= 8; i++)
            {
                for (int j = 16; j <= 20; j++)
                {
                    if (((i == 6) && (j == 18)) || ((i == 4) || (i == 8)) || ((j == 16) || (j == 20)))
                    {
                        if ((!(Image[i, j].equal(black))) && (!(Image[i, j].equal(grey))))
                        {
                            ok = false;
                        }
                        else
                        {
                            Image[i, j] = new Pixel(grey);
                        }
                    }
                    else
                    {
                        if ((!(Image[i, j].equal(White))) && (!(Image[i, j].equal(grey))))
                        {
                            ok = false;
                        }
                        else
                        {
                            Image[i, j] = new Pixel(grey);
                        }
                    }


                }
            }
            if (ok)
            {
                Console.WriteLine("HELOOOO");
            }
            return ok;
        }

        /// <summary>
        /// Lit les bits de données
        /// </summary>
        /// <param Image="Image"></param>
        /// <returns>List de bits</returns>
        public List<bool> lecture_Data(Pixel[,] Image)
        {
            List<bool> Donne = new List<bool>();
            int j = Image.GetLength(0) - 1;
            bool montee = true;
            Pixel black = new Pixel(0, 0, 0);
            Pixel grey = new Pixel(150, 150, 150);
            Pixel White = new Pixel(255, 255, 255);
            int compteur = 0;
            int Max = 0;
            if (j == 20)
            {
                Max = 208;
            }
            else if (j == 24)
            {
                Max = 352;
            }
            while (j >= 0)
            {
                if (montee)
                {
                    for (int i = 0; i < Image.GetLength(0); i++)
                    {
                        if (compteur >= Max)
                        {
                            break;
                        }
                        if (((((i + j) % 2 == 0) && (Image[i, j].equal(black))) || ((((i + j) % 2 != 0)) && Image[i, j].equal(White))) && (!(Image[i, j].equal(grey))))
                        {
                            Donne.Add(false);
                            compteur++;
                            Image[i, j] = new Pixel(grey);
                        }
                        else if (((((i + j) % 2 == 0) && (Image[i, j].equal(White))) || ((((i + j) % 2 != 0)) && Image[i, j].equal(black))) && (!(Image[i, j].equal(grey))))
                        {
                            Donne.Add(true);
                            compteur++;
                            Image[i, j] = new Pixel(grey);
                        }
                        if (compteur >= Max)
                        {
                            break;
                        }
                        if (((((i + (j - 1)) % 2 == 0) && (Image[i, j - 1].equal(black))) || ((((i + (j - 1)) % 2 != 0)) && Image[i, j - 1].equal(White))) && (!(Image[i, j - 1].equal(grey))))
                        {
                            Donne.Add(false);
                            compteur++;
                            Image[i, j - 1] = new Pixel(grey);
                        }
                        else if (((((i + (j - 1)) % 2 == 0) && (Image[i, j - 1].equal(White))) || ((((i + (j - 1)) % 2 != 0)) && Image[i, j - 1].equal(black))) && (!(Image[i, j - 1].equal(grey))))
                        {
                            Donne.Add(true);
                            compteur++;
                            Image[i, j - 1] = new Pixel(grey);
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
                    for (int i = Image.GetLength(0) - 1; i >= 0; i--)
                    {
                        if (compteur >= Max)
                        {
                            break;
                        }
                        if (((((i + j) % 2 == 0) && (Image[i, j].equal(black))) || ((((i + j) % 2 != 0)) && Image[i, j].equal(White))) && (!(Image[i, j].equal(grey))))
                        {
                            Donne.Add(false);
                            compteur++;
                            Image[i, j] = new Pixel(grey);
                        }
                        else if (((((i + j) % 2 == 0) && (Image[i, j].equal(White))) || ((((i + j) % 2 != 0)) && Image[i, j].equal(black))) && (!(Image[i, j].equal(grey))))
                        {
                            Donne.Add(true);
                            compteur++;
                            Image[i, j] = new Pixel(grey);
                        }
                        if (compteur >= Max)
                        {
                            break;
                        }
                        if (((((i + (j - 1)) % 2 == 0) && (Image[i, j - 1].equal(black))) || ((((i + (j - 1)) % 2 != 0)) && Image[i, j - 1].equal(White))) && (!(Image[i, j - 1].equal(grey))))
                        {
                            Donne.Add(false);
                            compteur++;
                            Image[i, j - 1] = new Pixel(grey);
                        }
                        else if ((((i + (j - 1)) % 2 == 0) && (Image[i, j - 1].equal(White))) || ((((i + (j - 1)) % 2 != 0)) && Image[i, j - 1].equal(black)) && (!(Image[i, j - 1].equal(grey))))
                        {
                            Donne.Add(true);
                            compteur++; Image[i, j - 1] = new Pixel(grey);
                        }

                    }
                    j -= 2;
                    montee = true;
                }

            }


            return Donne;
        }

        /// <summary>
        /// Retourne le code
        /// </summary>
        /// <returns>code lu</returns>
        public string To_String()
        {
            return code;
        }
        /// <summary>
        /// Lit le masque
        /// </summary>
        /// <param Image="Image"></param>
        public void lecture_mask(Pixel[,] Image)
        {
            Pixel grey = new Pixel(150, 150, 150);
            for (int i = 0; i < Image.GetLength(0); i++)
            {
                if ((((i >= 0) && (i < 9)) || ((i >= Image.GetLength(0) - 8) && (i < Image.GetLength(0)))) && (i != 6))
                {
                    Image[Image.GetLength(0) - 9, i] = grey;
                }

            }
            for (int i = 0; i < Image.GetLength(0); i++)
            {

                if ((((i >= 0) && (i < 7)) || ((i >= Image.GetLength(0) - 9) && (i < Image.GetLength(0)))) && (i != Image.GetLength(0) - 7))
                {

                    Image[i, 8] = grey;
                }

            }
        }
    }
}
