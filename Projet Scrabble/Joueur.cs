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
        List<char> jetons = new List<char>();


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

        public string toString()
        {
            return "Le joueur " + nom + " a actuellement " + score + " point(s) et a trouvé " + mots.Count + " mot(s).";
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

        /*string nom;
int score;
string[] mottrouves;

public string nom
    {
        get
        {
            return _nom;
        }
        set
        {
            _nom = value;
        }
    }
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }
    public string [] mottrouves
    {
        get
        {
            return _mottrouves
        }
        set
        {
            _mottrouves = value;
        }
    }

    public Joueur (string nom, int score, string [] mottrouves)
{
if(nom=!null)
        {
_nom = nom;
        }
if(score==0)
        {
_score = score;
        }
if(mottrouves=null)
        {
_mottrouves = mottrouves;
        }

}
public Joueur(string nom)
    {
        if (nom = !null)
        {
            _nom = nom;
        }
_score = null;
_mottrouves = null;
    }
    public void Add_Mot(string mot)
    {
        mottrouves.Length = mottrouves.Length++;
        int a = mottrouves.Length;
        mottrouves[a] = mot;
    }
    public override string ToString()
    {
        return nom + " a trouve la liste de mots suivants et a donc un score de " + score + " points";
    }
*/
    }
}
