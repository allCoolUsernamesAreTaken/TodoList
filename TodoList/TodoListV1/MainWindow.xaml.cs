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
        // ATTRIBUTS DE CLASSE =========================
        private int _heures;
        private int _minutes;

        // GETTERS & SETTERS =========================
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

        // Getters & setters spéciaux
        public string LabelDuree
        {
            get
            {
                return MamaJane.CalculLabelDuree(this.Heures, this.Minutes);
            }
        }

        // LANCEMENT DU PROGRAMME =========================
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

        // METHODES =========================
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            PapaJoe.CreerTache(this.txtBxIntitule.Text, new TimeSpan(0, this.Heures, this.Minutes, 0));
        }
        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            PapaJoe.RetirerTaches(this.lstVwListeTaches.SelectedItems, PapaJoe.ListeTachesPrincipale);
        }
        private void btnEditer_Click(object sender, RoutedEventArgs e)
        {
            foreach (Tache item in this.lstVwListeTaches.SelectedItems)
            {
                new FenetreEditerTache(this, item).Show();
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
            int[] hrsMns = MamaJane.CalculDuree((int)this.sldrDuree.Value);
            this.Heures = hrsMns[0];
            this.Minutes = hrsMns[1];
            this.lblDuree.DataContext = this.LabelDuree; // TODO : vérifier pourquoi le refresh ne se fait pas automatiquement.
        }

        // Affichage de la fenêtre des programmes
        private void btnProgrammes_Click(object sender, RoutedEventArgs e)
        {
            new FenetreProgrammes().Show();
        }
    }
}
