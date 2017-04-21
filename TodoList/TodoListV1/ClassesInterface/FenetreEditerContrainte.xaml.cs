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
    /// Récupère lors de sa construction une Contrainte à traiter. 
    /// Permet la modification des valeurs de la Contrainte.
    /// </summary>
    public partial class FenetreEditerContrainte : Window
    {
        // ATTRIBUTS DE CLASSE =========================
        private ContrainteTemps _contrainteTraitee;
        private DateTime _dateCreationTache;

        // GETTERS & SETTERS =========================
        public ContrainteTemps ContrainteTraitee
        {
            get
            {
                return _contrainteTraitee;
            }

            set
            {
                _contrainteTraitee = value;
            }
        }
        public DateTime DateCreationTache
        {
            get
            {
                return _dateCreationTache;
            }

            set
            {
                _dateCreationTache = value;
            }
        }

        // Getters & setters spéciaux


        // CONSTRUCTEUR =========================
        public FenetreEditerContrainte(Tache tch)
        {
            InitializeComponent();
            this.DateCreationTache = tch.DateCreation;
            this.ContrainteTraitee = (tch.ContrainteTps == null) ? new ContrainteTemps() : tch.ContrainteTps;
            this.DataContext = ContrainteTraitee;

            string str = " jour";
            for (int i = 0; i < 15; i++)
            {
                this.cmbBxDelai.Items.Add(i + str);
                str = " jours";
            }
            this.cmbBxDelai.SelectedIndex = 0;
        }

        // METHODES
        private void cal_Click(object sender, SelectionChangedEventArgs e)
        {
            DateTime? myDate = this.cal.SelectedDate;
            if(myDate != null)
            {
                this.lblDateLimite.Content = this.cal.SelectedDate.ToString();
                //MessageBox.Show(this.ContrainteTraitee.ToString());
            }

        }

        private void btnSauver_Click(object sender, RoutedEventArgs e)
        {
            DateTime? myDate = this.cal.SelectedDate;
            if (myDate != null)
            {
                PapaJoe.MiseAJourTache(this.DateCreationTache, new ContrainteTemps() { DateLimite = (DateTime)myDate });
            }
        }
    }
}
