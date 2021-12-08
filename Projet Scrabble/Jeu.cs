using System;

namespace Projet_Scrabble
{
    class Jeu
    {
        static void Main(string[] args)
        {
            //Initialisation temporaire du plateau
            string fichier = "V;A;S;_;L;O;B;_;_;W;O;W;_;_;_;_;V;E;_;_;_;A;D;J;U;R;E;R;_;_;L; E;X;E;M;E;S;_;E;_;E;M;A;I;L;_; _;T;_;_;U;_;_;_;P;_;_;G;_;_;_; L;A;_;_;_;_;_;F;I;G;E;E;_;T;_; A;N;_;_;_;_;U;R;E;_;_;A;V;E;_; _;T;E;K;_;_;_;U;_;_;_;T;O;C;_; F;_;P;_;_;_;E;S;_;_;_;_;E;_;L; A;P;I;N;A;_;_;T;_;_;_;H;U;N;I; R;A;_;_;_;O;_;R;A;_;_;U;_;_;_; _;R;U;_;_;N;U;A;I;_;_;E;_;C;_; B;A;T;_;D;_;_;S;_;_;_;R;O;I;R; _;S;E;B;U;M;_;S;_;Q;_;A;_;_;A; D;O;_;A;_;I;R;R;_;U;N;I;O;N;_; _;L;_;T;_;_;_;_;M;E;_;T;U;_";

            //Configuration du nombre de joueur
            Console.WriteLine("Ceci est un jeu de scrabble. Nous allons commencer à jouer, veuillez tout d'abord nous donner le nombre de joueurs");
            int PlayerNumber = 0;
            while(PlayerNumber != 2 || PlayerNumber != 3 || PlayerNumber != 4)
            {
                Console.WriteLine("Le scrabble se joue avec 2 à 4 joueurs");
                PlayerNumber = Convert.ToInt32(Console.ReadLine());
            }

            //Création des joueurs 1 et 2 (+ 3 et/ou 4 si nécessaire)
            Console.WriteLine("\n\nJoueur 1, donne moi ton nom pour cette partie :");
            Joueur P1 = new Joueur(Convert.ToString(Console.ReadLine()));

            Console.WriteLine("\n\nJoueur 2, donne moi ton nom pour cette partie :");
            Joueur P2 = new Joueur(Convert.ToString(Console.ReadLine()));

            if (PlayerNumber > 2) //Joueur 3 si il doit y en voir un
            {
                Console.WriteLine("\n\nJoueur 3, donne moi ton nom pour cette partie :");
                Joueur P3 = new Joueur(Convert.ToString(Console.ReadLine()));
                if (PlayerNumber > 3) //Joueur 4 si il doit y en avoir un
                {
                    Console.WriteLine("\n\nJoueur 4, donne moi ton nom pour cette partie :"); 
                    Joueur P4 = new Joueur(Convert.ToString(Console.ReadLine()));
                }
            }

            //Création du plateau
            Console.WriteLine("\n\nVoulez-vous jouer avec un plateau vide, ou un plateau avec déjà un début de jeu ?\nMettez 1 pour vide ou 2 sinon : ");
            string answer = Convert.ToString(Console.Read());
            while(answer != "1" && answer != "2")
            {
                Console.WriteLine("\n\nMerci de mettre 1 pour vide ou 2 sinon : ");
                answer = Convert.ToString(Console.Read());
            }
            if (answer == "1")
            {
                Plateau monplateau = new Plateau();
            }
            else
            {
                Plateau monplateau = new Plateau(fichier);
            }
            //Création des timer
            DateTime GameStart = DateTime.Now; 
            TimeSpan TimerTotal = DateTime.Now - GameStart;
            Console.WriteLine("Combien de minutes voulez vous avoir à chaque tour de jeu ?");
            TimeSpan TimeAllowed = new TimeSpan(0, Convert.ToInt32(Console.ReadLine()), 0); //Nombre de minutes par tour
            
            while(TimerTotal.TotalMinutes <= 6)
            {
                



                TimerTotal = DateTime.Now - GameStart;
            }



            Console.ReadKey();
        }
    }
}
