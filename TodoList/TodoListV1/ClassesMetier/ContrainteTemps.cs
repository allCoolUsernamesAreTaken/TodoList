using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TodoListV1.Exceptions;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// Classe regroupant les caractéristiques de contraintes de temps d'une tâche, et les méthodes les concernant
    /// </summary>
    [Serializable]
    public class ContrainteTemps : INotifyPropertyChanged
    {
        // ATTRIBUTS DE CLASSE =========================
        private DateTime _dateLimite;
        private TimeSpan _delaiUrgence;
        private Periodicite _periodicite;

        public event PropertyChangedEventHandler PropertyChanged;

        // GETTERS & SETTERS =========================

        [XmlAttribute()]
        public DateTime DateLimite
        {
            get
            {
                return _dateLimite;
            }

            set
            {
                _dateLimite = value;
                OnPropertyChanged("DateLimite");
                OnPropertyChanged("DateLimiteString");
            }
        }
        [XmlIgnore()]
        public TimeSpan DelaiUrgence
        {
            get
            {
                return _delaiUrgence;
            }

            set
            {
                _delaiUrgence = value;
                OnPropertyChanged("DelaiUrgence");
            }
        }
        [XmlElement()]
        public Periodicite Periodicite
        {
            get
            {
                return _periodicite;
            }

            set
            {
                _periodicite = value;
                OnPropertyChanged("Periodicite");
            }
        }

        // Getters & Setters spéciaux
        [XmlAttribute()]
        public string DelaiXml // Un TimeSpan ne peut pas être serialisé tel quel. On passe donc par un string.
        {
            get
            {
                return XmlConvert.ToString(_delaiUrgence);
            }

            set
            {
                _delaiUrgence = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        [XmlIgnore()]
        public string DateLimiteString
        {
            get
            {
                return DateLimite.ToString("dd/MM/yyyy");
            }
        }
        [XmlIgnore()]
        public string DelaiUrgenceString
        {
            get
            {
                return DelaiUrgence.Days < 2 ? DelaiUrgence.Days + "jour" : DelaiUrgence.Days + "jours";
            }
        }

        // CONSTRUCTEUR =========================
        public ContrainteTemps() : base() {}

        // METHODES =========================
        public Boolean EstEnRetard()
        {
            DateTime maintenant = DateTime.UtcNow;
            return (DateTime.Compare(maintenant, this.DateLimite) > 0);
        }

        public Boolean EstUrgent()
        {
            DateTime maintenant = DateTime.UtcNow;
            return (DateTime.Compare(maintenant, this.DateLimite.Subtract(this.DelaiUrgence)) > 0);
        }

        public override String ToString()
        {
            string str = this.DateLimiteString;
            if(this.DelaiUrgence != null)
            {
                str += " | " + this.DelaiUrgenceString;
            }
            if(this.Periodicite != null)
            {
                str += " | " + this.Periodicite.ToString();
            }
            return str;
        }

        public DateTime AjoutPeriodicite()
        {
            if(this.Periodicite == null)
            {
                return this.DateLimite;
            }else
            {
                switch (this.Periodicite.Unite)
                {
                    case UnitesTemps.heure:
                        return this.DateLimite.AddHours(this.Periodicite.Quantite);
                    case UnitesTemps.jour:
                        return this.DateLimite.AddDays(this.Periodicite.Quantite);
                    case UnitesTemps.semaine:
                        return this.DateLimite.AddDays(this.Periodicite.Quantite*7);
                    case UnitesTemps.mois:
                        return this.DateLimite.AddMonths(this.Periodicite.Quantite);
                    default:
                        throw new UnitesTempsException("Unité de temps inconnue");
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
