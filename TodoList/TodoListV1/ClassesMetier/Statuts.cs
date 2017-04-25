using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListV1.Exceptions;

namespace TodoListV1.ClassesMetier
{
    /// <summary>
    /// Statuts est l'ensemble des statuts que peut prendre une tâche
    /// </summary>
    public enum Statuts
    {
        aFaire,
        fait,
        urgent,
        enRetard
    }

    /// <summary>
    /// StatutsMethodes comme son nom l'indique permet d'accéder à des méthodes concernant les statuts
    /// </summary>
    static class StatutsMethodes
    {
        public static string StatutsToString(Statuts stt)
        {
            switch (stt)
            {
                case Statuts.aFaire :
                    return "à faire";
                case Statuts.fait:
                    return "fait";
                case Statuts.urgent:
                    return "urgent";
                case Statuts.enRetard:
                    return "en retard";
                default:
                    throw new StatutsException("Statut inconnu.");
            }
        }
    }
}
