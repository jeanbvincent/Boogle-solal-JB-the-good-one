using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boogle_Solal_JB
{
    internal class Plateau
    {
        De[,] des;
        Lettre[,] faces;
        List<Lettre> liste;
        public Plateau(List<Lettre> liste)
        {
            this.liste = liste;             // on initialise la liste de Lettre
            De[,] des = new De[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    De de = new De(liste);  // on crée le dé
                    des[i, j] = de;         // on ajoute le dé à notre tableau 
                    liste = de.Liste;       // il faut modifier les occurence des Lettres de la liste 

                }
            }
            this.des = des;
            Lettre[,] faces = new Lettre[4, 4];   // on crée le tableau des faces des dés qui vont etre montrés
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
        public int nbOccurences(Lettre lettre) // fonction renvoyant le nombre d'occurences d'une lettre dans le plateau (faces visibles)
        {
            int compteur = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (lettre.Caractere == des[i, j].Face.Caractere)
                    {
                        compteur++;
                    }
                }
            }
            return compteur;
        }
        public bool estVoisin(Lettre a, Lettre b)
        {
            if (b == null || nbOccurences(b) == 0)
            {
                Console.WriteLine(false); 
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (faces[i, j] != null && faces[i, j].Caractere == a.Caractere)
                    {
                        Console.WriteLine("a: " + i + " " + j);
                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                if (!(x == 0 && y == 0))
                                {
                                    int voisinX = i + x;
                                    int voisinY = j + y;
                                    if (voisinX >= 0 && voisinX < 4 && voisinY >= 0 && voisinY < 4)
                                    {
                                        if (faces[voisinX, voisinY] != null && faces[voisinX, voisinY].Caractere == b.Caractere)
                                        {
                                            Console.WriteLine(true);
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(false);
            return false;
        }
        public bool Test_Plateau(string mot) // lance la fonction récursive ChercheMot en partant de chaque occurence de la première lettre
        {
            bool retour = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ( mot!=null && mot!="" && faces[i, j].Caractere == mot[0])
                    {
                        int[] tab = { i, j };
                        if (ChercheMot2(mot, 1, faces[i, j], tab))
                        {
                            retour = true;
                        }
                    }
                }
            }
            return retour;
        }
        public bool ChercheMot2(string mot, int index, Lettre lettreActuelle, int[] coord)
        {
            if (index == mot.Length) return true;
            // Check all the 8 surrounding neighbors of the current cell
            for (int i = coord[0] - 1; i <= coord[0] + 1; i++)
            {
                for (int j = coord[1] - 1; j <= coord[1] + 1; j++)
                {
                    // Ensure the indices are within bounds and not the current cell
                    if (i >= 0 && i < 4 && j >= 0 && j < 4 && (i != coord[0] || j != coord[1]))
                    {
                        Lettre voisin = faces[i, j];
                        // Ensure the neighbor has the correct character and is not null
                        if (voisin != null && voisin.Caractere == mot[index])
                        {
                            // Store the current state before modifying it
                            Lettre temp = faces[coord[0], coord[1]];
                            faces[coord[0], coord[1]] = null;  // Mark the current cell as visited
                            int[] newCoord = { i, j };
                            // Recursively search for the next letter
                            if (ChercheMot2(mot, index + 1, voisin, newCoord))
                            {
                                // Restore the grid state after the recursion
                                faces[coord[0], coord[1]] = temp;
                                return true;
                            }
                            // Restore the grid state if the search fails
                            faces[coord[0], coord[1]] = temp;
                        }
                    }
                }
            }
            return false;
        }
        public bool ChercheMot(string mot, int index, Lettre lettreActuelle, int[] coord)
        {
            if (index == mot.Length) return true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("Lettre: " + lettreActuelle.Caractere);
                    Lettre voisin = faces[i, j];
                    Console.WriteLine("Lettre voisin:" + voisin.Caractere);
                    Console.WriteLine(i + " " + j);
                    if (voisin.Caractere == mot[index] && voisin != null)
                    {
                        Console.WriteLine("Lettre: " + lettreActuelle.Caractere);
                        Console.WriteLine("Lettre voisin:" + voisin.Caractere);
                        Console.WriteLine("coord Lettre" + coord[0] + coord[1]);
                        if (estVoisin(lettreActuelle, voisin))
                        {

                            faces[coord[0], coord[1]] = null;
                            int[] tab = { i, j };
                            if (ChercheMot(mot, index + 1, voisin, tab))
                            {
                                faces[i, j] = voisin;
                                return true;
                            }
                            faces[i, j] = voisin;
                        }
                    }
                }
            }
            return false;
        }
        public string toString() // affiche le plateau sous forme de tableau 4x4
        {
            string retour = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    retour = retour + faces[i, j].Caractere + " ";
                }
                retour = retour + "\n";
            }
            return retour;
        }
        public void Lance() // change les faces hautes des dés
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Random rdm = new Random();
                    des[i, j].Lance(rdm);
                    faces[i, j] = des[i, j].Face;
                }
            }
        }
    }
}
