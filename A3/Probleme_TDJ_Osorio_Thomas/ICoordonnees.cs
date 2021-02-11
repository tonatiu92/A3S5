using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_TDJ_Osorio_Thomas
{
    interface ICoordonnees
    {
        string Nom { get; set; }
        string Prenom { get; set; }
        string Adresse { get; set; }
        string Numero { get; set; }
    }
}
