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
        List<string> motsTrouves; /// liste des mots trouvés par le joueur
        List<Lettre> lettres; /// liste des lettres composant les mots trouvés par le joueur
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
        public bool Contain(string mot) /// vérifie si le joueur a déjà trouvé le mot mis en paramètre
        {
            bool retour = false;
            for (int i = 0; i < motsTrouves.Count; i++)
            {
                if (mot == motsTrouves[i]) /// on compare simplement le mot à chaque mot déjà trouvé
                {
                    retour = true;
                }
            }
            return retour;
        }
        public bool Add_Mot(string mot) /// ajoute le mot en paramètre à la liste des mots trouvés par le joueur
        {
            if (motsTrouves.Contains(mot))
            {
                Console.WriteLine($"Vous avez déjà trouvé le mot {mot} !"); /// on n'éxecute pas si le mot a déjà été trouvé
                return false;
            }
            motsTrouves.Add(mot);
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
            score += points; /// on ajoute les points marqués par les lettres du mot au score du joueur
            Console.WriteLine($"+ {points}pts !");
            return true;
        }

        public int CalculerPointsMot(string mot) /// calcule les points marqués par un mot donné
        {
            int points = 0;
            mot = mot.ToUpper();
            for (int i = 0; i < mot.Length; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    if (mot[i] == lettres[j].Caractere)
                    {
                        points += lettres[j].Points; /// on ajoute simplement le score de chaque lettre à la valeur retournée
                    }
                }
            }
            return points;
        }
        public string toString() /// donne le score du joueur et le nombre de points qu'il a marqué
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
