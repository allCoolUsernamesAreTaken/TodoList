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
    /// Récupère lors de sa construction une Tâche à traiter. 
    /// Permet la modification des valeurs de la Tâche.
    /// La touche "sauver" envoie à PapaJoe la Tâche avant modification, pour identification, 
    /// et la tâche après modification, pour remplacement
    /// </summary>
    public partial class FenetreEditerTache : Window
    {
        // ATTRIBUTS DE CLASSE =========================
        private Tache _tacheTraitee;
        private int _heures;
        private int _minutes;
        
        // GETTERS & SETTERS =========================
        public Tache TacheTraitee
        {
            get
            {
                return _tacheTraitee;
            }

            set
            {
                _tacheTraitee = value;
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
        
        // CONSTRUCTEUR =========================
        public FenetreEditerTache(MainWindow Wndw, Tache tch)
        {
            InitializeComponent();
            // Récupération des données en local 
            this.TacheTraitee = tch;
            // Récupération des paramètres de timeSpan
            this.Heures = (int)this.TacheTraitee.Duree.Hours;
            this.Minutes = (int)this.TacheTraitee.Duree.Minutes;
            // Datacontexts et bindings
            this.DataContext = TacheTraitee;
            this.lblDuree.DataContext = LabelDuree;
            this.cmbBxStatut.ItemsSource = PapaJoe.ListeStatuts;
        }

        // METHODES =========================
        private void btnSauver_Click(object sender, RoutedEventArgs e)
        {
            // Variables de validation ou d'erreur
            bool ok = true;
            string errorStr = "";

            // Récupération des champs
            string itl = this.txtBxIntitule.Text;
            TimeSpan dr = new TimeSpan(0, this.Heures, this.Minutes, 0);
            Statuts stt = (Statuts)this.cmbBxStatut.SelectedValue;
            string nts = this.txtBxNotes.Text;

            // Envoi des champs pour update ou Message d'erreur
            if (ok)
            {
                PapaJoe.MiseAJourTache(this.TacheTraitee, new Tache(itl, dr, stt, nts));
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
        private void btnEditerCtr_Click(object sender, RoutedEventArgs e)
        {
            new FenetreEditerContrainte(this.TacheTraitee.ContrainteTps).Show();
        }
    }
}
