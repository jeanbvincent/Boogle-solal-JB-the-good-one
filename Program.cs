using System;
using System.Collections.Generic;
using WordCloudSharp; /// wordCloudSharp est la bibliothèque utilisée pour faire le nuage de mots
using System.Drawing;

namespace Projet_Boogle_Solal_JB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Jeu jeu = new Jeu();
            jeu.Demarrer();
        }
    }
}
