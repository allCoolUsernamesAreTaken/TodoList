using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// ListeTache est un conteneur de Tâches avec un certain nombre de méthodes de traitement associées.
    /// Actuellement sa seule utilité est de gérer la liste de taches, 
    /// mais il permettra à terme de créer des programmes de tâches
    /// </summary>
    [Serializable]
    public class ListeTaches
    {
        // ATTRIBUTS DE CLASSE
        private ObservableCollection<Tache> _listeDeTaches; // TODO : Vérifier s'il y a une collection plus appropriée
        private DateTime _sauvegardeTime;

        // GETTERS & SETTERS
        [XmlElement()]
        public ObservableCollection<Tache> ListeDeTaches
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


        // CONSTRUCTEURS
        public ListeTaches(): base() {
            this.ListeDeTaches = new ObservableCollection<Tache>();
        }


        // METHODES
        public void AjouterTache(Tache tch)
        {
            this.ListeDeTaches.Add(tch);
        }

        public void RetirerTache(Tache tch)
        {
            this.ListeDeTaches.Remove(tch);
        }

        public void SupprimerTache(Tache tch)
        {
            tch.Dispose();
        }

        public void MiseAJourTache(Tache tch, Tache newTch)
        {
            Tache majTch = this.ListeDeTaches.FirstOrDefault(t => t.Equals(tch));
            majTch.Intitule = newTch.Intitule;
            majTch.Duree = newTch.Duree;
            majTch.Statut = newTch.Statut;
        }
    }
}
