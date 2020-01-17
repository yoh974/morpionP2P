using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace Morpion
{
    public partial class Form1 : Form
    {
        private const string SYMBOL_1 = "O";
        private const string SYMBOL_2 = "X";
        int nb_joueur;
        Joueur joueur_1;
        Joueur joueur_2;
        Label[] tab = new Label[9];
        private Grille _grille1;
        private Morpion _morpion;

        public Form1()
        {
            InitializeComponent();
            string nom_joueur = "";
            DialogResult dialogResult =
                MessageBox.Show("Jouer contre un humain?", "Humain ou ordinateur", MessageBoxButtons.YesNo);
            if (dialogResult.ToString() == "Yes")
            {
                nb_joueur = 2;
            }
            else
            {
                nb_joueur = 1;
            }

            nom_joueur = Interaction.InputBox("Entrez le nom du joueur", "Entrez le nom du joueur", "joueur_1");
            const string nomDonnee = "Ordinateur";
            _grille1 = new Grille(ref label1,ref label2,ref label3,ref label4,ref label5,ref label6,ref label7,ref label8,ref label9);
            _morpion = new Morpion(nom_joueur_1,nom_joueur_2,_grille1);
            _morpion.init(nom_joueur,nomDonnee);
        }

        private void label_Click(object sender, EventArgs e)
        {
            //attempt to cast the sender as a label
            Label lbl = sender as Label;
            _morpion.remplirGrille(_grille1.getLabelIndice(lbl));
        }
    }
}