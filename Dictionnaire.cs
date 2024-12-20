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
                filePath = "MotsPossiblesFR.txt";
            }
            else if (langue != "FR")
            {
                filePath = "MotsPossiblesEN.txt";
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
        public void BubbleSort(List<string> mots)
        {
            int n = mots.Count;

            // Parcours de la liste
            for (int i = 0; i < n - 1; i++)
            {
                // Comparaison des éléments adjacents
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (string.Compare(mots[j], mots[j + 1], StringComparison.Ordinal) > 0) // StringComparison.Ordinal defini le critaire de comparaison
                    {        
                        // Échange si l'élément actuel est plus grand que le suivant
                        string temp = mots[j];
                        mots[j] = mots[j + 1];
                        mots[j + 1] = temp;
                    }
                }
            }
        }

        private static void TriInsertionStrings(List<string> mots)
        {
            int n = mots.Count;
        
            for (int i = 1; i < n; i++)
            {
                string cle = mots[i]; // Élément à insérer
                int j = i - 1;

                // Déplacer les éléments plus grands que `cle` d'une position vers la droite
                while (j >= 0 && string.Compare(mots[j], cle, StringComparison.Ordinal) > 0)
                {
                    mots[j + 1] = mots[j];
                    j--;
                }

                // Insérer l'élément à sa position correcte
                mots[j + 1] = cle;
            }
        }

        public static void MergeSort(List<string> mots, int left, int right)
        {
            if (left < right)
            {
                // Calcul de l'indice du milieu
                int middle = (left + right) / 2;

                // Diviser récursivement la moitié gauche
                MergeSort(mots, left, middle);

                // Diviser récursivement la moitié droite
                MergeSort(mots, middle + 1, right);

                // Fusionner les deux moitiés
                Merge(mots, left, middle, right);
            }
        }

        public static void Merge(List<string> mots, int left, int middle, int right)
        {
            // Tailles des deux sous-tableaux à fusionner
            int n1 = middle - left + 1;
            int n2 = right - middle;

            // Sous-listes temporaires
            List<string> leftArray = new List<string>();
            List<string> rightArray = new List<string>();

            // Remplir les sous-listes avec les éléments de la liste principale
            for (int i = 0; i < n1; i++)
                leftArray.Add(mots[left + i]);

            for (int j = 0; j < n2; j++)
                rightArray.Add(mots[middle + 1 + j]);

            // Indices pour parcourir les sous-listes
            int iLeft = 0, iRight = 0;
            int k = left;

            // Fusionner les éléments des sous-listes dans l'ordre croissant
            while (iLeft < n1 && iRight < n2)
            {
                if (string.Compare(leftArray[iLeft], rightArray[iRight]) <= 0)
                {
                    mots[k] = leftArray[iLeft];
                    iLeft++;
                }
                else
                {
                    mots[k] = rightArray[iRight];
                    iRight++;
                }
                k++;
            }

            // Ajouter les éléments restants de la sous-liste gauche
            while (iLeft < n1)
            {
                mots[k] = leftArray[iLeft];
                iLeft++;
                k++;
            }

            // Ajouter les éléments restants de la sous-liste droite
            while (iRight < n2)
            {
                mots[k] = rightArray[iRight];
                iRight++;
                k++;
            }
        }
    }
}
