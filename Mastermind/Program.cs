using System;
using System.Text.RegularExpressions;

namespace TP2
{
    class Program
    {
        /**
         * Affichage d'un entier à une position précise
         */
        private static void afficheValeur(int x, int y, int val)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(val);
        }

        /**
         * Controle si le caractère fait partie des couleurs possibles
         */
        private static bool caracValid(char caract)
        {
            string pattern = @"([BRNVJOG])";
            return Regex.IsMatch(caract.ToString(),pattern);
        }

        /**
         * Saisie un caractère à une position précise et avec controle
         */
        private static char saisieCouleur(int x, int y)
        {
            char c;
            do
            {
                Console.SetCursorPosition(x, y);
                c = Console.ReadKey().KeyChar;
            } while (!caracValid(c));
            return c;
        }

        /**
         * Saisie d'une combinaison de 5 couleurs et retour du tableau
         */
        private static char[] saisieCombinaison(int ligne)
        {
            char[] tCombinaison = new char[5];
            for (int i = 0; i < 5; i++)
            {
                tCombinaison[i] = saisieCouleur(20 + i * 4, ligne);
            }
            return tCombinaison;
        }

        /**
         * Compte le nombre de bien placés et remplace les cases concernées
         */
        private static int calculBpMp(char[] vec1, char[] vec2)
        {
            int cumul = 0;
            for (int i = 0; i < 5; i++)
            {
                // 2 couleurs identiques à la même position
                if (vec1[i] == vec2[i])
                {
                    cumul++;
                    // changement de valeurs pour ne pas les recompter
                    vec1[i] = 'X';
                    vec2[i] = 'Y';
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // 2 couleurs identiques à des positions différentes
                    if (vec1[i] == vec2[j])
                    {
                        cumul++;
                        // changement des valeurs pour ne pas les recompter
                        vec1[i] = 'X';
                        vec2[j] = 'Y';
                    }
                }
            }
            return cumul;
        }

        /**
         * Compte le nombre de mal placés et remplace les cases concernées
         */
   

        /**
         * Retour du message à afficher en fonction du nombre d'essais
         */
        private static string Bilan(int nbEssai)
        {
            if (nbEssai <= 5)
            {
                return "Bravo";
            }
            else
            {
                if (nbEssai <= 10)
                {
                    return "Correct";
                }
                else
                {
                    return "Décevant";
                }
            }
        }

        static void Main(string[] args)
        {
            // déclarations
            char[] tFormule = new char[5];
            char[] tCopieFormule = new char[5];
            char[] tEssai = new char[5];
            int nbEssai;    // compteur d'essais
            int bp, mp;     // compteurs de bien et mal placés

            // saisie de la formule à chercher
            Console.Write("1er joueur : ");
            Console.WriteLine();
            Console.Write("COuleurs disponible : [Bleu(B), Rouge(R), Noir(N), Vert(V), Jaune(J), Orange(O), Gris(G)");
            tFormule = saisieCombinaison(0);
            

            // Début second joueur
            Console.Clear();
            Console.Write("2eme joueur :                               bien placé     mal placé");

            // boucle des essais
            nbEssai = 0;
            do
            {
                nbEssai++;
                // sauvegarde de la formule d'origine
                Array.Copy(tFormule, tCopieFormule, 5);
                // saisie de l'essai
                Console.SetCursorPosition(5, nbEssai);
                Console.Write("essai " + nbEssai);
                tEssai = saisieCombinaison(nbEssai);
                // calcul et affichage des bien et mal placés
                bp = calculBpMp(tCopieFormule, tEssai);
                afficheValeur(45, nbEssai, bp);
                mp = calculBpMp(tCopieFormule, tEssai);
                afficheValeur(60, nbEssai, mp);
            } while (bp < 5);

            // affichage final
            Console.WriteLine();
            Console.Write("Vous avez trouvé en " + nbEssai + " essais : " + Bilan(nbEssai));

            Console.ReadLine();
        }
    }
}