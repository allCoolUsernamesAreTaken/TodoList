using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListV1.ClassesMetier;

namespace TodoListV1.ClassesControleur
{
    /// <summary>
    /// L'actualiseur est le contrôleur chargé de checker les tâches d'une liste 
    /// et de mettre à jour leurs caractéristiques si nécessaire (notamment selon leurs délais)
    /// </summary>
    public class ActualiseurListeTaches
    {
        // ATTRIBUTS DE CLASSE =========================
        private ListeTaches _listeDeTaches;

        // GETTERS & SETTERS =========================
        public ListeTaches ListeDeTaches
        {
            get
            {
                return _listeDeTaches;
            }

            set
            {
                _listeDeTaches = value;
            }
        }

        // CONSTRUCTEURS =========================
        public ActualiseurListeTaches() : base() {}
        public ActualiseurListeTaches(ListeTaches lstTch) : base()
        {
            this.ListeDeTaches = lstTch;
        }

        // METHODES =========================
        static void ActualiserListeTaches(ListeTaches lstTch)
        {
            foreach (Tache tch in lstTch.ListeDeTaches)
            {
                tch.ActualiserStatut();
                // Création de nouvelles tâches pour les tâches à périodicité dont la date est passée
                // Todo : pas de création en boucle. Donc un statut pour la tâche qui en a enfanté une autre.
                if(tch.idTacheRepetee == null && tch.ContrainteTps.EstEnRetard() && tch.ContrainteTps.Periodicite != null)
                {
                    Tache nwTch = new Tache(
                        tch.Intitule,
                        tch.Duree,
                        Statuts.aFaire,
                        tch.Notes,
                        new ContrainteTemps()
                        {
                            DateLimite = tch.ContrainteTps.AjoutPeriodicite(),
                            DelaiUrgence = tch.ContrainteTps.DelaiUrgence,
                            Periodicite = tch.ContrainteTps.Periodicite
                        }
                        );
                    lstTch.AjouterTache(nwTch);
                    tch.idTacheRepetee = nwTch.DateCreation;
                }
            }
        }
    }
}
