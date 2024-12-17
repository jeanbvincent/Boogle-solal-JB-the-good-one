using Boggle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle_Solal_JB
{
    internal class De
    {
        Lettre[] lettres;                 // ensemble des 6 faces du dé composés d'une lettre de Classe Lettre 
        Lettre face;                      // face tournée vers le haut, indique la Lettre qui sera visible 
        List<Lettre> liste;
        public De(List<Lettre> liste)
        {
            this.liste = liste;
            Lettre[] lettres = new Lettre[6];                    // dé est un tableau de 6 lettres (de classe Lettre), il y a une Lettre par face
            for (int i = 0; i < 6; i++)
            {
                Random rdm = new Random();
                int valeur = -1;
                while (valeur == -1 || liste[valeur].Occurence < 0)   //tant qu'on a épuisé le nombre total de fois qu'on peut ajouté une lettre dans le jeu de dés (lettre.Occurence==0) on cherche une autre lettre au hasard 
                {
                    valeur = rdm.Next(0, 26); // on choisit un entier entre 0 et 26 inclus 
                    lettres[i] = liste[valeur]; // la face i du dé prendra la lettre de l'index "valeur" de la liste "liste" de Lettre.
                    liste[valeur].Occurence--;    // On diminue de 1 le nombre de fois que la lettre peut apparaitre dans le tableau  
                }
            }
            this.lettres = lettres;
            Random random = new Random();
            int r = random.Next(6);
            this.face = lettres[r];                    // on tire au hasard la face tournée vers le haut
        }
        public Lettre[] Lettres
        {
            get { return lettres; }
            set { lettres = value; }
        }
        public Lettre Face
        {
            get { return face; }
            set { face = value; }
        }
        public List<Lettre> Liste
        {
            get { return liste; }
            set { liste = value; }
        }
        public string toString()
        {
            string retour = $"Les faces sont {lettres[0].Caractere}, {lettres[1].Caractere}, {lettres[2].Caractere}, {lettres[3].Caractere}, {lettres[4].Caractere}, {lettres[5].Caractere} et la face vers le haut est {face.Caractere}.";
            return retour;
        }
        public void Lance(Random r)
        {
            int valeur = r.Next(6);
            face = lettres[valeur];
        }
    }
}
