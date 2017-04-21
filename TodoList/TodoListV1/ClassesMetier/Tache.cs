using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// Tache est la classe représentant une tâche à accomplir.
    /// Elle présente un set complet d'attributs et des méthodes standards de comparaison, 
    /// mais aussi des getters d'attributs traduits en string.
    /// </summary>
    [Serializable]
    public class Tache : IDisposable, INotifyPropertyChanged
    {
        // ATTRIBUTS DE CLASSE =========================
        private string _intitule;
        private TimeSpan _duree;
        private Statuts _statut;
        private DateTime _dateCreation;
        private string _notes;
        private ContrainteTemps _contrainteTps;

        public event PropertyChangedEventHandler PropertyChanged;

        // GETTERS & SETTERS =========================
        [XmlAttribute()]
        public string Intitule
        {
            get
            {
                return _intitule;
            }

            set
            {
                _intitule = value;
                OnPropertyChanged("Intitule");
            }
        }
        [XmlIgnore()]
        public TimeSpan Duree
        {
            get
            {
                return _duree;
            }

            set
            {
                _duree = value;
                OnPropertyChanged("DureeString");
            }
        }
        [XmlAttribute()]
        public Statuts Statut
        {
            get
            {
                return _statut;
            }

            set
            {
                _statut = value;
                OnPropertyChanged("StatutString");
            }
        }
        [XmlAttribute()]
        public DateTime DateCreation
        {
            get
            {
                return _dateCreation;
            }

            set
            {
                _dateCreation = value;
            }
        }
        [XmlElement()]
        public string Notes
        {
            get
            {
                return _notes;
            }

            set
            {
                _notes = value;
                OnPropertyChanged("Notes");
            }
        }
        [XmlElement()]
        public ContrainteTemps ContrainteTps
        {
            get
            {
                return _contrainteTps;
            }

            set
            {
                _contrainteTps = value;
                OnPropertyChanged("ContrainteTps");
                OnPropertyChanged("ContrainteTpsString");
            }
        }


        // Getters & Setters spéciaux
        [XmlIgnore()]
        public string StatutString
        {
            get
            {
                return StatutsMethodes.StatutsToString(_statut);
                // Retourne la string correspondante au statut
            }
        }
        [XmlIgnore()]
        public string DureeString
        {
            get
            {
                char[] toTrim1 = {'0'};
                char[] toTrim2 = { ':' };
                return _duree.ToString().TrimEnd(toTrim1).TrimEnd(toTrim2);
            }

            set
            {
                //TODO : mettre un mécanisme de vérification avec TryParse et rejet d'exception
                _duree = DoubleToTimeSpan(double.Parse(value));
            }

        }
        [XmlIgnore()]
        public string DateCreationString
        {
            get
            {
                return _dateCreation.ToString();
            }
        }
        [XmlIgnore()]
        public string ContrainteTpsString
        {
            get
            {
                return (this.ContrainteTps == null) ? "Aucune contrainte" : _contrainteTps.ToString();
            }
        }
        [XmlAttribute()]
        public string DureeXml // Un TimeSpan ne peut pas être serialisé tel quel. On passe donc par un string.
        {
            get
            {
                return XmlConvert.ToString(_duree);
            }

            set
            {
                _duree = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

        // CONSTRUCTEURS =========================
        public Tache() : base() {
            this.DateCreation = DateTime.UtcNow;
            // Le Thread.Sleep permet d'empêcher que deux tâches puissent être créées à la même milliseconde.
            System.Threading.Thread.Sleep(1);
        }
        public Tache(string itl, double dr, Statuts stt, string nts = null, ContrainteTemps ctrTps = null) : this(itl, DoubleToTimeSpan(dr), stt, nts, ctrTps) { }// Constructeur avec doubleToTimeSpan. Si supprimé, supprimer aussi DoubleToTimeSpan()
        public Tache(string itl, TimeSpan tmSpn, Statuts stt, string nts = null, ContrainteTemps ctrTps = null) // Constructeur avec TimeSpan
        {
            this.Intitule = itl;
            this.Duree = tmSpn;
            this.Statut = stt;
            this.Notes = nts;
            this.ContrainteTps = ctrTps;
            this.DateCreation = DateTime.UtcNow;
            // Le Thread.Sleep permet d'empêcher que deux tâches puissent être créées à la même milliseconde.
            System.Threading.Thread.Sleep(1);
        }

        // METHODES =========================
        public bool ComparerId(object obj)  // ComparerId vérifie si les deux objets ont le même identifiant, donc sont sensés être les mêmes.
        {
            bool result = false;
            if (obj.GetType() == this.GetType())
            {
                if (
                    ((Tache)obj).DateCreation.Equals(this.DateCreation)
                    )
                result = true;
            }
            return result;
        }
        public override string ToString()
        {
            return this.Intitule 
                + " " + this.Duree.ToString() + "h" 
                + " " + StatutsMethodes.StatutsToString(this.Statut)
                +" " + this.DateCreationString;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public static TimeSpan DoubleToTimeSpan(Double dbl)
        {
            int dys = (int)(dbl / 24);
            int hrs = (int)(dbl % 24);
            int mns = (int)((dbl % 1) * 60);
            return new TimeSpan(dys, hrs, mns, 0);
        }
        public void UrgenceStatut()
        {
            if (this.ContrainteTps != null)
            {
                if (this.ContrainteTps.EstEnRetard())
                {
                    this.Statut = Statuts.enRetard;
                }
                else if (this.ContrainteTps.EstUrgent())
                {
                    this.Statut = Statuts.urgent;
                }
            }
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
