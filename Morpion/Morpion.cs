using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Morpion
{
    public class Morpion
    {
        private const string SYMBOL_1 = "O";
        private const string SYMBOL_2 = "X";
        int nb_joueur;
        Joueur joueur_1;
        Joueur joueur_2;
        private Grille _grille;

        private IA _ia;
        public Label nom_joueur_2 { get; set; }

        public Label nom_joueur_1 { get; set; }

        public Morpion(Label nomJoueur1, Label nomJoueur2, Grille grille)
        {
            nom_joueur_1 = nomJoueur1;
            nom_joueur_2 = nomJoueur2;
            _grille = grille;
        }


        public void init(string nom_joueur1, string nomJoueur2)
        {
            string nom_joueur = nom_joueur1;
            Random rand = new Random();
            joueur_1 = new Joueur(nom_joueur);
            joueur_2 = new Joueur(nomJoueur2);

            joueur_1.Caractere = SYMBOL_1;
            joueur_2.Caractere = SYMBOL_2;
            _ia = new IA(_grille, SYMBOL_1, SYMBOL_2);
            joueur_1.Tour = (rand.Next(0, 2) == 1);
            joueur_2.Tour = !joueur_1.Tour;
            nom_joueur_1.Text = joueur_1.Nom;
            nom_joueur_2.Text = joueur_2.Nom;

            if (joueur_2.Tour)
            {
                nom_joueur_2.BorderStyle = BorderStyle.FixedSingle;
                Play();
            }
        }

        private void Play()
        {
            if (joueur_2.Tour && joueur_2.Nom == "Ordinateur")
            {
                remplirGrille(_ia.Jouer_IA());
            }
            else
            {
                //attente tour joueur 2
            }
        }

        public void remplirGrille(int indice)
        {
            string gagnant = "";
            string O_X = (joueur_1.Tour) ? joueur_1.Caractere : joueur_2.Caractere;
            _grille.remplirCase(O_X, indice);
            joueur_1.Tour = !joueur_1.Tour;
            joueur_2.Tour = !joueur_2.Tour;
            nom_joueur_2.BorderStyle = (joueur_2.Tour) ? BorderStyle.FixedSingle : BorderStyle.None;
            nom_joueur_1.BorderStyle = (joueur_1.Tour) ? BorderStyle.FixedSingle : BorderStyle.None;
            if (Partie_non_fini())
            {
                Play();
            }
            else
            {
                switch (gameStateCheck())
                {
                    case SYMBOL_1:
                        gagnant = "vous avez gagné";
                        break;
                    case SYMBOL_2:
                        gagnant = "vous avez perdu";
                        break;
                    default:
                        gagnant = "match nul";
                        break;
                }

                string stateCheck = gameStateCheck();
                switch (stateCheck)
                {
                    case SYMBOL_1:
                        gagnant = "gagné";
                        break;
                    case SYMBOL_2:
                        gagnant = "perdu";
                        break;
                    case "d":
                        gagnant = "match nul";
                        break;
                    default:
                        gagnant = "statut de la partie indéterminé";
                        break;
                }
                
                DialogResult dialogResult = MessageBox.Show(
                    "Partie terminée : " + gagnant + " \n\nVoulez-vous rejouez?", "Partie terminé",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    createNewGame();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        public void createNewGame()
        {
            Random rand = new Random();
            _grille.newGrille();
            joueur_1.Tour = (rand.Next(0, 2) == 1);
            joueur_2.Tour = !joueur_1.Tour;
            if (joueur_2.Tour)
            {
                Play();
            }
        }

        private bool Partie_non_fini()
        {
            return gameStateCheck() == "c";
        }

        //test si un des deux joueur a gagné retourne c pour continuer ou le caractère gagnant
        private string isGameWin()
        {
            if (_grille.checkGagnant(SYMBOL_1))
                return SYMBOL_1;
            if (_grille.checkGagnant(SYMBOL_2))
                return SYMBOL_2;

            return "c";
        }

        //vérification générale utilisant les deux précéentes fonction
        string gameStateCheck()
        {
            //si il y a un gagnant on retourne sa valeur
            if (isGameWin() != "c")
            {
                return isGameWin();
            }
            //s'il n'y a plus de case vide Match nul
            else if (_grille.checkFullGrid())
            {
                return "d";
            }
            //sinon la partie continue
            else
            {
                return "c";
            }
        }
    }
}