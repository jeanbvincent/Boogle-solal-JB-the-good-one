using System;
using System.Collections.Generic;

namespace Projet_Boogle_Solal_JB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\solal\\Downloads\\Lettres.txt";
            List<Lettre> lettres = new List<Lettre>(); //creation d'un liste avec les lettre utilises

            //on utilisera pas de try{} catch{} puisque on connait deja le fichier "lettres"

            foreach (string ligne in File.ReadLines(filePath))   // Lire chaque ligne du fichier
            {
                // Séparer les valeurs par le point-virgule grace à .Split()
                string[] parties = ligne.Split(';');             // La methode Split() est utilisée pour diviser une chaîne en un tableau "parties"(de longeur 3) de sous-chaînes                                                
                if (parties.Length == 3)
                {
                    char caractere = char.Parse(parties[0]);     // On utilisera Parse au lieu de Convert.To puisque on veut une conversion stricte et spécifique au type
                    int points = int.Parse(parties[1]);          // Convert.To est plus permissif
                    int occurrences = int.Parse(parties[2]);

                    // Créer un nouveau objet Lettre et l'ajouter à la liste
                    Lettre lettre = new Lettre(caractere, points, occurrences);
                    lettres.Add(lettre);
                }
            }
            foreach (Lettre lettre in lettres)
            {
                Console.Write(lettre.Caractere + " ");
                Console.Write(lettre.Points + " ");
                Console.WriteLine(lettre.Occurence);

            }
            Console.WriteLine();


            string langue = "";
            langue = langue.ToUpper();
            while (langue != "fr" && langue != "en" && langue != "FR" && langue != "EN") //on demande a l'utilisateur de chosir de jouer en francais ou anglais
            {
                Console.WriteLine("Langue du jeu: \n tapez fr pour francais ou en pour anglais");
                langue = Console.ReadLine();
            }
            Dictionnaire dictionnaire = new Dictionnaire(langue);  //creation dictionnaire

            Console.WriteLine("Nom du premier joueur :"); //Création des deux joueurs
            Joueur joueur1 = new Joueur(Console.ReadLine(), lettres);
            Console.WriteLine("Nom du deuxieme joueur");
            Joueur joueur2 = new Joueur(Console.ReadLine(), lettres);


            Console.WriteLine("Tour de " + joueur1.Nom);
            Plateau plateau = new Plateau(lettres); // création du plateau
            Console.WriteLine(plateau.toString());

            TimeSpan dureeLimite = TimeSpan.FromMinutes(1); // Limite de temps pour chaque joueur
            DateTime debut;

            // Tour du joueur 1
            debut = DateTime.Now;
            while (DateTime.Now - debut < dureeLimite)
            {
                Console.WriteLine("Tapez un mot");
                string essaie = Console.ReadLine();
                essaie = essaie.ToUpper();

                if (plateau.Test_Plateau(essaie)) // vérifie que le mot est dans le plateau
                {
                    if (dictionnaire.RechDichoRecursif(essaie, 0, dictionnaire.Mots.Count)) // vérifie que le mot est dans le dictionnaire
                    {
                        joueur1.Add_Mot(essaie); // ajoute le mot et calcule les points
                    }
                    else
                    {
                        Console.WriteLine("Le mot n'appartient pas au dictionnaire!");
                    }
                }
                else
                {
                    Console.WriteLine("Le mot n'appartient pas au plateau!");
                }

                if (DateTime.Now - debut >= dureeLimite)
                {
                    Console.WriteLine("Temps écoulé pour " + joueur1.Nom + "!");
                    break;
                }
            }

            // Tour du joueur 2
            plateau.Lance(); // Lancer un nouveau plateau
            Console.WriteLine("Tour de " + joueur2.Nom);
            Console.WriteLine(plateau.toString());

            debut = DateTime.Now;
            while (DateTime.Now - debut < dureeLimite)
            {
                Console.WriteLine("Tapez un mot");
                string essaie = Console.ReadLine();
                essaie = essaie.ToUpper();

                if (plateau.Test_Plateau(essaie)) // vérifie que le mot est dans le plateau
                {
                    if (dictionnaire.RechDichoRecursif(essaie, 0, dictionnaire.Mots.Count)) // vérifie que le mot est dans le dictionnaire
                    {
                        joueur2.Add_Mot(essaie); // ajoute le mot et calcule les points
                    }
                    else
                    {
                        Console.WriteLine("Le mot n'appartient pas au dictionnaire!");
                    }
                }
                else
                {
                    Console.WriteLine("Le mot n'appartient pas au plateau!");
                }

                if (DateTime.Now - debut >= dureeLimite)
                {
                    Console.WriteLine("Temps écoulé pour " + joueur2.Nom + "!");
                    break;
                }
            }

            // Fin de la partie : afficher les scores et déterminer le gagnant
            Console.WriteLine("\n--- Résultats finaux ---");

            // Récapitulatif du joueur 1
            Console.WriteLine(joueur1.toString());

            // Récapitulatif du joueur 2
            Console.WriteLine(joueur2.toString());

            // Annonce du gagnant
            if (joueur1.Score > joueur2.Score)
            {
                Console.WriteLine($"Félicitations {joueur1.Nom}, vous êtes le gagnant avec {joueur1.Score} points !");
            }
            else if (joueur1.Score < joueur2.Score)
            {
                Console.WriteLine($"Félicitations {joueur2.Nom}, vous êtes le gagnant avec {joueur2.Score} points !");
            }
            else
            {
                Console.WriteLine($"Égalité parfaite entre {joueur1.Nom} et {joueur2.Nom} avec {joueur1.Score} points chacun !");
            }

            // Fin du programme
            Console.WriteLine("\nMerci d'avoir joué à Boggle !");
            Console.WriteLine("Appuyez sur une touche pour quitter.");
            Console.ReadKey();
        }
    }
}
