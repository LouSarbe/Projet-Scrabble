using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Scrabble
{
    class Joueur
    {

        //Déclaration des attributs
        string nom;
        int score = 0;
        List<string> mots = null;
        List<Jeton> jetons = new List<Jeton>();


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

        //Constructeur
        public Joueur(string nom) //Constructeur de base
        {
            this.nom = nom;
        }

        public Joueur(string nom, int score, List<string> mots) //Constructeur particulier (joueur rejoint en cours de partie)
        {
            this.nom = nom;
            this.score = score;
            this.mots = mots;
        }

        //Opérations
        public void Add_Mot(string mot)
        {
            mots.Add(mot);
        }

        public string toString() //Affiche le nom du joueur, son score, son nombre de mots trouvés et sa main
        {
            string ret = "\n\nLe joueur " + nom + " a actuellement " + score + " point(s) et a trouvé " + mots.Count + " mot(s).\nIl a dans sa main : ";
            for (int i = 0; i < jetons.Count - 1; i++)
            {
                ret += jetons[i] + ", ";
            }
            ret += " et " + jetons[jetons.Count - 1];
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
        public void Remove_Main_Courante(Jeton monjeton)
        {
            jetons.Remove(monjeton);
        }
    }
}
