using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osorio_Thomas_TDJ_Probleme
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MIXMO";
            Jeu encours = new Jeu();
            encours.partie();
            Console.ReadLine();
        }
    }
}
////////AUTO_CRITIQUE/////////
///point négatif: j'aurai pu diminuer certaines boucles je peux être répétitif à des moments
///               je pense que j'aurai pu rajouter les jokers avec un peu plus de temps
///               affichage de ma grille, je n'ai pas réussi à afficher la grille comme je le souhaitais
/// Point positif:l'encapsulation POO, j'ai esssayé de respecter au mieux les classes et de les relier de la bonne manière
///               La plupart des tâches demandées sont faites
///               commentaire +quelques test unitaires
///               j'ai essayé de rendre facile la lecture des donnée.
///               
///