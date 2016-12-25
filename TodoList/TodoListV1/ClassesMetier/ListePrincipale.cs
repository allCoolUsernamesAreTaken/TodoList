using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// liste de tâche regroupant l'ensemble des tâches et des progammes d'un utilisateur, 
    /// et les champs nécessaires à sa gestion.
    /// </summary>
    [Serializable]
    public class ListePrincipale : ListeTaches
    {
        // ATTRIBUTS DE CLASSE
        private DateTime _sauvegardeTime;
        private ObservableCollection<Programme> _listeProgrammes;
        private string _message;

        // GETTERS & SETTERS
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


        // METHODES
        public override void RetirerTache(Tache tch)
        {
            if(this.ListeProgrammes.Count()>0)
            {
                foreach (Programme prg in this.ListeProgrammes)
                {
                    prg.RetirerTache(tch);
                }
            }
            this.ListeDeTaches.Remove(tch);
            MiseAJourTempsTotal();
        }

        // Méthodes de gestion de la liste des programmes
        public bool ChercherProgramme(Programme prg)
        {
            if (this.ListeProgrammes != null && this.ListeProgrammes.Count() > 0)
            {
                foreach (Programme item in this.ListeProgrammes)
                {
                    if (prg.ComparerId(item))
                    {
                        return true;
                    }
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

            if (this.ChercherProgramme(prg))
            {
                this.Message = "Le programme existe déjà";
            }
            else
            {
                this.Message = "Le programme va être ajouté";
                this.ListeProgrammes.Add(prg);
            }
        }

        public void RetirerProgramme(Programme prg)
        {
            if (this.ChercherProgramme(prg))
            {
                this.ListeProgrammes.Remove(prg);
            }
        }

        public void MiseAJourProgramme(Programme prg, Programme newPrg)
        {
            Programme majPrg = this.ListeProgrammes.FirstOrDefault(p => p.ComparerId(prg));
            majPrg.Intitule = newPrg.Intitule;
            majPrg.DureeMax = newPrg.DureeMax;
        }

    }
}
