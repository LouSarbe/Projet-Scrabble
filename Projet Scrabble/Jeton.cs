using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Scrabble
{
    class Jeton
    {
        char jeton;
        int points;


        //Constructeur
        public Jeton(char lettre, int points)
        {
            jeton = lettre;
            this.points = points;
        }

        //Opérations
        public string toString()
        {
            return "Ce jeton est un " + jeton + " et il vaut " + points + " point(s)";
        }
    }
}
