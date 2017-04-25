using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TodoListV1.ClassesMetier
{
    [Serializable]
    public class Periodicite
    {
        // ATTRIBUTS DE CLASSE =========================
        private int _quantite;
        private UnitesTemps _unite;

        // GETTERS & SETTERS =========================
        [XmlAttribute()]
        public int Quantite
        {
            get
            {
                return _quantite;
            }

            set
            {
                _quantite = value;
            }
        }
        [XmlAttribute()]
        public UnitesTemps Unite
        {
            get
            {
                return _unite;
            }

            set
            {
                _unite = value;
            }
        }

        // CONSTRUCTEUR =========================
        public Periodicite() : base() { }
        public Periodicite(int qtt, UnitesTemps ut)
        {
            this.Quantite = qtt;
            this.Unite = ut;
        }

    }
}
