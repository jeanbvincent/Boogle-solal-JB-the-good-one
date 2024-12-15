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
            get { return Score; }
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

            motsTrouves[motsTrouves.Count] = mot; //on ajoute le nouveau mot a la liste 
            
            int points = 0;            //on veut compter les points gagnés par le mot formé et les ajouter aux points totales du joueur 
            mot = mot.ToUpper();       //necessité de convertir le mot en majusules pour comparé chaque charactere du mot avec les lettres de notre classe lettre 
            for (int i = 0; i < mot.Length; i++)
            {
                for (int j = 0; j < lettres.Count; j++)  //on cherche savoir quelle lettre est le caractere mot[i]
                {
                    if (mot[i] == lettres[j].Caractere)
                    { 
                    points = points+ lettres[j].Points;  //on ajoute les point équivalent du caractere mot[i]
                    }
                }

            }
            score = score+points;  //on ajoute les points au score du joueur
        }

        public string toString()
        {
            string description = "";
            description = description + nom + " à: " + score + " points et à trouvés les mots suivants:" + "\n";

            for (int i = 0; i < motsTrouves.Count; i++)
            {
                description = description + motsTrouves[i]+ "\n";
            }
            return description;
        }


    }
}
