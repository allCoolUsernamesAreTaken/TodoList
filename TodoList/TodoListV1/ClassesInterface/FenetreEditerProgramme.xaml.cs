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
using System.Windows.Shapes;
using TodoListV1.ClassesControleur;
using TodoListV1.ClassesMetier;

namespace TodoListV1.ClassesInterface
{
    /// <summary>
    /// Récupère lors de sa construction un Programme à traiter et la window qui l'envoie. 
    /// Permet la modification des valeurs du Programme.
    /// La touche "sauver" envoie à PapaJoe le Programme avant modification, pour identification, 
    /// et le programme après modification, pour remplacement
    /// </summary>
    public partial class FenetreEditerProgramme : Window
    {

        // ATTRIBUTS DE CLASSE
        private FenetreProgrammes _windowSender;
        private Programme _programmeTraite;
        private int _heures;
        private int _minutes;


        // GETTERS & SETTERS
        public FenetreProgrammes WindowSender
        {
            get
            {
                return _windowSender;
            }

            set
            {
                _windowSender = value;
            }
        }

        public Programme ProgrammeTraite
        {
            get
            {
                return _programmeTraite;
            }

            set
            {
                _programmeTraite = value;
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

        // Getters & setters spéciaux
        public string LabelDuree
        {
            get
            {
                return MamaJane.CalculLabelDuree(this.Heures, this.Minutes);
            }
        }


        // CONSTRUCTEUR
        public FenetreEditerProgramme(FenetreProgrammes Wndw, Programme prg)
        {
            InitializeComponent();
            // Binding de la ListView
            this.lstVwListeTaches.DataContext = PapaJoe.ListeTachesPrincipale;
            // Récupération des données en local 
            this.WindowSender = Wndw;
            this.ProgrammeTraite = prg;
            // Récupération des paramètres de timeSpan
            this.Heures = (int)this.ProgrammeTraite.DureeMax.Hours;
            this.Minutes = (int)this.ProgrammeTraite.DureeMax.Minutes;
            // Datacontexts et bindings
            this.DataContext = ProgrammeTraite;
            this.lblDuree.DataContext = LabelDuree;

        }

        // METHODES
        // Sauvegarde des modifications du programme
        private void btnSauver_Click(object sender, RoutedEventArgs e)
        {
            // Variables de validation ou d'erreur
            bool ok = true;
            string errorStr = "";

            // Récupération des champs
            string itl = this.txtBxIntitule.Text;
            TimeSpan dr = new TimeSpan(0, this.Heures, this.Minutes, 0);
            
            // Envoi des champs pour update ou Message d'erreur
            if (ok)
            {
                PapaJoe.MiseAJourProgramme(this.ProgrammeTraite, new Programme(itl, dr));
                this.WindowSender.lstVwListeProgrammes.Items.Refresh();
            }
            else
            {
                MessageBox.Show(errorStr);
            }

        }

        private void sldrDuree_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int[] hrsMns = MamaJane.CalculDuree((int)this.sldrDuree.Value);
            this.Heures = hrsMns[0];
            this.Minutes = hrsMns[1];
            this.lblDuree.DataContext = this.LabelDuree; // TODO : vérifier pourquoi le refresh ne se fait pas automatiquement.
        }

        private void btnAjouterTache_Click(object sender, RoutedEventArgs e)
        {
            foreach (Tache tch in this.lstVwListeTaches.SelectedItems)
            {
                PapaJoe.AjouterTache(tch, this.ProgrammeTraite);
            }
        }

        private void btnRetirerTache_Click(object sender, RoutedEventArgs e)
        {
            PapaJoe.RetirerTaches(this.lstVwTachesProgramme.SelectedItems, this.ProgrammeTraite);
        }
    }
}
