using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle_Solal_JB
{
    internal class Lettre
    {
        char caractere;
        int points;
        int occurence;

        public char Caractere
        {
            get { return caractere; }
            set { caractere = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public int Occurence
        {
            get { return occurence; }
            set { occurence = value; }
        }

        public Lettre(char caractere, int points, int occurence)
        {
            this.caractere = caractere;
            this.points = points;
            this.occurence = occurence;

        }

        public List<Lettre> Convert(string mot, List<Lettre> lettres )
        {
             List<Lettre> motLettres = new List<Lettre>();
            for (int i = 0; i < mot.Length; i++)
            {
                for (int j = 0; j < lettres.Count; j++)
                {
                    if (mot[i] == lettres[j].Caractere)
                    {
                        motLettres[i]= lettres[j];
                    }
                }
            }
             return motLettres;

        }

    }
}
