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
    /// Classe regroupant les caractéristiques de contraintes de temps d'une tâche, et les méthodes les concernant
    /// </summary>
    [Serializable]
    public class ContrainteTemps : INotifyPropertyChanged
    {
        // ATTRIBUTS DE CLASSE =========================
        private DateTime _dateLimite;
        private TimeSpan _delaiUrgence;
        private TimeSpan _periodicite;

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
        [XmlIgnore()]
        public TimeSpan Periodicite
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
        [XmlAttribute()]
        public string PeriodiciteXml // Un TimeSpan ne peut pas être serialisé tel quel. On passe donc par un string.
        {
            get
            {
                return XmlConvert.ToString(_periodicite);
            }

            set
            {
                _periodicite = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
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
            return this.DateLimite.ToString();
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
