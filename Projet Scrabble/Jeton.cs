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

        //Propriété
        public char Char
        {
            get { return jeton; }
        }
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        //Constructeur
        public Jeton(char lettre, int points)
        {
            jeton = lettre;
            this.points = points;
        }

        //Opérations
        public string toString()
        {
            return "Jeton : " + jeton + " \t Valeur : " + points + " point(s)";
        }
    }
}
