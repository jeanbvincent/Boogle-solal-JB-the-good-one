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
            string filePath = ""; /// contient le chemin jusqu'au document texte qu'on choisira
            if (langue == "FR")
            {
                filePath = "MotsPossiblesFR.txt";
            }
            else if (langue != "FR")
            {
                filePath = "MotsPossiblesEN.txt";
            }
            string contenu = File.ReadAllText(filePath); /// on lit le texte du document
            char[] delimiteurs = { ' ', '\n', '\r', '\t' }; /// caractères séparant les mots (sauts de ligne, tabulations...)
            mots = new List<string>(contenu.Split(delimiteurs, StringSplitOptions.RemoveEmptyEntries)); /// on sépare les mots (str -> liste de mots)
            TriFusion(mots, 0, mots.Count-1); /// on trie la liste de mots par ordre alphabétique (comme ça c'est fait)
        }
        public List<string> Mots
        {
            get { return mots; }

        }
        public string toString() /// décrit le dictionnaire (langue et mots par longueur/lettre)
        {
            int longueurMax = 0; /// on cherche la taille maximale d'un mot dans le dictionnaire
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
                string mot = mots[i]; /// on prend le mot actuel en paramètre
                motsParLongueur[mot.Length]++; /// on incrémente à la bonne longueur
                char premiereLettre = char.ToUpper(mot[0]);
                if (premiereLettre >= 'A' && premiereLettre <= 'Z')
                {
                    motsParLettre[premiereLettre - 'A']++; /// on regarde la distance de la lettre à A correspondante
                }
            }
            string resultat = $"Langue : {langue}, Nombre total de mots : {mots.Count}";
            resultat += ", Nombre de mots par longueur : ";
            for (int i = 1; i <= longueurMax; i++) /// on ignore la longueur 0
            {
                if (motsParLongueur[i] > 0)
                {
                    resultat += $"Longueur {i} : {motsParLongueur[i]} mots, "; /// on écrit pour chaque longueur
                }
            }
            resultat += "Nombre de mots par première lettre : ";
            for (int i = 0; i < 26; i++)
            {
                if (motsParLettre[i] > 0)
                {
                    char lettre = (char)('A' + i);
                    resultat += $"Lettre '{lettre}' : {motsParLettre[i]} mots, "; /// on écrit pour chaque première lettre
                }
            }
            return resultat;
        }
        public bool Recherche(string mot) /// cherche si un mot en paramètre est bien présent dans le dictionnaire (complexité linéaire)
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
        public bool RechDichoRecursif(string mot, int debut, int fin) /// recherche récursive de mot /!\ le dictionnaire doit être trié au préalabe !!!
        {
            if (debut > fin)
            {
                return false;
            }
            int milieu = (debut + fin) / 2; /// calcul de l'indice du milieu
            if (mots[milieu] == mot) /// on compare le mot cherché au mot à l'indice du milieu
            {
                return true;
            }
            else if (mot.CompareTo(mots[milieu]) < 0) /// mot plus petit que le milieu, on recherche à gauche
            {
                return RechDichoRecursif(mot, debut, milieu - 1);
            }
            else /// sinon on recherche à droite
            {
                return RechDichoRecursif(mot, milieu + 1, fin);
            }
        }
        public void TriBulles(List<string> mots) /// trie en remontant à chaque fois la plus grande valeur vers le haut (comme des bulles d'eau d'où le nom)
        {
            int n = mots.Count;
            /// parcours de la liste
            for (int i = 0; i < n - 1; i++)
            {
                /// comparaison des éléments adjacents
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (string.Compare(mots[j], mots[j + 1], StringComparison.Ordinal) > 0) /// StringComparison.Ordinal définit le critaire de comparaison
                    {
                        /// échange si l'élément actuel est plus grand que le suivant
                        string temp = mots[j];
                        mots[j] = mots[j + 1];
                        mots[j + 1] = temp;
                    }
                }
            }
        }

        private static void TriInsertion(List<string> mots)
        {
            int n = mots.Count;
            for (int i = 1; i < n; i++)
            {
                string cle = mots[i]; /// élément à insérer
                int j = i - 1;
                /// déplacer les éléments plus grands que `cle` d'une position vers la droite
                while (j >= 0 && string.Compare(mots[j], cle, StringComparison.Ordinal) > 0)
                {
                    mots[j + 1] = mots[j];
                    j--;
                }
                /// insérer l'élément à sa position correcte
                mots[j + 1] = cle;
            }
        }

        public static void TriFusion(List<string> mots, int gauche, int droite)
        {
            if (gauche < droite)
            {
                /// calcul de l'indice du milieu
                int milieu = (gauche + droite) / 2;
                /// trier récursivement la moitié gauche
                TriFusion(mots, gauche, milieu);
                /// trier récursivement la moitié droite
                TriFusion(mots, milieu + 1, droite);
                /// fusionner les deux moitiés
                Fusionner(mots, gauche, milieu, droite);
            }
        }
        public static void Fusionner(List<string> mots, int gauche, int milieu, int droite)
        {
            /// tailles des deux sous-tableaux à fusionner
            int n1 = milieu - gauche + 1;
            int n2 = droite - milieu;
            /// sous-listes temporaires
            List<string> listeGauche = new List<string>();
            List<string> listeDroite = new List<string>();
            /// remplir les sous-listes avec les éléments de la liste principale
            for (int i = 0; i < n1; i++)
                listeGauche.Add(mots[gauche + i]);
            for (int j = 0; j < n2; j++)
                listeDroite.Add(mots[milieu + 1 + j]);
            /// indices pour parcourir les sous-listes
            int iGauche = 0, iDroite = 0;
            int k = gauche;
            /// fusionner les éléments des sous-listes dans l'ordre croissant
            while (iGauche < n1 && iDroite < n2)
            {
                if (string.Compare(listeGauche[iGauche], listeDroite[iDroite]) <= 0)
                {
                    mots[k] = listeGauche[iGauche];
                    iGauche++;
                }
                else
                {
                    mots[k] = listeDroite[iDroite];
                    iDroite++;
                }
                k++;
            }
            /// ajouter les éléments restants de la sous-liste gauche
            while (iGauche < n1)
            {
                mots[k] = listeGauche[iGauche];
                iGauche++;
                k++;
            }
            /// ajouter les éléments restants de la sous-liste droite
            while (iDroite < n2)
            {
                mots[k] = listeDroite[iDroite];
                iDroite++;
                k++;
            }
        }
    }
}
