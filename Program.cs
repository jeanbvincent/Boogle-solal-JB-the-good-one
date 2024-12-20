using System;
using System.Collections.Generic;
using WordCloudSharp;
using System.Drawing;
using WordCloud;

namespace Projet_Boogle_Solal_JB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Lettres.txt";
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
            string langue = "";
            langue = langue.ToUpper();
            while (langue != "fr" && langue != "en" && langue != "FR" && langue != "EN") //on demande a l'utilisateur de chosir de jouer en francais ou anglais
            {
                Console.WriteLine("Langue du jeu : \nTapez FR pour francais ou EN pour anglais");
                langue = Console.ReadLine();
            }
            Dictionnaire dictionnaire = new Dictionnaire(langue);  //creation dictionnaire
            Console.WriteLine("\nNom du premier joueur :"); //Création des deux joueurs
            Joueur joueur1 = new Joueur(Console.ReadLine(), lettres);
            Console.WriteLine("\nNom du deuxieme joueur :");
            Joueur joueur2 = new Joueur(Console.ReadLine(), lettres);
            Console.WriteLine($"\n---Tour de {joueur1.Nom}---\n");
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
                        joueur1.Add_Mot(essaie); // ajoute le mot et calcule les points (sauf si mot déjà trouvé)
                    }
                    else
                    {
                        Console.WriteLine($"Le mot {essaie} n'appartient pas au dictionnaire !");
                    }
                }
                else
                {
                    Console.WriteLine($"Le mot {essaie} n'appartient pas au plateau !");
                }
                if (DateTime.Now - debut >= dureeLimite)
                {
                    Console.WriteLine($"\nTemps écoulé pour {joueur1.Nom} !");
                    break;
                }
            }
            // Tour du joueur 2
            plateau.Lance(); // Lancer un nouveau plateau
            Console.WriteLine($"\n---Tour de {joueur2.Nom}---\n");
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
                        joueur2.Add_Mot(essaie); // ajoute le mot et calcule les points (sauf si mot déjà trouvé)
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
                    Console.WriteLine($"\nTemps écoulé pour {joueur2.Nom} !");
                    break;
                }
            }
            // Fin de la partie : afficher les scores et déterminer le gagnant
            Console.WriteLine("\n--- Résultats finaux ---\n");
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
            // Calcul de la différence de points
            int scoreJoueur1 = joueur1.Score;
            int scoreJoueur2 = joueur2.Score;
            int difference = Math.Abs(scoreJoueur1 - scoreJoueur2); // Différence absolue
            int scoreMax = Math.Max(scoreJoueur1, scoreJoueur2);    // Score le plus élevé
            // Affichage du résultat en fonction de la fraction
            if (difference <= scoreMax / 4)
            {
                Console.WriteLine("Victoire serrée ! (1/4 d'écart !)");
            }
            else if (difference <= scoreMax / 2)
            {
                Console.WriteLine("Belle victoire ! (1/2 d'écart !)");
            }
            else if (difference <= (3 * scoreMax) / 4)
            {
                Console.WriteLine("Victoire écrasante ! (3/4 d'écart !)");
            }
            else
            {
                Console.WriteLine("Haagra totale ! (victoire totale !)");
            }
            // Fusionner les listes de mots trouvés par les deux joueurs
            List<string> listeMotsNuage = new List<string>();
            listeMotsNuage.AddRange(joueur1.MotsTrouves);
            listeMotsNuage.AddRange(joueur2.MotsTrouves);
            // Retirer les doublons (mots trouvés par les deux joueurs)
            listeMotsNuage = listeMotsNuage.Distinct().ToList();
            List<int> listeFrequencesNuage = new List<int>(new int[listeMotsNuage.Count]); // Liste de fréquences égales à 1
            for (int i = 0; i < listeFrequencesNuage.Count; i++)
            {
                listeFrequencesNuage[i] = 1; // Assigner la même fréquence (1) pour tous les mots
            }
            var wordCloud = new WordCloudSharp.WordCloud(800, 600);
            var image = wordCloud.Draw(listeMotsNuage,listeFrequencesNuage, Color.White);
            string cheminImage = Path.GetFullPath("nuage_mots.png");
            image.Save(cheminImage);
            Console.WriteLine($"\nLe nuage de mots a été enregistré ici : {cheminImage}");
            //Fin du programme
            Console.WriteLine("\nAppuyez sur une touche pour quitter.");
            Console.ReadKey();
        }
    }
}
