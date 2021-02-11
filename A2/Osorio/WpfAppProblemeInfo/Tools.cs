namespace WpfAppProblemeInfo
{

    /// <summary>
    /// La Classe tools me permet d'avoir accès à une banque d'opérations qui seront utilisé dans plusieus classe car très général,
    /// notamment entre la classe QR code et MyImage comme la puisssance ou encore des conversions bool en byte et en int. Elle est aussi utile car accessible 
    /// dans l'éventualité de créer de nouvelles classes pour réaliser de nouvelles fonctions
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Convertit un tableau de booleen (bits) en enter
        /// </summary>
        /// <param tableau="convert"></param>
        /// <param exposant max="expo"></param>
        /// <returns>valeur int du tableau</returns>
        public static int Bool_to_Int(bool[] convert, int expo)
        {

            int a = 0;
            for (int i = 0; i < convert.Length; i++)
            {
                if (convert[i])
                {
                    a += puissance(expo, 2);
                }
                expo--;
            }
            return a;
        }

        /// <summary>
        /// Effectue l'opération puissance
        /// </summary>
        /// <param exposant="exposant"></param>
        /// <param base="bas"></param>
        /// <returns>bas puissance exposant</returns>
        public static int puissance(int exposant, int bas)
        {
            int a = 1;
            if (exposant != 0)
            {
                while (exposant != 0)
                {
                    a *= bas;
                    exposant--;
                }
            }
            return a;
        }

        /// <summary>
        /// Convertit un entier en bit
        /// </summary>
        /// <param nombre entier="val"></param>
        /// <param nbbit sur lequel il est coder="nbBit"></param>
        /// <returns></returns>
        public static bool[] int_to_bit(int val, int nbBit)
        {
            bool[] nombre = new bool[nbBit];
            int[] inter = new int[nbBit];
            int i = 0;


            while ((val != 0) && (i < nbBit))
            {
                inter[i] = val % 2;
                val /= 2;
                i += 1;
            }
            int tmp = 0;
            for (int k = nbBit - 1; k >= 0; k--)
            {
                if (inter[k] == 0)
                {
                    nombre[tmp] = false;
                    tmp++;
                }
                else
                {
                    nombre[tmp] = true;
                    tmp++;
                }
            }

            return nombre;
        }

        /// <summary>
        /// Obtient l'index d'un bit de byte
        /// </summary>
        /// <param byte="a"></param>
        /// <param index rechercher="n"></param>
        /// <returns>le bit correspondant a n</returns>
        public static bool GetBit_byte(byte a, int n)
        {
            return ((a & (byte)(1 << n)) != 0);
        }

        /// <summary>
        /// Facilite l'opération puissance pour des nombres coder sur plusieurs octets
        /// </summary>
        /// <param numero de loctet="exposant"></param>
        /// <returns>la puissance</returns>
        public static int BinaryPow(int exposant)
        {
            int resultat = 256;
            for (int i = 1; i < exposant; i++)
            {
                resultat *= 256;
            }

            return resultat;
        }



    }
}
