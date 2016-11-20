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

        // LANCEMENT DU PROGRAMME
        public MainWindow()
        {
            InitializeComponent(); // Initialisation automatique
            PapaJoe.MiseEnPlace(); // Initialisation du contrôleur
            ReglagesManuels(); // Initialisation des objets 
        }

        public void ReglagesManuels()
        {
            this.lstVwListeTaches.DataContext = PapaJoe.ListeTachesPrincipale;
        }


        // METHODES
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            double dr;
            if(double.TryParse(this.txtBxDuree.Text, out dr))
            {
                PapaJoe.AjouterTache(this.txtBxIntitule.Text, dr);
            }
            else
            {
                MessageBox.Show("La durée entrée n'est pas valide");
            }
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
    }
}
