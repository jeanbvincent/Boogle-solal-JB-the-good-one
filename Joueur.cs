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
        public bool Add_Mot(string mot)
        {
            if (motsTrouves.Contains(mot))
            {
                Console.WriteLine($"Vous avez déjà trouvé le mot {mot} !");
                return false; // Le mot est déjà dans la liste
            }
            motsTrouves.Add(mot); // On ajoute le mot à la liste
            int points = 0; // Calcul des points
            mot = mot.ToUpper();
            for (int i = 0; i < mot.Length; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    if (mot[i] == lettres[j].Caractere)
                    {
                        points += lettres[j].Points;
                    }
                }
            }
            score += points; // Ajout des points au score
            Console.WriteLine($"+ {points}pts !");
            return true; // Mot ajouté avec succès
        }

        public int CalculerPointsMot(string mot)
        {
            int points = 0;
            mot = mot.ToUpper();
            for (int i = 0; i < mot.Length; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    if (mot[i] == lettres[j].Caractere)
                    {
                        points += lettres[j].Points;
                    }
                }
            }
            return points;
        }
        public string toString()
        {
            string description = "";
            description = $"{nom} a {score} points et a trouvé les mots suivants :\n";
            for (int i = 0; i < motsTrouves.Count; i++)
            {
                description = description + $"{motsTrouves[i]} (+{CalculerPointsMot(motsTrouves[i])}pts) \n";
            }
            return description;
        }
    }
}
