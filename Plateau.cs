using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle_Solal_JB
{
    internal class Plateau /// initialisation de la classe plateau
    {
        De[,] des;
        Lettre[,] faces;
        List<Lettre> liste;
        public Plateau(List<Lettre> liste)
        {
            this.liste = liste;
            De[,] des = new De[4, 4]; /// le plateau est un tableau de 4x4 dés
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    De de = new De(liste);
                    des[i, j] = de;
                    liste = de.Liste;
                }
            }
            this.des = des;
            Lettre[,] faces = new Lettre[4, 4]; /// le plateau visible est décrit par l'ensemble des faces visibles des dés
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    faces[i, j] = des[i, j].Face;
                }
            }
            this.faces = faces;
        }
        public De[,] Des
        {
            get { return des; }
            set { des = value; }
        }
        public Lettre[,] Faces
        {
            get { return faces; }
            set { faces = value; }
        }
        public List<Lettre> Liste
        {
            get { return Liste; }
            set { Liste = value; }
        }
        public int nbOccurences(Lettre lettre) /// compte le nombre d'occurences d'une lettre en paramètre dans les faces visibles du plateau
        {
            int compteur = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (lettre.Caractere == des[i, j].Face.Caractere)
                    {
                        compteur++; /// on parcourt simplement le plateau en incrémentant un compteur à chaque fois qu'on rencontre la lettre cherchée
                    }
                }
            }
            return compteur;
        }
        public bool Test_Plateau(string mot) /// Permet de lancer ChercheMot en comparant chaque lettre à la première lettre du mot cherché (pour avoir l'index)
        {
            bool retour = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ( mot!=null && mot!="" && faces[i, j].Caractere == mot[0])
                    {
                        int[] tab = { i, j };
                        if (ChercheMot(mot, 1, faces[i, j], tab)) /// on peut lancer cherche mot en utilisant les coordonnées trouvées de la 1ere lettre
                        {
                            retour = true;
                        }
                    }
                }
            }
            return retour;
        }
        public bool ChercheMot(string mot, int index, Lettre lettreActuelle, int[] coord) /// cherche si un mot est dans le tableau de manière récursive (true ou false)
        {
            if (index == mot.Length) return true; /// cas de base : mot cherché = longueur de ce qui a été trouvé
            for (int i = coord[0] - 1; i <= coord[0] + 1; i++)
            {
                for (int j = coord[1] - 1; j <= coord[1] + 1; j++)
                {
                    if (i >= 0 && i < 4 && j >= 0 && j < 4 && (i != coord[0] || j != coord[1])) /// on va regarder tous les voisins de notre lettre en paramètre
                    {
                        Lettre voisin = faces[i, j];

                        if (voisin != null && voisin.Caractere == mot[index]) /// si un des voisins correspond à la lettre suivante qu'on cherche...
                        {
                            Lettre temp = faces[coord[0], coord[1]];
                            faces[coord[0], coord[1]] = null; /// ...on met la face en null pour ne pas risquer deux fois de repasser par la même lettre...
                            int[] newCoord = { i, j };
                            if (ChercheMot(mot, index + 1, voisin, newCoord)) ///  ... si le mot est effectivement dans le plateau...
                            {
                                faces[coord[0], coord[1]] = temp; /// ... on peut restituer les lettres null dans leurs cases respectives
                                return true; /// ... et on renvoie true
                            }
                            faces[coord[0], coord[1]] = temp; /// sinon en restitue quand même les lettres null
                        }
                    }
                }
            }
            return false;
        }
        public string toString() /// affiche le plateau
        {
            string retour = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    retour = retour + faces[i, j].Caractere + " "; /// pour chaque dé du plateau, on affiche la lettre visible
                }
                retour = retour + "\n";
            }
            return retour;
        }
        public void Lance() /// relance les dés pour mélanger le plateau
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Random rdm = new Random();
                    des[i, j].Lance(rdm);
                    faces[i, j] = des[i, j].Face; /// chaque dé reste à sa place mais affiche une lettre différente
                }
            }
        }
    }
}
