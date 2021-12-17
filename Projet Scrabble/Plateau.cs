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


        List<char> jetons = new List<char>(); //Initie une liste qui nous donnera les jetons utilisés pendant le placement d'un nouveau mot

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
            for(int i =0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++) plateau[i, j] = '_';

                //Configuration des cases spéciales
                plateau[0, 0] = '4';
                plateau[0, 7] = '4';
                plateau[0, 14] = '4';
                plateau[7, 0] = '4';
                plateau[14, 0] = '4';
                plateau[14, 14] = '4';
                plateau[14, 7] = '4';
                plateau[7, 14] = '4';
                plateau[1, 1] = '3';
                plateau[2, 2] = '3';
                plateau[3, 3] = '3';
                plateau[4, 4] = '3';
                plateau[13, 1] = '3';
                plateau[12, 2] = '3';
                plateau[11, 3] = '3';
                plateau[10, 4] = '3';
                plateau[10, 10] = '3';
                plateau[11, 11] = '3';
                plateau[12, 12] = '3';
                plateau[13, 13] = '3';
                plateau[4, 10] = '3';
                plateau[3, 11] = '3';
                plateau[2, 12] = '3';
                plateau[1, 13] = '3';
                plateau[5, 1] = '2';
                plateau[9, 1] = '2';
                plateau[5, 13] = '2';
                plateau[9, 13] = '2';
                plateau[1, 5] = '2';
                plateau[5, 5] = '2';
                plateau[9, 5] = '2';
                plateau[13, 5] = '2';
                plateau[1, 9] = '2';
                plateau[5, 9] = '2';
                plateau[9, 9] = '2';
                plateau[13, 9] = '2';
                plateau[3, 0] = '1';
                plateau[11, 0] = '1';
                plateau[0, 3] = '1';
                plateau[0, 11] = '1';
                plateau[6, 2] = '1';
                plateau[2, 6] = '1';
                plateau[8, 2] = '1';
                plateau[2, 8] = '1';
                plateau[7, 3] = '1';
                plateau[3, 7] = '1';
                plateau[6, 6] = '1';
                plateau[8, 8] = '1';
                plateau[6, 8] = '1';
                plateau[8, 6] = '1';
                plateau[14, 3] = '1';
                plateau[3, 14] = '1';
                plateau[14, 11] = '1';
                plateau[11, 14] = '1';
                plateau[7, 11] = '1';
                plateau[11, 7] = '1';
                plateau[6, 12] = '1';
                plateau[12, 6] = '1';
                plateau[8, 12] = '1';
                plateau[12, 8] = '1';
            }
        }
        /*public Plateau(string fichier) //Constructeur particulier
        {
            plateau = new char[15, 15];
            for(int i = 0; i < plateau.GetLength(0); i++)
            {
                for(int j = 0; j < plateau.GetLength(1); j++)
                {
                    plateau[i, j] = fichier[2 * i + j * 30];
                }
            }
        }*/

        //Opérations

        /// <summary>
        /// Affiche le plateau de jeu (les cases spéciales sont affichées comme vides)
        /// </summary>
        /// <returns>Un string comportant l'affichage entier du plateau</returns>
        public string toString()
        {
            string ret = "\n\n";
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.GetLength(1); j++)
                {
                    if (plateau[i, j] == '1' || plateau[i, j] == '2' || plateau[i, j] == '3' || plateau[i, j] == '4') ret += "_ "; //Les 1, 2, 3 et 4 sont des cases 
                    else ret += plateau[i, j] + " ";
                }
                ret += "\n";
            }
            return ret;
        }

        /// <summary>
        /// Teste qu'un mot donné par un joueur peut-être placé dans le plateau, dans la direction et la position donnée par le joueur. Additionnellement, permet de savoir quels jetons le joueur va utiliser lors de son tour
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <param name="direction"></param>
        /// <returns>true si tout est bon, false si il y a un problème (avec un message d'erreur selon le problème)</returns>
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
                        if (plateau[ligne, colonne - i] != mot[i] && plateau[ligne, colonne - i] != '_' && plateau[ligne, colonne - i] != '1' && plateau[ligne, colonne - i] != '2' && plateau[ligne, colonne - i] != '3' && plateau[ligne, colonne - i] != '4')
                        {
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés
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
                        if (plateau[ligne, colonne + i] != mot[i] && plateau[ligne, colonne  + i] != '_' && plateau[ligne, colonne - i] != '1' && plateau[ligne, colonne - i] != '2' && plateau[ligne, colonne - i] != '3' && plateau[ligne, colonne - i] != '4')
                        {
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés
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
                        if (plateau[ligne - i, colonne] != mot[i] && plateau[ligne - i, colonne] != '_' && plateau[ligne, colonne - i] != '1' && plateau[ligne, colonne - i] != '2' && plateau[ligne, colonne - i] != '3' && plateau[ligne, colonne - i] != '4')
                        {
                            Console.WriteLine("Erreur, il n'est pas possible de placer ce mot ici.");
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés
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
                        if (plateau[ligne + i, colonne] != mot[i] && plateau[ligne + i, colonne] != '_' && plateau[ligne, colonne - i] != '1' && plateau[ligne, colonne - i] != '2' && plateau[ligne, colonne - i] != '3' && plateau[ligne, colonne - i] != '4')
            {
                            ret = false;
                        }
                        else //Tout est bon, on maintenant regarder les jetons utilisés
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
