using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TodoListV1.Exceptions;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// ListeTache est le modèle abstrait de conteneur de Tâches avec un certain nombre de méthodes de traitement associées.
    /// Elle sert de parent à la Liste principale et aux programmes.
    /// </summary>
    [Serializable]
    public abstract class ListeTaches : INotifyPropertyChanged
    {
        // ATTRIBUTS DE CLASSE =========================
        private ObservableCollection<Tache> _listeDeTaches; // TODO : Vérifier s'il y a une collection plus appropriée
        private TimeSpan _dureeTotale;
        public event PropertyChangedEventHandler PropertyChanged; // Interface INotifyPropertyChanged

        // GETTERS & SETTERS =========================
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
        public TimeSpan DureeTotale
        {
            get
            {
                return _dureeTotale;
            }

            set
            {
                _dureeTotale = value;
                OnPropertyChanged("DureeTotale");
            }
        }

        // CONSTRUCTEURS =========================
        public ListeTaches(): base() {
            this.ListeDeTaches = new ObservableCollection<Tache>();
        }
        
        // METHODES =========================
        public virtual void AjouterTache(Tache tch)
        {
            if (!this.ContientTache(tch))
            {
                this.ListeDeTaches.Add(tch);
                MiseAJourTempsTotal();
            }
            else
            {
                throw new ListeTachesException("La tâche est déjà présente dans la liste.");
            }
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
            tch = this.RecupereTache(tch);
            tch.Intitule = newTch.Intitule;
            tch.Duree = newTch.Duree;
            tch.Statut = newTch.Statut;
            MiseAJourTempsTotal();
        }
        public virtual void MiseAJourTempsTotal()
        {
            DureeTotale = new TimeSpan(0, 0, 0, 0);
            foreach (Tache item in ListeDeTaches)
            {
                DureeTotale = DureeTotale.Add(item.Duree);
            }
        }
        public virtual bool ContientTache(Tache tch)
        {
            return RecupereTache(tch) != null;
        }
        public virtual Tache RecupereTache(Tache tch)
        {
            return this.ListeDeTaches.FirstOrDefault(t => t.ComparerId(tch));
        }

        // Méthode de l'interface INotifyPropertyChanged
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
