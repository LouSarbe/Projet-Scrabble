using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Scrabble
{
    class Dictionnaire
    {
        //Déclaration
        List<string>[] dico;

        //Propriétés
        public List<string>[] Dico
        {
            get { return dico; }
            //set { dico = value; }
        }

        //Constructeur
        public Dictionnaire(string fichier)
        {
            dico = new List<string>[16];

            for (int i = 0; i < 16; i++) dico[i] = new List<string>(); //On initie toutes les listes pour qu'elles ne soient pas null

            StreamReader lecteur = new StreamReader(fichier); //On initialise un lecteur de fichier
            string ligne; //On utilisera cette variable pour y stocker chaque ligne du fichier l'une après l'autre
            while (!lecteur.EndOfStream) //Tant que le fichier n'est pas complètement lu
            {
                ligne = lecteur.ReadLine(); //ligne prend la valeur de la dernière ligne non lue
                if (ligne != null && ligne != "" && ligne.Length > 2)
                {
                    string[] mots = ligne.Split(' '); //On sépare les mots dans un tableau (un mot par case du tableau)
                    for (int i = 0; i < mots.Length; i++)
                    {
                        dico[mots[i].Length].Add(mots[i]); //On ajoute chaque mot un par un dans notre liste dans notre tableau
                    }
                }
            }
            lecteur.Close();
        }


        //Opérations
        public string toString()
        {
            string ret = "\n\nCeci est un dictionnaire de français.\nOn y trouve : ";
            int total = 0;
            for(int i = 2; i < 15; i++)
            {
                ret += dico[i].Count + " mots de " + i + " lettres.\n";
                total += dico[i].Count;
            }
            ret += dico[15].Count + " mots de 15 lettres.";
            ret += "\nCe qui nous fait un total de " + total + "mots.";
            return ret;
        }
        public bool RechDichoRecursif(string mot)
        {
            int longueur = mot.Length;
            if (dico[longueur].Contains(mot)) return true;
            else return false;
        }
    }
}
