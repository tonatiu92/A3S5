using System;

namespace WpfAppProblemeInfo
{
    class Complexe
    {
        double reel;
        double imaginaire;


        public Complexe(double R, double I)
        {
            this.reel = R;
            this.imaginaire = I;
        }

        public double Reel
        {
            get
            {
                return reel;
            }
            set
            {
                reel = value;
            }
        }

        public double Imaginaire
        {
            get
            {
                return imaginaire;
            }
            set
            {
                imaginaire = value;
            }
        }

        public Complexe Square()
        {
            double a = (reel * reel) - (imaginaire * imaginaire);
            double b = 2 * reel * imaginaire;
            Complexe carre = new Complexe(a, b);
            return carre;
        }

        public double Norme()
        {
            return Math.Sqrt(reel * reel + imaginaire * imaginaire);
        }

        public Complexe Add(Complexe somme)
        {
            double a = somme.Reel + reel;
            double b = somme.Imaginaire + imaginaire;
            Complexe add = new Complexe(a, b);
            return add;
        }



    }
}
