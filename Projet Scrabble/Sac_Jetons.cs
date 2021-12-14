using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Scrabble
{
    class Sac_Jetons
    {
        //Déclaration
        List<Jeton> sac;
        int nombre;

        //Propriétés
        public List<Jeton> Sac
        {
            get { return sac; }
        }
        public int Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        //Constructeur
        public Sac_Jetons(string fichier)
        {
            sac = new List<Jeton>();

            StreamReader lecteur = new StreamReader(fichier); //On crée une variable qui lit le fichier
            string ligne; //Chaque ligne du document sera tour à tour dans cette variable string
            while (!lecteur.EndOfStream) //Boucle qui se termine à la fin du document
            {
                try
                {
                    ligne = lecteur.ReadLine(); //La ligne prend la valeur de la dernière ligne non lue du document
                    if (ligne != null && ligne != "") //On vérifie que la ligne a des informations
                    {
                        string[] infos = ligne.Split(';'); //On sépare les infos séparées par un ; dans un tableau de string tel que : (lettre, points, nombre)
                        for (int i = 0; i < Convert.ToInt32(infos[2]); i++) //On répète l'opération autant de fois qu'il y a  de jeton
                        {
                            Jeton jeton = new Jeton(Convert.ToChar(infos[0]), Convert.ToInt32(infos[1])); //On crée un jeton (lettre, points)
                            sac.Add(jeton); //On ajoute le jeton au sac
                            nombre++; //J'ajoute à chaque jeton une unité au compteur de jetons dans le sac
                        }
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
        public Jeton Retire_Jeton(Random r)
        {
            Jeton ret;
            int n = r.Next(sac.Count);
            ret = sac[n];
            sac.RemoveAt(n);
            nombre--;
            return ret;
        }
        public string toString()
        {
            string ret = "";
            for(int i = 0; i < nombre; i++)
            {
                ret += "\n" + sac[i].toString();
            }
            return ret;
        }
    }
}
