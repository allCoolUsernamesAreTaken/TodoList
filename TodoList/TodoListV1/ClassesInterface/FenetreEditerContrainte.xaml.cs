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

        // CONSTRUCTEUR =========================
        public FenetreEditerContrainte(ContrainteTemps ctrTps)
        {
            InitializeComponent();
            this.ContrainteTraitee = ctrTps;
            this.DataContext = ContrainteTraitee;
        }

    }
}
