namespace Projet_Boogle_Solal_JB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\jeanb\\Downloads\\Lettres.txt";
            List<Lettre> lettres = new List<Lettre>(); //creation d'un liste avec les lettre utilises

            //on utilisera pas de try{} catch{} puisque on connait deja le fichier "lettres"

            foreach (string ligne in File.ReadLines(filePath))   // Lire chaque ligne du fichier
            {
                // Séparer les valeurs par le point-virgule grace à .Split()
                string[] parties = ligne.Split(';');             // La methode Split() est utilisée pour diviser une chaîne en un tableau "parties"(de longeur 3) de sous-chaînes                                                
                if (parties.Length == 3)
                {
                    char caractere = char.Parse(parties[0]);     // On utilisera Parse au lieu de Convert.To puisque on veut une conversion stricte et spécifique au type
                    int points = int.Parse(parties[1]);          // Convert.To est plus permissif
                    int occurrences = int.Parse(parties[2]);

                    // Créer un nouveau objet Lettre et l'ajouter à la liste
                    Lettre lettre = new Lettre(caractere, points, occurrences);
                    lettres.Add(lettre);
                }
            }
            foreach (Lettre lettre in lettres)
            {
                Console.Write(lettre.Caractere + " ");
                Console.Write(lettre.Points + " ");
                Console.WriteLine(lettre.Occurence);

            }
            Console.WriteLine();

         
            string langue = "";
            langue = langue.ToUpper();
            while (langue != "fr" && langue != "en" && langue != "FR" && langue != "EN") //on demande a l'utilisateur de chosir de jouer en francais ou anglais
            {
                Console.WriteLine("Langue du jeu: \n tapez fr pour francais ou en pour anglais");
                langue = Console.ReadLine();
            }
            Dictionnaire dictionnaire = new Dictionnaire(langue);  //creation dictionnaire
                                                            
            Console.WriteLine("Nom du premier joueur :"); //Création des deux joueurs
            Joueur joueur1 = new Joueur(Console.ReadLine(),lettres);
            Console.WriteLine("Nom du deuxieme joueur");
            Joueur joueur2 = new Joueur(Console.ReadLine(),lettres);


            Console.WriteLine("Tour de "+joueur1.Nom);
            Plateau plateau = new Plateau(lettres);  //creation tableau
            Console.WriteLine(plateau.toString());
 
            DateTime debut = DateTime.Now;   //on initialise le temporisateur a l'heure actuelle 
            TimeSpan dureeLimite = TimeSpan.FromMinutes(20);   //on choisi notre limite de temps
            
            while (DateTime.Now - debut < dureeLimite)   //le joueur peu essayer des mots tant que la différence entre le temps du début et le temps actuel est plus petite que la durée du tour
            {
                Console.WriteLine("Tapez un mot");
                string essaie=  Console.ReadLine();
                essaie = essaie.ToUpper();

                if (plateau.Test_Plateau(essaie) == true)  //on verifie que le mot est dans le tableau 
                {
                    if (dictionnaire.RechDichoRecursif(essaie,0,essaie.Length) == true)  //on vérifie que le mot appartient au dictionnaire 
                    {
                        joueur1.Add_Mot(essaie);   //On ajoute le mot a la liste des mots trouve qui va compter egalement les points obtenue 

                    }
                    else 
                    {
                        Console.WriteLine("Le mot n'appartient pas au dictionnaire!");
                    }
                }
                else
                {
                    Console.WriteLine("Le mot n'appartient pas au tableau!");      
                }
                if (DateTime.Now - debut >= dureeLimite)
                {
                    Console.WriteLine("Temps écoulé!!");
                    break;
                }
            }

            plateau.Lance();
            Console.WriteLine("Tour de " + joueur2.Nom);
            Console.WriteLine(plateau.toString());



            //   for (int i = 0; i<4;i++)
            //   {
            //      for (int j = 0; j<4;j++)
            //      { 
            //       Console.Write("dé" +i+","+j+" : ");
            //       for (int h = 0; h < 6; h++)
            //       {
            //           Console.Write(plateau.Des[i, j].Lettres[h].Caractere);
            //       }
            //       Console.WriteLine() ;
            //      
            //      } 
            //    }

            // foreach (Lettre lettre in lettres)
            // {
            //     Console.Write(lettre.Caractere + " ");
            //     Console.Write(lettre.Points + " ");
            //     Console.WriteLine(lettre.Occurence);
            //
            // }
            // Console.WriteLine();






            // Console.WriteLine("mot");
            // string mot = Console.ReadLine();
            // Console.WriteLine(plateau.Test_Plateau(mot));

            //  Console.WriteLine(dictionnaire.Mots[0]);
            //
            //  foreach (string popo in dictionnaire.Mots)
            //  { Console.WriteLine(popo); }
            //
            //  Console.WriteLine(dictionnaire.Recherche("PERVERTI"));




            //
            //            Console.WriteLine("faces du de");
            //            De de1 = new De();
            //           for (int i = 0; i < 6; i++)
            //           {
            //                Console.Write(i+" " );
            //                Console.WriteLine(de1.Lettres[i]);
            //           }
            //           
            //           Console.WriteLine("premier lance");
            //           Random randome = new Random();
            //           de1.lance(randome);
            //
            //           Console.WriteLine(de1.Face);
            //            Console.WriteLine("deuxieme  lance");
            //            de1.lance(randome);
            //           Console.WriteLine(de1.Face);
            //         
        }
    }
}