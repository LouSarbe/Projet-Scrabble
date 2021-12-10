using System;

namespace Projet_Scrabble
{
    class Jeu
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki; //Initialise la touche pressée
            Random r = new Random(); //Initialise l'aléatoire
            //Initialisation temporaire du plateau
            string fichier = "V;A;S;_;L;O;B;_;_;W;O;W;_;_;_;_;V;E;_;_;_;A;D;J;U;R;E;R;_;_;L; E;X;E;M;E;S;_;E;_;E;M;A;I;L;_; _;T;_;_;U;_;_;_;P;_;_;G;_;_;_; L;A;_;_;_;_;_;F;I;G;E;E;_;T;_; A;N;_;_;_;_;U;R;E;_;_;A;V;E;_; _;T;E;K;_;_;_;U;_;_;_;T;O;C;_; F;_;P;_;_;_;E;S;_;_;_;_;E;_;L; A;P;I;N;A;_;_;T;_;_;_;H;U;N;I; R;A;_;_;_;O;_;R;A;_;_;U;_;_;_; _;R;U;_;_;N;U;A;I;_;_;E;_;C;_; B;A;T;_;D;_;_;S;_;_;_;R;O;I;R; _;S;E;B;U;M;_;S;_;Q;_;A;_;_;A; D;O;_;A;_;I;R;R;_;U;N;I;O;N;_; _;L;_;T;_;_;_;_;M;E;_;T;U;_";

            //Création du dictionnaire
            Dictionnaire mondico = new Dictionnaire();
            
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

            Joueur P3 = null;
            Joueur P4 = null;

            //Création d'un tableau de joueur (qui nous permettra de sélectionner les bon joueurs à leur tour)
            Joueur[] P = new Joueur[4];
            P[0] = P1;
            P[1] = P2;
            
            if (PlayerNumber > 2) //Joueur 3 si il doit y en voir un
            {
                Console.WriteLine("\n\nJoueur 3, donne moi ton nom pour cette partie :");
                P3 = new Joueur(Convert.ToString(Console.ReadLine()));
                P[2] = P3;
                if (PlayerNumber > 3) //Joueur 4 si il doit y en avoir un
                {
                    Console.WriteLine("\n\nJoueur 4, donne moi ton nom pour cette partie :"); 
                    P4 = new Joueur(Convert.ToString(Console.ReadLine()));
                    P[3] = P4;
                }
            }

            //Quel joueur commence la partie
            int PlayerTurn = r.Next(0, PlayerNumber);

            //Création du plateau
            Console.WriteLine("\n\nVoulez-vous jouer avec un plateau vide, ou un plateau avec déjà un début de jeu ?\nMettez 1 pour vide ou 2 sinon : "); 
            string answer = Convert.ToString(Console.Read());
            while(answer != "1" && answer != "2") //On veut être sûr que l'utilisateur marque ce que l'on demande
            {
                Console.WriteLine("\n\nMerci de mettre 1 pour vide ou 2 sinon : ");
                answer = Convert.ToString(Console.Read());
            }

            Plateau monplateau;
            if (answer == "1") monplateau = new Plateau();
            else monplateau = new Plateau(fichier);

            //Création des timers
            DateTime GameStart = DateTime.Now; //Heure du début du jeu
            TimeSpan TimerTotal = DateTime.Now - GameStart; //Timer total de la session de jeu
            Console.WriteLine("Combien de secondes voulez vous avoir à chaque tour de jeu ?");
            TimeSpan TimeAllowed = new TimeSpan(0, 0, Convert.ToInt32(Console.ReadLine())); //Nombre de secondes par tour
            DateTime DebutTour = DateTime.Now; //Heure du début de tour
            TimeSpan TimerTour = DateTime.Now - DebutTour; //Timer du tour

            //Initialisation du sac de jetons
            Sac_Jetons sac = new Sac_Jetons();

