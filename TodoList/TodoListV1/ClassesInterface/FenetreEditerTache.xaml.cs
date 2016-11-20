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
    /// Récupère lors de sa construction une Tâche à traiter et la window qui l'envoie. 
    /// Permet la modification des valeurs de la Tâche.
    /// La touche "sauver" envoie à PapaJoe la Tâche avant modification, pour identification, 
    /// et la tâche après modification, pour remplacement
    /// </summary>
    public partial class FenetreEditerTache : Window
    {
        // ATTRIBUTS DE CLASSE
        private MainWindow _windowSender;
        private Tache _tacheTraitee;

        // GETTERS & SETTERS
        public MainWindow WindowSender
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


        // CONSTRUCTEUR
        public FenetreEditerTache(MainWindow Wndw, Tache tch)
        {
            InitializeComponent();
            // Récupération des données en local 
            this.WindowSender = Wndw;
            this.TacheTraitee = tch;
            this.DataContext = TacheTraitee;
            this.cmbBxStatut.ItemsSource = PapaJoe.ListeStatuts;
        }

        // METHODES
        // Sauvegarde des modifications de la tâche
        private void btnSauver_Click(object sender, RoutedEventArgs e)
        {
            // Variables de validation ou d'erreur
            bool ok = true;
            string errorStr = "";

            // Récupération des champs
            string itl = this.txtBxIntitule.Text;
            double dr;
            if (!double.TryParse(this.txtBxDuree.Text, out dr))
            {
                errorStr += "La durée est invalide";
                ok = false;
            }
            Statuts stt = (Statuts)this.cmbBxStatut.SelectedValue;

            // Envoi des champs pour update ou Message d'erreur
            if (ok)
            {
                PapaJoe.MiseAJourTache(this.TacheTraitee, new Tache(itl, dr, stt));
                this.WindowSender.lstVwListeTaches.Items.Refresh();
            }
            else
            {
                MessageBox.Show(errorStr);
            }
        }
    }
}
