using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Scrabble
{
    class Joueur
    {

        //Déclaration des attributs
        string nom;
        int score = 0;
        List<string> mots = new List<string>();
        List<Jeton> jetons = new List<Jeton>();
        SortedList<char, int> valeur = new SortedList<char, int>();


        //Propriétés
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public List<string> Mots
        {
            get { return mots; }
            set { mots = value; }
        }
        public List<Jeton> Jetons
        {
            get { return jetons; }
            set { jetons = value; }
        }
        public string Nom
        {
            get { return nom; }
        }
        public SortedList<char, int> Valeur
        {
            get { return valeur; }
        }

        //Constructeur
        public Joueur(string nom) //Constructeur de base
        {
            this.nom = nom;

            //Création d'une référence dans la classe joueur qui à chaque lettre connait une valeur
            StreamReader lecteur = new StreamReader("Jetons.txt"); //On crée une variable qui lit le fichier
            string ligne; //Chaque ligne du document sera tour à tour dans cette variable string
            while (!lecteur.EndOfStream) //Boucle qui se termine à la fin du document
            {
                try
                {
                    ligne = lecteur.ReadLine(); //La ligne prend la valeur de la dernière ligne non lue du document
                    if (ligne != null && ligne != "") //On vérifie que la ligne a des informations
                    {
                        string[] infos = ligne.Split(';'); //On sépare les infos séparées par un ; dans un tableau de string tel que : (lettre, points, nombre)
                        valeur.Add(Convert.ToChar(infos[0]), Convert.ToInt32(infos[1]));
                    }
                }
                catch (Exception e) //Vérification des erreurs
                {
                    Console.WriteLine(e.Message);
                }
            }
            lecteur.Close(); //On ferme le lecteur une fois l'opération finie
        }

        public Joueur(string nom, int score, List<string> mots) //Constructeur particulier (joueur rejoint en cours de partie)
        {
            this.nom = nom;
            this.score = score;
            this.mots = mots;

            //Création d'une référence dans la classe joueur qui à chaque lettre connait une valeur
            StreamReader lecteur = new StreamReader("Jetons.txt"); //On crée une variable qui lit le fichier
            string ligne; //Chaque ligne du document sera tour à tour dans cette variable string
            while (!lecteur.EndOfStream) //Boucle qui se termine à la fin du document
            {
                try
                {
                    ligne = lecteur.ReadLine(); //La ligne prend la valeur de la dernière ligne non lue du document
                    if (ligne != null && ligne != "") //On vérifie que la ligne a des informations
                    {
                        string[] infos = ligne.Split(';'); //On sépare les infos séparées par un ; dans un tableau de string tel que : (lettre, points, nombre)
                        valeur.Add(Convert.ToChar(infos[0]), Convert.ToInt32(infos[1]));
                    }
                }
                catch (Exception e) //Vérification des erreurs
                {
                    Console.WriteLine(e.Message);
                }
            }
            lecteur.Close(); //On ferme le lecteur une fois l'opération finie
        }

        //Opérations
        public void Add_Mot(string mot)
        {
            mots.Add(mot);
        }

        public string toString() //Affiche le nom du joueur, son score, son nombre de mots trouvés et sa main
        {
            string ret = "\n\nLe joueur " + nom + " a actuellement " + score + " point(s) et a trouvé " + mots.Count + " mot(s).\n\nIl a dans sa main : ";
            for (int i = 0; i < jetons.Count; i++) ret += "\n" + jetons[i].toString() + ", ";
            return ret;
        }

        public void Add_Score(int val)
        {
            score += val;
        }
        public void Add_Main_Courante(Jeton monjeton)
        {
            jetons.Add(monjeton);
        }
        public void Remove_Main_Courante(char jeton)
        {
            bool stop = false;
            for(int i = 0; i < jetons.Count; i++)
            {
                if (jetons[i].Char == jeton && !stop)
                {
                    jetons.RemoveAt(i);
                    stop = true;
                }
            }
        }
        public bool Existe(char jeton)
        {
            bool ret = false;
            for (int i = 0; i < jetons.Count; i++) if (jetons[i].Char == jeton) ret = true;
            return ret;
        }
    }
}
