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
    /// liste de tâche regroupant l'ensemble des tâches d'un utilisateur, et les champs nécessaires à sa gestion.
    /// </summary>
    [Serializable]
    public class ListePrincipale : ListeTaches
    {
        // ATTRIBUTS DE CLASSE
        private DateTime _sauvegardeTime;
        private ObservableCollection<Programme> _listeProgrammes;


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
    }
}
