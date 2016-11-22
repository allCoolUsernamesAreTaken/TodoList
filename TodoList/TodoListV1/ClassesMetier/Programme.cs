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
        private TimeSpan _dureeMax;


        // GETTERS & SETTERS
        [XmlIgnore()]
        public TimeSpan DureeMax
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

        [XmlElement()]
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


    }
}
