using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using TodoListV1.Exceptions;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// liste de tâche regroupant l'ensemble des tâches et des progammes d'un utilisateur, 
    /// et les champs nécessaires à sa gestion.
    /// </summary>
    [Serializable]
    public class ListePrincipale : ListeTaches
    {
        // ATTRIBUTS DE CLASSE =========================
        private DateTime _sauvegardeTime;
        private ObservableCollection<Programme> _listeProgrammes;
        private string _message;

        // GETTERS & SETTERS =========================
        [XmlAttribute()]
        public DateTime SauvegardeTime
        {
            get
            {
                return _sauvegardeTime;
            }

            set
            {
                _sauvegardeTime = value;
            }
        }
        [XmlElement()]
        public ObservableCollection<Programme> ListeProgrammes
        {
            get
            {
                return _listeProgrammes;
            }

            set
            {
                _listeProgrammes = value;
            }
        }
        [XmlIgnore()]
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }
        
        // METHODES =========================
        public override void RetirerTache(Tache tch)
        {
            base.RetirerTache(tch);

            if(this.ListeProgrammes != null)
            {
                foreach (Programme prg in this.ListeProgrammes)
                {
                    prg.RetirerTache(tch);
                    prg.MiseAJourTempsTotal();
                }
            }
        }
        public override void MiseAJourTache(Tache tch, Tache newTch)
        {
            base.MiseAJourTache(tch, newTch);
            if(this.ListeProgrammes != null)
            {
                foreach (Programme prg in this.ListeProgrammes)
                {
                    if (prg.ListeDeTaches.Contains(tch))
                    {
                        prg.MiseAJourTempsTotal();
                    }
                }
            }
        }

        // Méthodes de gestion de la liste des programmes
        public bool ContientProgramme(Programme prg)
        {
            if (this.ListeProgrammes != null)
            {
                if(this.RecupererProgramme(prg) != null) {
                    return true;
                }
            }
            return false;
        } // TODO : vérifier la pertinence de la vérification systématique.
        public void AjouterProgramme(Programme prg)
        {
            if (this.ListeProgrammes == null)
            {
                this.ListeProgrammes = new ObservableCollection<Programme>();
            }

            if (this.ContientProgramme(prg))
            {
                // TODO : Exception
                throw new ListePrincipaleException("Le programme existe déjà");
            }
            else
            {
                this.Message = "Le programme va être ajouté";
                this.ListeProgrammes.Add(prg);
            }
        }
        public void RetirerProgramme(Programme prg)
        {
            if (this.ContientProgramme(prg))
            {
                this.ListeProgrammes.Remove(prg);
            }
        }
        public void MiseAJourProgramme(Programme prg, Programme newPrg)
        {
            Programme majPrg = this.RecupererProgramme(prg);
            majPrg.Intitule = newPrg.Intitule;
            majPrg.DureeMax = newPrg.DureeMax;
        }

        public Programme RecupererProgramme(Programme prg)
        {
            return this.ListeProgrammes.FirstOrDefault(p => p.ComparerId(prg));
        }

    }
}
