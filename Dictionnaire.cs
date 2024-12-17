using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle_Solal_JB
{
    internal class Dictionnaire
    {
        private List<string> mots;
        private string langue;
        public Dictionnaire(string langue)
        {
            string filePath = ""; // contient le chemin jusqu'au document MotsPossibles
            if (langue == "FR")
            {
                filePath = "C:\\Users\\solal\\Downloads\\MotsPossiblesFR.txt";
            }
            else if (langue != "FR")
            {
                filePath = "C:\\Users\\solal\\Downloads\\MotsPossiblesEN.txt";
            }
            string contenu = File.ReadAllText(filePath); // on lit le texte du document
            char[] delimiteurs = { ' ', '\n', '\r', '\t' }; // caractères séparant les mots (sauts de ligne, tabulations...)
            mots = new List<string>(contenu.Split(delimiteurs, StringSplitOptions.RemoveEmptyEntries)); // on sépare les mots (str -> liste de mots)
            mots.Sort(); // on trie la liste de mots par ordre alphabétique (comme ça c'est fait)
        }
        public List<string> Mots
        {
            get { return mots; }

        }
        public string toString() // décrit le dictionnaire (langue et mots par longueur/lettre)
        {
            int longueurMax = 0; // on cherche la taille maximale d'un mot dans le dictionnaire
            for (int i = 0; i < mots.Count; i++)
            {
                if (mots[i].Length > longueurMax)
                {
                    longueurMax = mots[i].Length;
                }
            }
            int[] motsParLongueur = new int[longueurMax + 1];
            int[] motsParLettre = new int[26];
            for (int i = 0; i < mots.Count; i++)
            {
                string mot = mots[i]; // on prend le mot actuel en paramètre
                motsParLongueur[mot.Length]++; // on incrémente à la bonne longueur
                char premiereLettre = char.ToUpper(mot[0]);
                if (premiereLettre >= 'A' && premiereLettre <= 'Z')
                {
                    motsParLettre[premiereLettre - 'A']++; // on regarde la distance de la lettre à A correspondante
                }
            }
            string resultat = $"Langue : {langue}, Nombre total de mots : {mots.Count}";
            resultat += ", Nombre de mots par longueur : ";
            for (int i = 1; i <= longueurMax; i++) // on ignore la longueur 0
            {
                if (motsParLongueur[i] > 0)
                {
                    resultat += $"Longueur {i} : {motsParLongueur[i]} mots, "; // on écrit pour chaque longueur
                }
            }
            resultat += "Nombre de mots par première lettre : ";
            for (int i = 0; i < 26; i++)
            {
                if (motsParLettre[i] > 0)
                {
                    char lettre = (char)('A' + i);
                    resultat += $"Lettre '{lettre}' : {motsParLettre[i]} mots, "; // on écrit pour chaque première lettre
                }
            }
            return resultat;
        }
        public bool Recherche(string mot)
        {
            bool rps = false;
            for (int i = 0; i < mots.Count; i++)
            {


                if (mots[i] == mot)
                {
                    rps = true;
                }

            }
            return rps;
        }
        public bool RechDichoRecursif(string mot, int debut, int fin)
        {
            if (debut > fin)
            {
                return false; // cas de base où le mot n'est pas dans la liste
            }
            int milieu = (debut + fin) / 2; // calcul de l'indice du milieu
            if (mots[milieu] == mot) // on compare le mot cherché au mot à l'indice du milieu
            {
                return true;
            }
            else if (mot.CompareTo(mots[milieu]) < 0) // mot plus petit que le milieu, on recherche à gauche
            {
                return RechDichoRecursif(mot, debut, milieu - 1);
            }
            else // sinon on recherche à droite
            {
                return RechDichoRecursif(mot, milieu + 1, fin);
            }
        }
    }
}
