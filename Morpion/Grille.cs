using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Morpion
{
    public class Grille
    {
        private Label[] tab = new Label[9];
        public static int IndexCenter { get; } = 4;

        public Grille(ref Label label1,ref Label label2,ref Label label3, ref Label label4,
        ref Label label5, ref Label label6,ref Label label7,ref Label label8,ref Label label9)
        {
            tab[0] = label1;
            tab[1] = label2;
            tab[2] = label3;
            tab[3] = label4;
            tab[4] = label5;
            tab[5] = label6;
            tab[6] = label7;
            tab[7] = label8;
            tab[8] = label9;
        }

        public void remplirCase(string X_O, int indice)
        {
            if(X_O.Length == 1 && tab[indice].Text == "")
                tab[indice].Text = X_O;
        }

        public void newGrille()
        {
            foreach (Label label in this.tab)
            {
                label.Text = "";
            }

        }

        public bool checkGagnant(string lettre_joueur)
        {
            string O = lettre_joueur + lettre_joueur + lettre_joueur;
            if (tab[0].Text + tab[1].Text + tab[2].Text == O || tab[0].Text + tab[4].Text + tab[8].Text == O ||
                tab[0].Text + tab[3].Text + tab[6].Text == O || tab[1].Text + tab[4].Text + tab[7].Text == O ||
                tab[2].Text + tab[5].Text + tab[8].Text == O || tab[2].Text + tab[4].Text + tab[6].Text == O ||
                tab[6].Text + tab[7].Text + tab[8].Text == O)
            {
                return true;
            }

            return false;
        }

        public bool checkFullGrid()
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

        public bool checkGridCenter()
        {

            if (tab[IndexCenter].Text == "")
            {
                return true;
            }

            return false;
        }

        public List<int> getAllEmptyCase()
        {
            List<int> vide = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (tab[i].Text == "")
                {
                    vide.Add(i);
                }
            }

            return vide;
        }

        public string getCaseValue(int indice)
        {
            return tab[indice].Text;
        }

        public int getLabelIndice(Label label)
        {
            return Int32.Parse(label.Name.Replace("label", ""))-1;
        }
    }
}