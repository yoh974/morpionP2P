using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace Morpion
{
    public partial class Form1 : Form
    {
        int nb_joueur;
        Joueur joueur_1;
        Joueur joueur_2;
        Label[] tab = new Label[9];

        public Form1()
        {
            //InitializeComponent();
            string nom_joueur = "";
            Random rand = new Random();
            DialogResult dialogResult =
                MessageBox.Show("Jouer contre un humain?", "humain ou ordinateur", MessageBoxButtons.YesNo);
            if (dialogResult.ToString() == "Yes")
            {
                nb_joueur = 2;
            }
            else
            {
                nb_joueur = 1;
            }

            nom_joueur = Interaction.InputBox("Entrez le nom du joueur", "Entrez le nom du joueur", "joueur_1");
            joueur_1 = new Joueur(nom_joueur);
            joueur_2 = new Joueur("Ordinateur");
            InitializeComponent();
            joueur_1.caractere = "O";
            joueur_2.caractere = "X";
            joueur_1.tour = (rand.Next(0, 2) == 1);
            joueur_2.tour = !joueur_1.tour;
            label_joueur.Text = (joueur_1.tour) ? joueur_1.nom : joueur_2.nom;
            tab[0] = label1;
            tab[1] = label2;
            tab[2] = label3;
            tab[3] = label4;
            tab[4] = label5;
            tab[5] = label6;
            tab[6] = label7;
            tab[7] = label8;
            tab[8] = label9;
            if (joueur_2.tour)
            {
                Play();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            remplir(ref label1);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            remplir(ref label5);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            remplir(ref label2);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            remplir(ref label3);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            remplir(ref label4);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            remplir(ref label6);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            remplir(ref label7);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            remplir(ref label8);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            remplir(ref label9);
        }

        private void remplir(ref Label label)
        {
            string gagnant = "";
            string O_X = (joueur_1.tour) ? joueur_1.caractere : joueur_2.caractere;
            label.Text = (label.Text == "") ? O_X : label.Text;
            joueur_1.tour = !joueur_1.tour;
            joueur_2.tour = !joueur_2.tour;
            if (Partie_non_fini())
            {
                Play();
            }
            else
            {
                gagnant = (isGameWin() == "O") ? "gagné" : "perdu";
                DialogResult dialogResult = MessageBox.Show("Partie terminée vous avez " + gagnant, "Partie terminé",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //TODO: new game
                    createNewGame();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                    Application.Exit();
                }
            }
        }

        private void createNewGame()
        {
            Random rand = new Random();
            foreach (Label label in this.tab)
            {
                label.Text = "";
            }
            joueur_1.tour = (rand.Next(0, 2) == 1);
            joueur_2.tour = !joueur_1.tour;
            label_joueur.Text = (joueur_1.tour) ? joueur_1.nom : joueur_2.nom;
            if (joueur_2.tour)
            {
                Play();
            }
        }

        private bool Partie_non_fini()
        {
            return gameStateCheck() == "c";
        }

        private void Play()
        {
            if (joueur_2.tour && joueur_2.nom == "Ordinateur")
            {
                Jouer_IA();
            }
        }

        private void Jouer_IA()
        {
            //L'IA essaye de gagner puis essaye de défendre puis essaye de jouer la case du milieu sinon joue une case vide au hasard
            if (!iaPlayToWin())
            {
                if (!iaPlayToDefend())
                {
                    if (!isCenterFree())
                    {
                        iaRandomPlay();
                    }
                }
            }
        }

        //test si un des deux joueur a gagné retourne c pour continuer ou le caractère gagnant
        private string isGameWin()
        {
            string O = "OOO";
            string X = "XXX";
            if (tab[0].Text + tab[1].Text + tab[2].Text == O || tab[0].Text + tab[4].Text + tab[8].Text == O ||
                tab[0].Text + tab[3].Text + tab[6].Text == O || tab[1].Text + tab[4].Text + tab[7].Text == O ||
                tab[2].Text + tab[5].Text + tab[8].Text == O || tab[2].Text + tab[4].Text + tab[6].Text == O ||
                tab[6].Text + tab[7].Text + tab[8].Text == O )
            {
                return "O";
            }

            if (tab[0].Text + tab[1].Text + tab[2].Text == X || tab[0].Text + tab[4].Text + tab[8].Text == X ||
                tab[0].Text + tab[3].Text + tab[6].Text == X || tab[1].Text + tab[4].Text + tab[7].Text == X ||
                tab[2].Text + tab[5].Text + tab[8].Text == X || tab[2].Text + tab[4].Text + tab[6].Text == X||
                tab[6].Text + tab[7].Text + tab[8].Text == X )
            {
                return "X";
            }

            return "c";
        }

        //vérifie s'il y a des case disponible sinon c'est game over
        bool isGameOver()
        {
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i].Text == "")
                {
                    return false;
                }
            }

            return true;
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
            else if (isGameOver())
            {
                return "d";
            }
            //sinon la partie continue
            else
            {
                return "c";
            }
        }

        //cette fonction vérifie que la case centrale est disponible e tla remplace par un O le cas echéant
        bool isCenterFree()
        {
            if (tab[4].Text == "")
            {
                remplir(ref tab[4]);
                return true;
            }

            return false;
        }

        //cette fonction renvoi un nombre au hasard entre 0 et a
        int random(int a)
        {
            Random random = new Random();
            return random.Next(0, a + 1);
        }

        //cette fonction cherche les cases disponibles et en choisit une à l'aide de la fonction random la case retenue est marquée avec un O
        void iaRandomPlay()
        {
            //création d'un tableau dynamique contenant les indices du tableau morpion qui seront vides
            List<int> vide = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (tab[i].Text == "")
                {
                    vide.Add(i);
                }
            }

            //on choisi au hasard un indice dans le tableau dynamique vide de 0 à la taille du tableau et le tableau morpion prend alors O
            remplir(ref tab[vide[random(vide.Count - 1)]]);
        }

        //cette fonction recherche si 2 'O' sont alignées horizontalement, verticalement, et en diagonale et met un 'O' pour gagner
        bool iaPlayToWin()
        {
            //une chaine pour tester la présence de O
            string car_ord = joueur_2.caractere;

            string tabLigne = "FFF";
            for (int i = 0; i < 9; i += 3)
            {
                if (i == 0)
                {
                    //test des colonnes
                    for (int j = 0; j < 3; j++)
                    {
                        //concaténation des colonnes
                        tabLigne = tab[j].Text + tab[j + 3].Text + tab[j + 6].Text;

                        //test des différentes position des O et affectation du O à l'unique case vide
                        if (tabLigne == car_ord + car_ord )
                        {
                            if (tab[j].Text == "")
                            {
                                remplir(ref tab[j]);
                                return true;
                            }
                            else if (tab[j + 3].Text == "")
                            {
                                remplir(ref tab[j + 3]);
                                return true;
                            }
                            else if (tab[j + 6].Text == "")
                            {
                                remplir(ref tab[j + 6]);
                                return true;
                            }
                        }
                    }

                    //diagonale à partir de la position 0
                    tabLigne = tab[0].Text + tab[4].Text + tab[8].Text;
                    if (tabLigne == car_ord + car_ord )
                    {
                        if (tab[0].Text == "")
                        {
                            remplir(ref tab[0]);
                            return true;
                        }
                        else if (tab[4].Text == "")
                        {
                            remplir(ref tab[4]);
                            return true;
                        }
                        else if (tab[8].Text == "")
                        {
                            remplir(ref tab[8]);
                            return true;
                        }
                    }

                    //diagonale à partir de la position 2
                    tabLigne = tab[2].Text + tab[4].Text + tab[6].Text;

                    if (tabLigne == car_ord + car_ord )
                    {
                        if (tab[2].Text == "")
                        {
                            remplir(ref tab[2]);
                            return true;
                        }
                        else if (tab[4].Text == "")
                        {
                            remplir(ref tab[4]);
                            return true;
                        }
                        else if (tab[6].Text == "")
                        {
                            remplir(ref tab[6]);
                            return true;
                        }
                    }
                }

                //test des lignes
                tabLigne = tab[i].Text + tab[i + 1].Text + tab[i + 2].Text;

                if (tabLigne == car_ord + car_ord )
                {
                    if (tab[i].Text == "")
                    {
                        remplir(ref tab[i]);
                        return true;
                    }
                    else if (tab[i + 1].Text == "")
                    {
                        remplir(ref tab[i + 1]);
                        return true;
                    }
                    else if (tab[i + 2].Text == "")
                    {
                        remplir(ref tab[i + 2]);
                        return true;
                    }
                }
            }

            return false;
        }

        //cette fonction recherche si 2 'X' sont alignées et empeche l'utilisateur de gagner
        bool iaPlayToDefend()
        {
            //test
            string car_ord = "X";
            string tabLigne = "FFF";
            for (int i = 0; i < 9; i += 3)
            {
                if (i == 0)
                {
                    //test des colonnes
                    for (int j = 0; j < 3; j++)
                    {
                        //concaténation des colonnes
                        tabLigne = tab[j].Text + tab[j + 3].Text + tab[j + 6].Text;

                        //test des différentes position des O et affectation du O à l'unique case vide
                        if (tabLigne == car_ord + car_ord )
                        {
                            if (tab[j].Text == "")
                            {
                                remplir(ref tab[j]);
                                return true;
                            }
                            else if (tab[j + 3].Text == "")
                            {
                                remplir(ref tab[j + 3]);
                                return true;
                            }
                            else if (tab[j + 6].Text == "")
                            {
                                remplir(ref tab[j + 6]);
                                return true;
                            }
                        }
                    }

                    //diagonale à partir de la position 0
                    tabLigne = tab[0].Text + tab[4].Text + tab[8].Text;
                    if (tabLigne == car_ord + car_ord )
                    {
                        if (tab[0].Text == "")
                        {
                            remplir(ref tab[0]);
                            return true;
                        }
                        else if (tab[4].Text == "")
                        {
                            remplir(ref tab[4]);
                            return true;
                        }
                        else if (tab[8].Text == "")
                        {
                            remplir(ref tab[8]);
                            return true;
                        }
                    }

                    //diagonale à partir de la position 2
                    tabLigne = tab[2].Text + tab[4].Text + tab[6].Text;

                    if (tabLigne == car_ord + car_ord )
                    {
                        if (tab[2].Text == "")
                        {
                            remplir(ref tab[2]);
                            return true;
                        }
                        else if (tab[4].Text == "")
                        {
                            remplir(ref tab[4]);
                            return true;
                        }
                        else if (tab[6].Text == "")
                        {
                            remplir(ref tab[6]);
                            return true;
                        }
                    }
                }

                //test des lignes
                tabLigne = tab[i].Text + tab[i + 1].Text + tab[i + 2].Text;

                if (tabLigne == car_ord + car_ord )
                {
                    if (tab[i].Text == "")
                    {
                        remplir(ref tab[i]);
                        return true;
                    }
                    else if (tab[i + 1].Text == "")
                    {
                        remplir(ref tab[i + 1]);
                        return true;
                    }
                    else if (tab[i + 2].Text == "")
                    {
                        remplir(ref tab[i + 2]);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}