using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpion
{
    class Joueur
    {
        public bool Tour { get; set; }
        public string Nom { get; } = "Ordinateur";
        public string Caractere { get; set; }

        public Joueur(string nom_donnee ="")
        {
            this.Nom = (nom_donnee =="") ? this.Nom : nom_donnee;
        }
    }
}
