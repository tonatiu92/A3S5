using System.Linq;

namespace WpfAppProblemeInfo
{
    class Histogramme
    {
        int[] HistoMoy;
        int[] HistoR;
        int[] HistoV;
        int[] HistoB;

        /// <summary>
        /// Cree les différents histogrammes
        /// </summary>
        /// <param name="Image"></param>
        public Histogramme(Pixel[,] Image)
        {
            HistoMoy = new int[256];
            HistoR = new int[256];
            HistoV = new int[256];
            HistoB = new int[256];
            for (int k = 0; k < 256; k++)
            {
                HistoMoy[k] = 0;
                HistoR[k] = 0;
                HistoV[k] = 0;
                HistoB[k] = 0;
            }
            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(1); j++)
                {
                    HistoMoy[Image[i, j].Moyenne()] += 1;
                    HistoR[Image[i, j].Rouge] += 1;
                    HistoV[Image[i, j].Vert] += 1;
                    HistoB[Image[i, j].Bleu] += 1;

                }
            }

        }
        public int[] HM
        {
            get
            {
                return HistoMoy;
            }
            set
            {
                HistoMoy = value;
            }
        }
        public int[] HR
        {
            get
            {
                return HistoR;
            }
            set
            {
                HistoR = value;
            }
        }
        public int[] HV
        {
            get
            {
                return HistoV;
            }
            set
            {
                HistoV = value;
            }
        }
        public int[] HB
        {
            get
            {
                return HistoB;
            }
            set
            {
                HistoB = value;
            }
        }
        /// <summary>
        /// Renvoie la valeur max entre les differents histo
        /// </summary>
        /// <returns>entier representant la valeur max</returns>
        public int Max_value()
        {
            int max = HistoMoy.Max();
            if (max < HistoR.Max())
            {
                max = HistoR.Max();
            }
            if (max < HistoV.Max())
            {
                max = HistoV.Max();
            }
            if (max < HistoB.Max())
            {
                max = HistoB.Max();
            }
            return max;

        }
    }
}
