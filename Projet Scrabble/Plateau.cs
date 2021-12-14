using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Scrabble
{
    class Plateau
    {
        //Déclaration
        char[,] plateau;


        List<char> jetons; //Initie une liste qui nous donnera les jetons utilisés pendant le placement d'un nouveau mot

        //Propriétés
        public char[,] Board
        {
            get { return plateau; }
            set { plateau = value; }
        }
        public List<char> Jetons
        {
            get { return jetons; }
            set { jetons = value; }
        }

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
            string ret = "\n\n";
            for (int i = 0; i < plateau.GetLength(0); i++)
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
            bool ret = true;
            if(direction == 'h') //Si mot va vers le haut
            {
                if (mot.Length > ligne + 1) //Vérification espace nécessaire
                {
                    Console.WriteLine("Erreur, il n'y a pas assez d'espace pour placer ce mot ici");
                    return false;
                }
                else
                {
                    for(int i = 0; i < mot.Length; i++) //Vérification que les cases sont vides, ou que les croisements ont les mêmes lettres
                    {
                        if (plateau[ligne, colonne - i] != mot[i] && plateau[ligne, colonne - i] != '_')
                        {
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés et les cases spéciales (mot double, lettre triple, ...)
                        {
                            if (plateau[ligne, colonne - i] != mot[i]) jetons.Add(mot[i]);
                        }
                    }
                    if (ret == false) Console.WriteLine("Il n'est pas possible de placer ce mot ici");
                    return ret;
                }
            }
            else if (direction == 'b')//Si mot va vers le bas
            {
                if (mot.Length >= 15 - ligne)//Vérification espace nécessaire
                {
                    Console.WriteLine("Erreur, il n'y a pas assez d'espace pour placer ce mot ici");
                            return false;
                        }
                else
                {
                    for (int i = 0; i < mot.Length; i++)//Vérification que les cases sont vides, ou que les croisements ont les mêmes lettres
                    {
                        if (plateau[ligne, colonne + i] != mot[i] && plateau[ligne, colonne  + i] != '_')
                        {
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés et les cases spéciales (mot double, lettre triple, ...)
                        {
                            if (plateau[ligne, colonne + i] != mot[i]) jetons.Add(mot[i]);
                        }
                    }
                    if (ret == false) Console.WriteLine("Il n'est pas possible de placer ce mot ici");
                    return ret;
                    }
                }
            else if (direction == 'g')//Si mot va vers la gauche
            {
                if (mot.Length > colonne + 1)//Vérification espace nécessaire
                {
                    Console.WriteLine("Erreur, il n'y a pas assez d'espace pour placer ce mot ici");
                    return false;
            }
                else
            {
                    for (int i = 0; i < mot.Length; i++)//Vérification que les cases sont vides, ou que les croisements ont les mêmes lettres
                    {
                        if (plateau[ligne - i, colonne] != mot[i] && plateau[ligne - i, colonne] != '_')
                        {
                            Console.WriteLine("Erreur, il n'est pas possible de placer ce mot ici.");
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés et les cases spéciales (mot double, lettre triple, ...)
                        {
                            if (plateau[ligne - i, colonne] != mot[i]) jetons.Add(mot[i]);
                        }
                    }
                    if (ret == false) Console.WriteLine("Il n'est pas possible de placer ce mot ici");
                    return ret;
                }
            }
            else if (direction == 'd')//Si mot va vers la droite
            {
                if (mot.Length >= 15 - colonne)//Vérification espace nécessaire
            {
                    Console.WriteLine("Erreur, il n'y a pas assez d'espace pour placer ce mot ici");
                    return false;
            }
                else
                {
                    for (int i = 0; i < mot.Length; i++)//Vérification que les cases sont vides, ou que les croisements ont les mêmes lettres
                    {
                        if (plateau[ligne + i, colonne] != mot[i] && plateau[ligne + i, colonne] != '_')
            {
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés et les cases spéciales (mot double, lettre triple, ...)
                        {
                            if (plateau[ligne + i, colonne] != mot[i]) jetons.Add(mot[i]);
                        }
                    }
                    if (ret == false) Console.WriteLine("Il n'est pas possible de placer ce mot ici");
                    return ret;
                }
            }
            else
            {
                Console.WriteLine("Erreur, la direction doit être h, b, d, ou g");
                return false;
            }
        }
    }
}
