using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Scrabble
{
    class Plateau
    {
        //Déclaration
        char[,] plateau;

        //Constructeurs
        public Plateau() //Constructeur de base
        {
            plateau = new char[15, 15];
        }
        public Plateau(string fichier) //Constructeur particulier
        {
            plateau = new char[15, 15];
            for(int i = 0; i < plateau.GetLength(0); i++)
            {
                for(int j = 0; j < plateau.GetLength(1); j++)
                {
                    plateau[i, j] = fichier[2 * i + j * 30];
                }
            }
        }

        //Opérations
        public string toString()
        {
            string ret = "\n\n\n";
            for (int i = 0; i < plateau.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < plateau.GetLength(1) - 1; j++)
                {
                    ret += plateau[i, j] + " ";
                }
                ret += plateau[i, plateau.GetLength(1) - 1] + "\n";
            }
            return ret;
        }
        public bool Test_Plateau(string mot, int ligne, int colonne, char direction)
        {
            if(direction == 'h')
            {
                if (mot.Length > ligne + 1)
                {
                    Console.WriteLine("Erreur, il n'y a pas assez d'espace pour placer ce mot ici");
                    return false;
                }
                else
                {
                    for(int i = 0; i < mot.Length; i++)
                    {
                        if (plateau[ligne, colonne - i] != mot[i] && plateau[ligne, colonne - i] != '_')
                        {
                            Console.WriteLine("Erreur, il n'est pas possible de placer ce mot ici.");
                            return false;
                        }
                        else return true;
                    }
                }
            }
            else if (direction == 'h')
            {

            }
            else if (direction == 'h')
            {

            }
            else if (direction == 'h')
            {

            }
            else
            {
                Console.WriteLine("Erreur, la direction doit être h, b, d, ou g. Essayez encore.");
                return false;
            }
        }
    }
}
