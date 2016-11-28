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

namespace TodoListV1.ClassesInterface
{
    /// <summary>
    /// FenetreProgrammes est la fenêtre principale d'interaction avec la liste de programmes
    /// </summary>
    public partial class FenetreProgrammes : Window
    {
        // ATTRIBUTS DE CLASSES
        private int _heures;
        private int _minutes;

        // GETTERS & SETTERS
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


        public FenetreProgrammes()
        {
            InitializeComponent();
            ReglagesManuels();
        }

        public void ReglagesManuels()
        {
            // Binding de la ListView
            this.lstVwListeProgrammes.DataContext = PapaJoe.ListeTachesPrincipale;

            // Binding du Label d'affichage de la durée
            this.Heures = 0;
            this.Minutes = 0;
            this.lblDuree.DataContext = this.LabelDuree;
        }

        private void sldrDuree_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int[] hrsMns = MamaJane.CalculDuree((int)this.sldrDuree.Value);
            this.Heures = hrsMns[0];
            this.Minutes = hrsMns[1];
            this.lblDuree.DataContext = this.LabelDuree; // TODO : vérifier pourquoi le refresh ne se fait pas automatiquement.
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            //TODO : méthode d'ajout
        }

        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            //TODO : méthode de retrait
        }

        private void btnEditer_Click(object sender, RoutedEventArgs e)
        {
            //TODO : méthode d'édition
        }
    }
}