            //Initialisation des mains
            for(int i = 0; i < PlayerNumber; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    P[i].Add_Main_Courante(sac.Retire_Jeton(r));
                }
                P[i].toString(); //Affiche les caractéristiques du joueur (dont sa main)
            }

            //Déclaration des variables motdouble et mottriple qui faciliteront le comptage des scores
            bool MotDouble;
            bool MotTriple;

            //Déclaration du score à ajouter au joueur en fin de tour
            int score;


            while (TimerTotal.TotalMinutes <= 6 && sac.Sac.Count > 0) //Début du jeu : le jeu s'arrête si on atteint 6 minutes
            {
                //Initialisation des paramètres
                answer = null;
                MotDouble = false;
                MotTriple = false;
                int ligne = 0;
                int colonne = 0;
                char direction = 'd';
                score = 0;

                P[PlayerTurn].Add_Main_Courante(sac.Retire_Jeton(r));

                DebutTour = DateTime.Now;

                while(!mondico.RechDichoRecursif(answer) || !monplateau.Test_Plateau(answer, ligne, colonne, direction)) //Tour du joueur
                {
                    Console.WriteLine("\n\nC'est au tour de " + P[PlayerTurn].Nom + " de jouer !\n" + P[PlayerTurn].toString()); //Affiche le joueur qui doit jouer et ses attributs
                    Console.WriteLine("\n\n" + monplateau.toString());
                    
                    cki = Console.ReadKey();
                    //Vérification du temps de tour
                    TimerTour = DateTime.Now - DebutTour;
                    if (cki.Key == ConsoleKey.Spacebar || TimerTour >= TimeAllowed) goto PasserLeTour; //Si le joueur appuie sur espace, il passe le tour sans poser de mot

                    Console.WriteLine("\nQuel mot voulez vous placer ? ");
                    answer = Convert.ToString(Console.ReadLine()); //Le joueur donne un mot à jouer

                    //Vérification du temps de tour
                    TimerTour = DateTime.Now - DebutTour;
                    if (TimerTour >= TimeAllowed) goto PasserLeTour;

                    //Le joueur place la première lettre dans une case et donne une direction pour jouer le mot
                    Console.WriteLine("Dans quelle ligne voulez vous mettre la première lettre ?");
                    ligne = Convert.ToInt32(Console.ReadLine());

                    //Vérification du temps de tour
                    TimerTour = DateTime.Now - DebutTour;
                    if (TimerTour >= TimeAllowed) goto PasserLeTour;

                    Console.WriteLine("Dans quelle colonne voulez vous mettre la première lettre ?");
                    colonne = Convert.ToInt32(Console.ReadLine());

                    //Vérification du temps de tour
                    TimerTour = DateTime.Now - DebutTour;
                    if (TimerTour >= TimeAllowed) goto PasserLeTour;

                    Console.WriteLine("Dans quelle direction voulez vous jouer la lettre ? Merci de mettre d pour droite, g pour gauche, h pour haut et b pour bas");
                    direction = Convert.ToChar(Console.ReadLine());

                    //Vérification du temps de tour
                    TimerTour = DateTime.Now - DebutTour;
                    if (TimerTour >= TimeAllowed) goto PasserLeTour;
                }
                
                //On place le mot dans le plateau
                for(int i = 0; i < answer.Length; i++) 
                {

                    if(direction == 'd') //Si la direction choisie est la droite
                    {
                        //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                        if (monplateau.Board[ligne, colonne + i] == '1') //Lettre double
                        {
                            score += Jeton[answer[i]].Points * 2;
                        }
                        else if (monplateau.Board[ligne, colonne + i] == '2') //Lettre triple
                        {
                            score += Jeton[answer[i]].Points * 3;
                        }
                        else if (monplateau.Board[ligne, colonne + i] == '3') //Mot double
                        {
                            MotDouble = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne, colonne + i] == '4') //Mot triple
                        {
                            MotTriple = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne, colonne + i] == '_') score += Jeton[answer[i]].Points; //Case vide
                        monplateau.Board[ligne, colonne + i] = answer[i];
                    }

                    else if (direction == 'g') //Si la direction choisie est la gauche
                    {
                        //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                        if (monplateau.Board[ligne, colonne - i] == '1') //Lettre double
                        {
                            score += Jeton[answer[i]].Points * 2; 
                        }
                        else if (monplateau.Board[ligne, colonne - i] == '2') //Lettre triple
                        {
                            score += Jeton[answer[i]].Points * 3;
                        }
                        else if (monplateau.Board[ligne, colonne - i] == '3') //Mot double
                        {
                            MotDouble = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne, colonne - i] == '4') //Mot triple
                        {
                            MotTriple = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne, colonne - i] == '_') score += Jeton[answer[i]].Points;
                        monplateau.Board[ligne, colonne - i] = answer[i];
                    }

                    else if (direction == 'h') //Si la direction choisie est le haut
                    {
                        //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                        if (monplateau.Board[ligne - i, colonne] == '1') //Lettre double
                        {
                            score += Jeton[answer[i]].Points * 2;
                        }
                        else if (monplateau.Board[ligne - i, colonne] == '2') //Lettre triple
                        {
                            score += Jeton[answer[i]].Points * 3;
                        }
                        else if (monplateau.Board[ligne - i, colonne] == '3') //Mot double
                        {
                            MotDouble = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne - i, colonne] == '4') //Mot triple
                        {
                            MotTriple = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne - i, colonne] == '_') score += Jeton[answer[i]].Points;
                        monplateau.Board[ligne - i, colonne] = answer[i];
                    }

                    else if (direction == 'b') //Si la direction choisie est le bas
                    {
                        //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                        if (monplateau.Board[ligne + i, colonne] == '1') //Lettre double
                        {
                            score += Jeton[answer[i]].Points * 2;
                        }
                        else if (monplateau.Board[ligne + i, colonne] == '2') //Lettre triple
                        {
                            score += Jeton[answer[i]].Points * 3;
                        }
                        else if (monplateau.Board[ligne + i, colonne] == '3') //Mot double
                        {
                            MotDouble = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne + i, colonne] == '4') //Mot triple
                        {
                            MotTriple = true;
                            score += Jeton[answer[i]].Points;
                        }
                        else if (monplateau.Board[ligne + i, colonne] == '_') score += Jeton[answer[i]].Points;
                        monplateau.Board[ligne + i, colonne] = answer[i];
                    }
                }

                P[PlayerTurn].Add_Mot(answer); //On rajoute le mot à la liste de mots trouvés

                //On rajoute ses points à son score
                if (MotDouble) P[PlayerTurn].Score += 2 * score;
                if (MotTriple) P[PlayerTurn].Score += 3 * score;
                else P[PlayerTurn].Score += score;

                //On enlève ses jetons utilisés de sa main
                for (int i = 0; i < monplateau.Jetons.Count; i++)
                    P[PlayerTurn].Remove_Main_Courante(monplateau.Jetons[i]);

                //On affiche ses informations ainsi que le plateau
                Console.WriteLine(P[PlayerTurn].toString());
                monplateau.toString();

                PasserLeTour: Console.WriteLine("Vous avez terminé votre tour ou votre temps est écoulé. Nous allons donc passer au joueur suivant");
                
                //On change le joueur qui va jouer ensuite
                if (PlayerTurn + 1 < PlayerNumber) PlayerTurn++;
                else PlayerTurn = 0;

                //On avance le timer total pour vérifier ensuite qu'on a pas dépasser les 6 minutes de jeu
                TimerTotal = DateTime.Now - GameStart;
            }



            Console.ReadKey();
        }
    }
}
