using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// Programme est une liste de tâches destinée à être créée par l'utilisateur et regrouper un certain nombre de tâches en vue de les exécuter.
    /// </summary>
    [Serializable]
    public class Programme : ListeTaches
    {
        // ATTRIBUTS DE CLASSE
        private string _intitule;
        private TimeSpan _dureeMax;
        private DateTime _dateCreation;


        // GETTERS & SETTERS
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
            }
        }
        [XmlIgnore()]
        public TimeSpan DureeMax // Un TimeSpan ne peut pas être serialisé tel quel. Voir la propriété DureeMaxXml ci-dessous.
        {
            get
            {
                return _dureeMax;
            }

            set
            {
                _dureeMax = value;
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

        // Getters & setters spéciaux
        [XmlAttribute()]
        public string DureeMaxXml // Un TimeSpan ne peut pas être serialisé tel quel. On passe donc par un string.
        {
            get
            {
                return XmlConvert.ToString(_dureeMax);
            }

            set
            {
                _dureeMax = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
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


        //CONSTRUCTEURS
        public Programme() : base() {}

        public Programme(string itl, TimeSpan drmx)
        {
            this.Intitule = itl;
            this.DateCreation = DateTime.UtcNow;
            this.DureeMax = drmx;
            // Le Thread.Sleep permet d'empêcher que deux programmes puissent être créées à la même milliseconde.
            System.Threading.Thread.Sleep(1);
        }


        // METHODES
        public bool ComparerId(object obj) // ComparerId vérifie si les deux objets ont le même identifiant, donc sont sensés être les mêmes.
        {
            bool result = false;
            if (obj.GetType() == this.GetType())
            {
                if (
                    ((Programme)obj).DateCreation.Equals(this.DateCreation)
                    )
                    result = true;
            }
            return result;
        }

        public override string ToString()
        {
            string str = this.Intitule + " " + this.DureeMax.ToString() + "\n";
            foreach (Tache tch in this.ListeDeTaches)
            {
                str += tch.Intitule + "\n";
            }
            return str;
        }

    }
}
