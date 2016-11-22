using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TodoListV1.ClassesMetier;

namespace TodoListV1.ClassesControleur
{
    /// <summary>
    /// PapaJoe est le contrôleur principal du programme. 
    /// C'est une classe statique qui charge la liste de taches principale, 
    /// et gère les interactions entre l'interface et les classes métiers 
    /// </summary>
    static class PapaJoe
    {
        // ATTRIBUTS DE CLASSE
        private static ListePrincipale _listeTachesPrincipale; // Liste de taches principale
        private static string _infos; // String d'informations pour tests
        private static List<KeyValuePair<Statuts, string>> _listeStatuts; // Dictionnaire statuts-strings pour binding


        // GETTERS & SETTERS
        public static ListePrincipale ListeTachesPrincipale
        {
            get
            {
                return _listeTachesPrincipale;
            }

            set
            {
                _listeTachesPrincipale = value;
            }
        }
        
        public static string Infos
        {
            get
            {
                _infos = "";
                if(ListeTachesPrincipale.SauvegardeTime != null)
                {
                    _infos += ListeTachesPrincipale.SauvegardeTime.ToString()+ " - temps total : " + ListeTachesPrincipale.TempsTotal.ToString() + "\n";
                }
                foreach (Tache item in ListeTachesPrincipale.ListeDeTaches)
                {
                    _infos += item.ToString() + "\n";
                }
                return _infos;
            }

            set
            {
                _infos = value;
            }
        }

        public static List<KeyValuePair<Statuts, string>> ListeStatuts
        {
            get
            {
                return _listeStatuts;
            }

            set
            {
                _listeStatuts = value;
            }
        }


        // MISE EN PLACE
        public static void MiseEnPlace()
        {
            // Initialisation de la liste de tâches
            //ListeTachesPrincipale = new ListePrincipale();
            //ListeTachesPrincipale.AjouterTache(new Tache("Ménage", 2, Statuts.aFaire));
            //ListeTachesPrincipale.AjouterTache(new Tache("Jeux", 1.5, Statuts.aFaire));
            //ListeTachesPrincipale.AjouterTache(new Tache("Travail", 3.75, Statuts.aFaire));
            //ListeTachesPrincipale.AjouterTache(new Tache("Courses", 0.5, Statuts.aFaire));
            //ListeTachesPrincipale.AjouterTache(new Tache("Danse", 2.75, Statuts.aFaire));
            // Chargement de la liste.
            DeSerialiserListe();


            // Initialisation du dictionnaire de statuts
            ListeStatuts = new List<KeyValuePair<Statuts, string>>();
            foreach (Statuts item in Enum.GetValues(typeof(Statuts)))
            {
                ListeStatuts.Add(new KeyValuePair<Statuts, string>(item, StatutsMethodes.StatutsToString(item)));
            }

        }

        
        // METHODES
        public static void AjouterTache(string itl, TimeSpan dr)
        {
            ListeTachesPrincipale.AjouterTache(new Tache(itl, dr, Statuts.aFaire));
        }

        public static void RetirerTaches(System.Collections.IList lstTch)
        {
            List<Tache> tmpLstTch = new List<Tache>();
            foreach (Tache item in lstTch)
            {
                tmpLstTch.Add(item);
            }
            foreach (Tache item in tmpLstTch)
            {
                ListeTachesPrincipale.RetirerTache(item);
            }
        }

        public static void MiseAJourTache(Tache oldTch, Tache newTch)
        {
            ListeTachesPrincipale.MiseAJourTache(oldTch, newTch);
        }

        // Sauvegarde et chargement XML
        public static void SerialiserListe()
        {
            // Instanciation de la date de sauvegarde
            ListeTachesPrincipale.SauvegardeTime = DateTime.UtcNow;
            // Process de sérisalition et de sauvegarde
            XmlSerializer serialiseur = new XmlSerializer(typeof(ListePrincipale));
            StreamWriter ecrivain = new StreamWriter("Text.xml", false);
            serialiseur.Serialize(ecrivain, ListeTachesPrincipale);
            ecrivain.Close();
        }

        public static void DeSerialiserListe()
        {
            XmlSerializer serialiseur = new XmlSerializer(typeof(ListePrincipale));
            StreamReader lecteur = new StreamReader("Text.xml");
            ListeTachesPrincipale = (ListePrincipale)serialiseur.Deserialize(lecteur);
            lecteur.Close();
            ListeTachesPrincipale.MiseAJourTempsTotal();
        }

    }
}
