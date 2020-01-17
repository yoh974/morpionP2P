using System;
using System.Collections.Generic;

namespace Morpion
{
    public class IA
    {
        private Grille _grille;
        private string _signe_ordinateur;
        private string _signe_joueur;

        public IA(Grille grille,string signeJoueur,string signe_ordinateur)
        {
            _signe_ordinateur = signe_ordinateur;
            _grille = grille;
            _signe_joueur = signeJoueur;
        }

        public int Jouer_IA()
        {
            //L'IA essaye de gagner puis essaye de défendre puis essaye de jouer la case du milieu sinon joue une case vide au hasard
            int playToWin = iaPlayToWin();
            if (playToWin == -1)
            {
                int playToDefend = iaPlayToDefend();
                if (playToDefend == -1)
                {
                    if (!isCenterFree())
                    {
                        return iaRandomPlay();
                    }
                    else
                    {
                        return Grille.IndexCenter;
                    }
                }
                else
                {
                    return playToDefend;
                }
            }
            else
            {
                return playToWin;
            }
        }

        //cette fonction vérifie que la case centrale est disponible e tla remplace par un O le cas echéant
        bool isCenterFree()
        {
            return _grille.checkGridCenter();
        }

        //cette fonction renvoi un nombre au hasard entre 0 et a
        int random(int a)
        {
            Random random = new Random();
            return random.Next(0, a + 1);
        }

        //cette fonction cherche les cases disponibles et en choisit une à l'aide de la fonction random la case retenue est marquée avec un O
        public int iaRandomPlay()
        {
            //création d'un tableau dynamique contenant les indices du tableau morpion qui seront vides
            List<int> vide = new List<int>();
            vide = _grille.getAllEmptyCase();

            //on choisi au hasard un indice dans le tableau dynamique vide de 0 à la taille du tableau et le tableau morpion prend alors O
            return vide[random(vide.Count - 1)];
        }

        //cette fonction recherche si 2 'O' sont alignées horizontalement, verticalement, et en diagonale et met un 'O' pour gagner
        int iaPlayToWin()
        {
            //une chaine pour tester la présence de O
            string car_ord = _signe_ordinateur;

            string tabLigne = "";
            for (int i = 0; i < 9; i += 3)
            {
                if (i == 0)
                {
                    //test des colonnes
                    for (int j = 0; j < 3; j++)
                    {
                        //concaténation des colonnes
                        tabLigne = _grille.getCaseValue(j)  + _grille.getCaseValue(j + 3)  + _grille.getCaseValue(j + 6) ;
                        //test des différentes position des O et affectation du O à l'unique case vide
                        if (tabLigne == car_ord + car_ord)
                        {
                            if (_grille.getCaseValue(j)  == "")
                            {
                                return j;
                                
                            }
                            else if (_grille.getCaseValue(j + 3)  == "")
                            {
                                return j + 3;
                                
                            }
                            else if (_grille.getCaseValue(j + 6)  == "")
                            {
                                return j + 6;
                                
                            }
                        }
                    }

                    //diagonale à partir de la position 0
                    tabLigne = _grille.getCaseValue(0)  + _grille.getCaseValue(4)  + _grille.getCaseValue(8) ;
                    if (tabLigne == car_ord + car_ord)
                    {
                        if (_grille.getCaseValue(0)  == "")
                        {
                            return 0;
                            
                        }
                        else if (_grille.getCaseValue(4)  == "")
                        {
                            return 4;
                            
                        }
                        else if (_grille.getCaseValue(8)  == "")
                        {
                            return 8;
                            
                        }
                    }

                    //diagonale à partir de la position 2
                    tabLigne = _grille.getCaseValue(2)  + _grille.getCaseValue(4)  + _grille.getCaseValue(6) ;

                    if (tabLigne == car_ord + car_ord)
                    {
                        if (_grille.getCaseValue(2)  == "")
                        {
                            return 2;
                            
                        }
                        else if (_grille.getCaseValue(4)  == "")
                        {
                            return 4;
                            
                        }
                        else if (_grille.getCaseValue(6)  == "")
                        {
                            return 6;
                            
                        }
                    }
                }

                //test des lignes
                tabLigne = _grille.getCaseValue(i)  + _grille.getCaseValue(i + 1)  + _grille.getCaseValue(i + 2) ;

                if (tabLigne == car_ord + car_ord)
                {
                    if (_grille.getCaseValue(i)  == "")
                    {
                        return i;
                        
                    }
                    else if (_grille.getCaseValue(i + 1)  == "")
                    {
                        return i + 1;
                        
                    }
                    else if (_grille.getCaseValue(i + 2)  == "")
                    {
                        return i + 2;
                        
                    }
                }
            }

            return -1;
        }

        //cette fonction recherche si 2 'X' sont alignées et empeche l'utilisateur de gagner
        int iaPlayToDefend()
        {
            //test
            string car_ord = _signe_joueur;
            string tabLigne = "FFF";
            for (int i = 0; i < 9; i += 3)
            {
                if (i == 0)
                {
                    //test des colonnes
                    for (int j = 0; j < 3; j++)
                    {
                        //concaténation des colonnes
                        tabLigne = _grille.getCaseValue(j)  + _grille.getCaseValue(j + 3)  + _grille.getCaseValue(j + 6) ;

                        //test des différentes position des O et affectation du O à l'unique case vide
                        if (tabLigne == car_ord + car_ord)
                        {
                            if (_grille.getCaseValue(j)  == "")
                            {
                                return j;
                                
                            }
                            else if (_grille.getCaseValue(j + 3)  == "")
                            {
                                return j + 3;
                                
                            }
                            else if (_grille.getCaseValue(j + 6)  == "")
                            {
                                return j + 6;
                                
                            }
                        }
                    }

                    //diagonale à partir de la position 0
                    tabLigne = _grille.getCaseValue(0)  + _grille.getCaseValue(4)  + _grille.getCaseValue(8) ;
                    if (tabLigne == car_ord + car_ord)
                    {
                        if (_grille.getCaseValue(0)  == "")
                        {
                            return 0;
                            
                        }
                        else if (_grille.getCaseValue(4)  == "")
                        {
                            return 4;
                            
                        }
                        else if (_grille.getCaseValue(8)  == "")
                        {
                            return 8;
                            
                        }
                    }

                    //diagonale à partir de la position 2
                    tabLigne = _grille.getCaseValue(2)  + _grille.getCaseValue(4)  + _grille.getCaseValue(6) ;

                    if (tabLigne == car_ord + car_ord)
                    {
                        if (_grille.getCaseValue(2)  == "")
                        {
                            return 2;
                            
                        }
                        else if (_grille.getCaseValue(4)  == "")
                        {
                            return 4;
                            
                        }
                        else if (_grille.getCaseValue(6)  == "")
                        {
                            return 6;
                            
                        }
                    }
                }

                //test des lignes
                tabLigne = _grille.getCaseValue(i)  + _grille.getCaseValue(i + 1)  + _grille.getCaseValue(i + 2) ;

                if (tabLigne == car_ord + car_ord)
                {
                    if (_grille.getCaseValue(i)  == "")
                    {
                        return i;
                        
                    }
                    else if (_grille.getCaseValue(i + 1)  == "")
                    {
                        return i + 1;
                        
                    }
                    else if (_grille.getCaseValue(i + 2)  == "")
                    {
                        return i + 2;
                        
                    }
                }
            }

            return -1;
        }
    
    }
}