using Boggle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle_Solal_JB
{
    internal class Joueur
    {
        string nom;
        int score;
        List<string> motsTrouves;
        List<Lettre> lettres;

        public Joueur(string nom, List<Lettre> lettres)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouves = new List<string>();
            this.lettres = lettres;
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public List<string> MotsTrouves
        {
            get { return motsTrouves; }
            set { motsTrouves = value; }
        }

        public bool Contain(string mot)
        {
            bool rps = false;

            for (int i = 0; i < motsTrouves.Count; i++)
            {
                if (mot == motsTrouves[i])
                {
                    rps = true;
                }
            }
            return rps;
        }

        public void Add_Mot(string mot)
        {
            // Vérifie si le mot est déjà dans la liste
            if (Contain(mot))
            {
                Console.WriteLine($"Erreur : Le mot \"{mot}\" a déjà été trouvé par {nom}.");
                return; // Arrête l'exécution si le mot est déjà trouvé
            }

            // Ajoute le nouveau mot à la liste
            motsTrouves.Add(mot);

            int points = 0; // Compteur pour les points gagnés
            mot = mot.ToUpper(); // Convertir le mot en majuscules pour la comparaison
            for (int i = 0; i < mot.Length; i++)
            {
                for (int j = 0; j < lettres.Count; j++) // Chercher les points pour chaque lettre du mot
                {
                    if (mot[i] == lettres[j].Caractere)
                    {
                        points += lettres[j].Points; // Ajoute les points correspondants
                    }
                }
            }

            score += points; // Ajoute les points au score du joueur
        }


        public string toString()
        {
            string description = "";
            description = description + nom + " à: " + score + " points et à trouvés les mots suivants:" + "\n";

            for (int i = 0; i < motsTrouves.Count; i++)
            {
                description = description + motsTrouves[i] + "\n";
            }
            return description;
        }


    }
}
