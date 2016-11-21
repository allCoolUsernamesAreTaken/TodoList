using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TodoListV1.ClassesControleur;
using TodoListV1.ClassesInterface;
using TodoListV1.ClassesMetier;

namespace TodoListV1
{
    /// <summary>
    /// MainWindow.xaml initialise ses composants, réveille PapaJoe, et se binde sur ses données
    /// </summary>
    public partial class MainWindow : Window
    {
        // ATTRIBUTS DE CLASSE
        private string _labelDuree;
        private int _heures;
        private int _minutes;


        // GETTERS & SETTERS
        public string LabelDuree
        {
            get
            {
                string hrsStr;
                string mnsStr;

                if (this.Heures < 10)
                {
                    hrsStr = "0" + this.Heures.ToString();
                }
                else
                {
                    hrsStr = this.Heures.ToString();
                }

                if (this.Minutes < 10)
                {
                    mnsStr = "0" + this.Minutes.ToString();
                }
                else
                {
                    mnsStr = this.Minutes.ToString();
                }

                return "Durée : " + hrsStr + ":" + mnsStr;
            }

            set
            {
                _labelDuree = value;
            }
        }

        public int Heures
        {
            get
            {
                return _heures;
            }

            set
            {
                _heures = value;
            }
        }

        public int Minutes
        {
            get
            {
                return _minutes;
            }

            set
            {
                _minutes = value;
            }
        }


        // LANCEMENT DU PROGRAMME
        public MainWindow()
        {
            InitializeComponent(); // Initialisation automatique
            PapaJoe.MiseEnPlace(); // Initialisation du contrôleur
            ReglagesManuels(); // Initialisation des objets 
        }

        public void ReglagesManuels()
        {
            // Binding de la ListView
            this.lstVwListeTaches.DataContext = PapaJoe.ListeTachesPrincipale;
            
            // Binding du Label d'affichage de la durée
            this.Heures = 0;
            this.Minutes = 0;
            this.lblDuree.DataContext = this.LabelDuree;
        }


        // METHODES
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            PapaJoe.AjouterTache(this.txtBxIntitule.Text, new TimeSpan(0, this.Heures, this.Minutes, 0));
        }

        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            // TODO : voir s'il est possible de modifier l'UpdateSourceTrigger le temps du foreach pour ne pas passer par une seconde liste.
            PapaJoe.RetirerTaches(this.lstVwListeTaches.SelectedItems);
        }

        private void btnEditer_Click(object sender, RoutedEventArgs e)
        {
            foreach (Tache item in this.lstVwListeTaches.SelectedItems)
            {
                FenetreEditerTache fntrEdTch = new FenetreEditerTache(this, item);
                fntrEdTch.Show();
            }
        }
        
        // Infos génériques
        private void btnInfos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(PapaJoe.Infos);
        }

        // Sauvegarde et chargement seriels XML
        private void btnSerialiser_Click(object sender, RoutedEventArgs e)
        {
            PapaJoe.SerialiserListe();
        }

        private void btnDeserialiser_Click(object sender, RoutedEventArgs e)
        {
            PapaJoe.DeSerialiserListe();
            this.lstVwListeTaches.DataContext = PapaJoe.ListeTachesPrincipale; // Rafraichit la vue.
        }

        // Calcul des variables de durée
        private void sldrDuree_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int[] hrsMns = CalculDuree((int)this.sldrDuree.Value);
            this.Heures = hrsMns[0];
            this.Minutes = hrsMns[1];
            this.lblDuree.DataContext = this.LabelDuree; // TODO : vérifier pourquoi le refresh ne se fait pas automatiquement.
        }

        public int[] CalculDuree(int dr)
        {
            // Réglages de la granularité du slider
            int[] hrsMns = {0,0};
            if (dr < 12) // Granularité de 5 mn en-dessous d'une heure
            {
                hrsMns[0] = 0;
                hrsMns[1] = dr * 5;
            }
            else if (dr >= 12 && dr < 24) // Passage à 15 mn entre 1 et 4h
            {
                hrsMns[0] = (int)(1 + (dr - 12) / 4);
                hrsMns[1] = (int)(((dr - 12) % 4) * 15);
            }
            else if (dr >= 24 && dr < 32) // Passage à demi-heures entre 4 et 8h
            {
                hrsMns[0] = (int)(4 + (dr - 24) / 2);
                hrsMns[1] = (int)(((dr - 24) % 2) * 30);
            }
            else // Passage à heures au-delà de 8h
            {
                hrsMns[0] = 8 + (dr - 32);
                hrsMns[1] = 0;
            }
            return hrsMns;
        }
    }
}
