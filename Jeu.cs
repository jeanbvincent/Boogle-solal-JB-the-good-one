using System;
using System.Collections.Generic;
using WordCloudSharp; /// wordCloudSharp est la bibliothèque utilisée pour faire le nuage de mots
using System.Drawing;

namespace Projet_Boogle_Solal_JB
{
    internal class Jeu
    {
        /// attributs nécessaires pour gérer une partie de Boggle
        private List<Lettre> lettres;
        private Dictionnaire dictionnaire;
        private Joueur joueur1;
        private Joueur joueur2;
        private Plateau plateau;
        private TimeSpan dureeLimite;

        /// constructeur pour initialiser les données nécessaires au jeu
        public Jeu()
        {
            lettres = ChargerLettres("Lettres.txt");
            dureeLimite = TimeSpan.FromMinutes(1); /// durée limite pour chaque tour
        }

        /// méthode pour démarrer le jeu
        public void Demarrer()
        {
            Console.WriteLine("---BOGGLE---");
            while (true) /// on fait une boucle pour permettre de relancer la partie si les joueurs veulent recommencer
            {
                InitialiserPartie();
                JouerTour(joueur1);
                plateau.Lance(); /// relance le plateau pour le deuxième joueur
                JouerTour(joueur2);
                AfficherResultats();
                GenererNuageDeMots();

                if (!DemanderRejouer())
                {
                    Console.WriteLine("\nMerci d'avoir joué ! À bientôt !");
                    break;
                }
            }
        }

        /// méthode pour charger les lettres depuis un fichier texte
        public List<Lettre> ChargerLettres(string filePath)
        {
            List<Lettre> lettres = new List<Lettre>();
            foreach (string ligne in File.ReadLines(filePath))
            {
                string[] parties = ligne.Split(';');
                if (parties.Length == 3)
                {
                    char caractere = char.Parse(parties[0]);
                    int points = int.Parse(parties[1]);
                    int occurrences = int.Parse(parties[2]);
                    Lettre lettre = new Lettre(caractere, points, occurrences);
                    lettres.Add(lettre); /// on ajoute nos 26 lettres à la classe lettre (avec ce fonctionnement on peut utiliser n'importe quel alphabet)
                }
            }
            return lettres;
        }

        /// méthode pour initialiser une partie (joueurs, dictionnaire, plateau)
        public void InitialiserPartie()
        {
            string langue = "";
            while (langue != "FR" && langue != "EN") /// relance en boucle tant que l'utilisateur ne saisit pas de valeur correcte
            {
                Console.WriteLine("\nLangue du jeu : Tapez FR pour français ou EN pour anglais"); /// demande la langue du jeu à l'utilisateur
                langue = Console.ReadLine()?.ToUpper();
            }
            dictionnaire = new Dictionnaire(langue);
            Console.WriteLine("\nNom du premier joueur :");
            joueur1 = new Joueur(Console.ReadLine(), lettres);
            Console.WriteLine("\nNom du deuxième joueur :");
            joueur2 = new Joueur(Console.ReadLine(), lettres);
            plateau = new Plateau(lettres);
        }

        /// méthode pour jouer un tour pour un joueur donné
        public void JouerTour(Joueur joueur)
        {
            Console.WriteLine($"\n--- Tour de {joueur.Nom} ---\n");
            Console.WriteLine(plateau.toString());
            DateTime debut = DateTime.Now;
            while (DateTime.Now - debut < dureeLimite)
            {
                Console.WriteLine("Tapez un mot");
                string essaie = Console.ReadLine()?.ToUpper();
                if (plateau.Test_Plateau(essaie)) /// teste si le mot existe dans le plateau
                {
                    if (dictionnaire.RechDichoRecursif(essaie, 0, dictionnaire.Mots.Count)) /// teste si le mot existe dans le dictionnaire
                    {
                        joueur.Add_Mot(essaie);
                    }
                    else
                    {
                        Console.WriteLine($"Le mot {essaie} n'appartient pas au dictionnaire !"); /// renvoie les messages d'erreurs correspondant
                    }
                }
                else
                {
                    Console.WriteLine($"Le mot {essaie} n'appartient pas au plateau !");
                }
            }
            Console.WriteLine($"\nTemps écoulé pour {joueur.Nom} !");
        }

        /// méthode pour afficher les résultats finaux
        public void AfficherResultats()
        {
            Console.WriteLine("\n--- Résultats finaux ---\n");
            Console.WriteLine(joueur1.toString()); /// on utilise toString pour afficher les scores des joueurs
            Console.WriteLine(joueur2.toString());
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
                Console.WriteLine($"Égalité parfaite entre {joueur1.Nom} et {joueur2.Nom} !");
            }
        }

        /// méthode pour générer un nuage de mots
        public void GenererNuageDeMots()
        {
            List<string> listeMotsNuage = joueur1.MotsTrouves.Concat(joueur2.MotsTrouves).Distinct().ToList();
            List<int> listeFrequencesNuage = new List<int>(new int[listeMotsNuage.Count]); /// tous les mots ont une fréquence égale à 1
            for (int i = 0; i < listeFrequencesNuage.Count; i++)
            {
                listeFrequencesNuage[i] = 1;
            }
            var wordCloud = new WordCloudSharp.WordCloud(800, 600);
            var image = wordCloud.Draw(listeMotsNuage, listeFrequencesNuage, Color.White); /// on dessine le nuage de mots
            string cheminImage = Path.GetFullPath("nuage_mots.png");
            image.Save(cheminImage);
            Console.WriteLine($"\nLe nuage de mots a été enregistré ici : {cheminImage}"); /// on précise aux joueurs où le nuage a été généré
        }

        /// méthode pour demander si les joueurs veulent rejouer
        public bool DemanderRejouer()
        {
            string rejouer;
            do
            {
                Console.WriteLine("\nVoulez-vous rejouer ? (O/N)"); /// permet aux joueurs de relancer la partie
                rejouer = Console.ReadLine()?.ToUpper();
                if (rejouer != "O" && rejouer != "N")
                {
                    Console.WriteLine("Entrée invalide. Veuillez taper 'O' pour rejouer ou 'N' pour quitter.");
                }
            } while (rejouer != "O" && rejouer != "N"); /// do while pour vérifier que les joueurs entrent une valeur correcte
            return rejouer == "O";
        }
    }
}
