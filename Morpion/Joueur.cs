using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    class Joueur
    {
        public bool tour;
        public string nom = "Ordinateur";
        public string caractere;
        public Joueur(string nom_donnee ="")
        {
            this.nom = (nom_donnee =="") ? this.nom : nom_donnee;
        }
    }
}
