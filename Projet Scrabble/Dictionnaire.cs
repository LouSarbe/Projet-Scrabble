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
            for (int i = 0; i < 16; i++) //Pour toutes les longueurs de mots différentes
            {
                dico[i] = new List<string>(); //La ligne du tableau prend la valeur d'une liste de tous les mots qui ont ce nombre de mots
            }
            Lire(fichier);
        }


        //Opérations
        public void Lire(string fichier)
        {
            StreamReader lecteur = new StreamReader(fichier); //On initialise un lecteur de fichier
            string ligne; //On utilisera cette variable pour y stocker chaque ligne du fichier l'une après l'autre
            while (!lecteur.EndOfStream) //Tant que le fichier n'est pas complètement lu
            {
                ligne = lecteur.ReadLine(); //ligne prend la valeur de la dernière ligne non lue
                if (ligne != null && ligne != "" && ligne.Length > 1)
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
        public string toString()
        {

        }
        public bool RechDichoRecursif(string mot)
        {

        }
    }
    /*
        public Dictionnary (string filename)
        {
            dictionnary = new List<string>[16];
            for(int i = 0; i<16; i++)
            {
                dictionnary[i] = new List<string>();
            }
            ReadFile(filename);
        }
        public void ReadFile(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string l;
            while (!reader.EndOfStream)
            {
                l = reader.ReadLine();
                if (l != null && l != "" && l.Length > 1)
                {
                    string[] tabString = l.Split(' ');
                    for (int i = 0; i< tabString.Length; i++)
                    {
                        dictionnary[tabString[i].Length].Add(tabString[i]);
                    }
                }
            }
            reader.Close();
        }
        public override string ToString()
        {
            string s = "Le dictionnaire est composé de :";
            for (int i = 2; i < 16; i++)
            {
                s += "\n" + dictionnary[i].Count + " mots de " + i + " lettres."; //les nombre de 2 chiffres sont comptés. ERREUR
            }
            return s;
        }
        public bool Research(string mot)
        {
            return false;
        }
    }*/
}
