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
    /// ListeTache est le modèle abstrait de conteneur de Tâches avec un certain nombre de méthodes de traitement associées.
    /// Elle sert de parent à la Liste principale et aux programmes.
    /// </summary>
    [Serializable]
    public abstract class ListeTaches
    {
        // ATTRIBUTS DE CLASSE
        private ObservableCollection<Tache> _listeDeTaches; // TODO : Vérifier s'il y a une collection plus appropriée
        //private DateTime _sauvegardeTime;
        private TimeSpan _tempsTotal;

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
        [XmlIgnore()]
        public TimeSpan TempsTotal
        {
            get
            {
                return _tempsTotal;
            }

            set
            {
                _tempsTotal = value;
            }
        }


        // CONSTRUCTEURS
        public ListeTaches(): base() {
            this.ListeDeTaches = new ObservableCollection<Tache>();
        }


        // METHODES
        public virtual void AjouterTache(Tache tch)
        {
            this.ListeDeTaches.Add(tch);
            MiseAJourTempsTotal();
        }

        public virtual void RetirerTache(Tache tch)
        {
            this.ListeDeTaches.Remove(tch);
            MiseAJourTempsTotal();
        }

        public virtual void SupprimerTache(Tache tch)
        {
            tch.Dispose();
            MiseAJourTempsTotal();
        }

        public virtual void MiseAJourTache(Tache tch, Tache newTch)
        {
            Tache majTch = this.ListeDeTaches.FirstOrDefault(t => t.ComparerId(tch));
            majTch.Intitule = newTch.Intitule;
            majTch.Duree = newTch.Duree;
            majTch.Statut = newTch.Statut;
            MiseAJourTempsTotal();
        }

        public virtual void MiseAJourTempsTotal()
        {
            TempsTotal = new TimeSpan(0, 0, 0, 0);
            foreach (Tache item in ListeDeTaches)
            {
                TempsTotal = TempsTotal.Add(item.Duree);
            }
        }
    }
}
