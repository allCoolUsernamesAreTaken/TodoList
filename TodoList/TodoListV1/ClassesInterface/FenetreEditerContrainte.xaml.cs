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

        private List<Periodicite> _listePeriodes;
        
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
        public List<Periodicite> ListePeriodes
        {
            get
            {
                return _listePeriodes;
            }

            set
            {
                _listePeriodes = value;
            }
        }


        // CONSTRUCTEUR =========================
        public FenetreEditerContrainte(Tache tch)
        {
            InitializeComponent();
            this.DateCreationTache = tch.DateCreation;
            this.ContrainteTraitee = (tch.ContrainteTps == null) ? new ContrainteTemps() : tch.ContrainteTps;
            this.DataContext = ContrainteTraitee;

            // Calendrier
            this.cal.SelectedDate = this.ContrainteTraitee.DateLimite;

            // Combobox delai d'urgence
            for (int i = 0; i < 15; i++)
            {
                String str = i < 2 ? " jour" : " jours";
                this.cmbBxDelai.Items.Add(new KeyValuePair<String, int>(i + str, i));
            }
            this.cmbBxDelai.DisplayMemberPath = "Key";
            this.cmbBxDelai.SelectedIndex = this.ContrainteTraitee.DelaiUrgence == null ? 0 : this.ContrainteTraitee.DelaiUrgence.Days;

            // Combobox periode
            this.ListePeriodes = new List<Periodicite>();
            this.ListePeriodes.Add(new Periodicite(1, UnitesTemps.jour));
            this.ListePeriodes.Add(new Periodicite(1, UnitesTemps.semaine));
            this.ListePeriodes.Add(new Periodicite(2, UnitesTemps.semaine));
            this.ListePeriodes.Add(new Periodicite(1, UnitesTemps.mois));
            this.ListePeriodes.Add(new Periodicite(2, UnitesTemps.mois));
            this.ListePeriodes.Add(new Periodicite(6, UnitesTemps.mois));
            this.cmbBxPeriodicite.ItemsSource = this.ListePeriodes;
            this.cmbBxPeriodicite.SelectedIndex = this.ContrainteTraitee.Periodicite == null ? 0 : this.ListePeriodes.FindIndex(prd => prd.Equals(this.ContrainteTraitee.Periodicite));

        }

        // METHODES
        private void cal_Click(object sender, SelectionChangedEventArgs e)
        {
            DateTime? myDate = this.cal.SelectedDate;
            if(myDate != null)
            {
                this.lblDateLimite.Content = this.cal.SelectedDate.ToString();
            }

        }

        private void btnSauver_Click(object sender, RoutedEventArgs e)
        {
            // On envoie si au choix la date sélectionnée n'est pas nulle, ou une date limite est déjà établie
            if (this.cal.SelectedDate != null || this.ContrainteTraitee.DateLimite != null)
            {
                PapaJoe.MiseAJourTache(this.DateCreationTache, new ContrainteTemps()
                {
                    DateLimite = (DateTime)this.cal.SelectedDate,
                    DelaiUrgence = new TimeSpan(((KeyValuePair<String, int>)this.cmbBxDelai.SelectedItem).Value, 0, 0, 0),
                    Periodicite = (Periodicite)this.cmbBxPeriodicite.SelectedItem
                });
            }
        }

    }
}
