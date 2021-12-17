using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Scrabble
{
    class Jeu
    { 
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki; //Initialise la touche pressée
            Random r = new Random(); //Initialise l'aléatoire

            //Création du dictionnaire
            Dictionnaire mondico = new Dictionnaire("Francais.txt");

            //Configuration du nombre de joueur
            Console.WriteLine("Ceci est un jeu de scrabble. Nous allons commencer à jouer, veuillez tout d'abord nous donner le nombre de joueurs");
            int PlayerNumber = Convert.ToInt32(Console.ReadLine());
            while (PlayerNumber != 2 && PlayerNumber != 3 && PlayerNumber != 4)
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

            string answer; //Création d'une string qui me servira à stocker les réponses utilisateur

            //Quel joueur commence la partie
            int PlayerTurn = r.Next(0, PlayerNumber);

            //Création du plateau
            Plateau monplateau = new Plateau();

            //Création des timers
            DateTime GameStart = DateTime.Now; //Heure du début du jeu
            TimeSpan TimerTotal = DateTime.Now - GameStart; //Timer total de la session de jeu
            Console.WriteLine("\n\nCombien de secondes voulez vous avoir à chaque tour de jeu ?");
            TimeSpan TimeAllowed = new TimeSpan(0, 0, Convert.ToInt32(Console.ReadLine())); //Nombre de secondes par tour
            DateTime DebutTour = DateTime.Now; //Heure du début de tour
            TimeSpan TimerTour = DateTime.Now - DebutTour; //Timer du tour

            //Initialisation du sac de jetons
            Sac_Jetons sac = new Sac_Jetons("Jetons.txt");

            //Initialisation des mains
            for (int i = 0; i < PlayerNumber; i++)
            {
                for (int j = 0; j < 6; j++)
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

            //Déclaration d'une variable qui regarde si le joueur a le jeton qu'il veut jouer dans sa main
            bool possedejeton;


            while (TimerTotal.TotalMinutes <= 30 && sac.Nombre > 0) //Début du jeu : le jeu s'arrête si on atteint 30 minutes
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

            //Tour du joueur
            DEBUTDETOUR:
                possedejeton = true;
                Console.WriteLine("\n\nC'est au tour de " + P[PlayerTurn].Nom + " de jouer !\n" + P[PlayerTurn].toString()); //Affiche le joueur qui doit jouer et ses attributs
                Console.WriteLine("\n\n" + monplateau.toString());

                //Vérification du temps de tour
                TimerTour = DateTime.Now - DebutTour;
                if (TimerTour >= TimeAllowed) goto PasserLeTour;

                else
                {
                    //Le joueur choisit un mot à  jouer
                    Console.WriteLine("\nQuel mot voulez vous placer ? ");
                    do
                    {
                        Console.WriteLine("Si vous voulez passer votre tour, appuyez sur espace. Sinon appuyez sur n'importe quelle touche");
                        cki = Console.ReadKey();
                        if (cki.Key == ConsoleKey.Spacebar) goto PasserLeTour; //Si le joueur appuie sur espace, il passe le tour sans poser de mot
                        Console.WriteLine("Quel mot voulez vous placer ?");
                        answer = Convert.ToString(Console.ReadLine()).ToUpper(); //Enregistre la réponse de l'utilisateur, en lettres majuscules
                        if (!mondico.RechercheMot(answer)) Console.WriteLine("Le mot " + answer + " n'appartient pas au dictionnaire");
                    } while (!mondico.RechercheMot(answer));

                    //Vérification du temps de tour
                    TimerTour = DateTime.Now - DebutTour;
                    if (TimerTour >= TimeAllowed) goto PasserLeTour;

                    do
                    {
                        //Ligne de la première lettre
                        Console.WriteLine("Dans quelle ligne voulez vous mettre la première lettre ?");
                        ligne = Convert.ToInt32(Console.ReadLine()) - 1;

                        //Vérification du temps de tour
                        TimerTour = DateTime.Now - DebutTour;
                        if (TimerTour >= TimeAllowed) goto PasserLeTour;

                        //Colonne de la première lettre
                        Console.WriteLine("Dans quelle colonne voulez vous mettre la première lettre ?");
                        colonne = Convert.ToInt32(Console.ReadLine()) - 1;

                        //Vérification du temps de tour
                        TimerTour = DateTime.Now - DebutTour;
                        if (TimerTour >= TimeAllowed) goto PasserLeTour;

                        //Choix de la direction
                        Console.WriteLine("Dans quelle direction voulez vous jouer la lettre ? Merci de mettre d pour droite, g pour gauche, h pour haut et b pour bas");
                        direction = Convert.ToChar(Console.ReadLine());

                        //Vérification du temps de tour
                        TimerTour = DateTime.Now - DebutTour;
                        if (TimerTour >= TimeAllowed) goto PasserLeTour;
                    } while (!monplateau.Test_Plateau(answer, ligne, colonne, direction));

                    //Boucle qui vérifie que le joueur a les bons jetons pour faire ce qu'il demande
                    for (int i = 0; i < monplateau.Jetons.Count; i++)
                    {
                        if (possedejeton)
                        {
                            //Vérifie que le joueur a le jeton correspondant ou un jeton joker
                            if (!P[PlayerTurn].Existe(monplateau.Jetons[i]) && !P[PlayerTurn].Existe('*')) possedejeton = false;
                            else if (P[PlayerTurn].Existe('*')) //Si le joueur utilise un joker, il est retiré instantanément
                            {
                                monplateau.Jetons.RemoveAt(i);
                                P[PlayerTurn].Remove_Main_Courante('*');
                            }
                        }
                    }

                    if (!possedejeton) //Si le joueur n'a pas les bons jetons, alors il est renvoyé au début de son tour

                    {
                        Console.WriteLine("Vous n'avez pas les bons jetons, veuillez recommencer votre tour !");
                        goto DEBUTDETOUR;
                    }

                    //On place le mot dans le plateau et on s'occupe du score
                    for (int i = 0; i < answer.Length; i++)
                    {

                        if (direction == 'd') //Si la direction choisie est la droite
                        {
                            //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                            if (monplateau.Board[ligne, colonne + i] == '1') //Lettre double
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 2;
                            }
                            else if (monplateau.Board[ligne, colonne + i] == '2') //Lettre triple
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 3;
                            }
                            else if (monplateau.Board[ligne, colonne + i] == '3') //Mot double
                            {
                                MotDouble = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne, colonne + i] == '4') //Mot triple
                            {
                                MotTriple = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne, colonne + i] == '_') score += P[PlayerTurn].Valeur[answer[i]]; //Case vide
                            monplateau.Board[ligne, colonne + i] = answer[i];
                        }

                        else if (direction == 'g') //Si la direction choisie est la gauche
                        {
                            //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                            if (monplateau.Board[ligne, colonne - i] == '1') //Lettre double
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 2;
                            }
                            else if (monplateau.Board[ligne, colonne - i] == '2') //Lettre triple
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 3;
                            }
                            else if (monplateau.Board[ligne, colonne - i] == '3') //Mot double
                            {
                                MotDouble = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne, colonne - i] == '4') //Mot triple
                            {
                                MotTriple = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne, colonne - i] == '_') score += P[PlayerTurn].Valeur[answer[i]];
                            monplateau.Board[ligne, colonne - i] = answer[i];
                        }

                        else if (direction == 'h') //Si la direction choisie est le haut
                        {
                            //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                            if (monplateau.Board[ligne - i, colonne] == '1') //Lettre double
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 2;
                            }
                            else if (monplateau.Board[ligne - i, colonne] == '2') //Lettre triple
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 3;
                            }
                            else if (monplateau.Board[ligne - i, colonne] == '3') //Mot double
                            {
                                MotDouble = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne - i, colonne] == '4') //Mot triple
                            {
                                MotTriple = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne - i, colonne] == '_') score += P[PlayerTurn].Valeur[answer[i]];
                            monplateau.Board[ligne - i, colonne] = answer[i];
                        }

                        else if (direction == 'b') //Si la direction choisie est le bas
                        {
                            //On vérifie ce qu'il y a dans la case (case vide, spéciale ou occuppée par la même lettre) et on agit en fontion de la situation
                            if (monplateau.Board[ligne + i, colonne] == '1') //Lettre double
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 2;
                            }
                            else if (monplateau.Board[ligne + i, colonne] == '2') //Lettre triple
                            {
                                score += P[PlayerTurn].Valeur[answer[i]] * 3;
                            }
                            else if (monplateau.Board[ligne + i, colonne] == '3') //Mot double
                            {
                                MotDouble = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne + i, colonne] == '4') //Mot triple
                            {
                                MotTriple = true;
                                score += P[PlayerTurn].Valeur[answer[i]];
                            }
                            else if (monplateau.Board[ligne + i, colonne] == '_') score += P[PlayerTurn].Valeur[answer[i]];
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
                }

            PasserLeTour: Console.WriteLine("Vous avez terminé votre tour ou votre temps est écoulé. Nous allons donc passer au joueur suivant");

                //On change le joueur qui va jouer ensuite
                if (PlayerTurn + 1 < PlayerNumber) PlayerTurn++;
                else PlayerTurn = 0;

                //On avance le timer total pour vérifier ensuite qu'on a pas dépasser les 6 minutes de jeu
                TimerTotal = DateTime.Now - GameStart;
            }

            //Enregistrement de la partie ? Possibilité de reprendre en cours de partie ? Test unitaire ? Gestion de la main ? Commencer au milieu ?
        }
    }
}
