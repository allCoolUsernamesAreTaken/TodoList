using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListV1.Exceptions;

namespace TodoListV1.ClassesMetier
{
    public enum UnitesTemps
    {
        heure,
        jour,
        semaine,
        mois
    }

    static class UnitesTempsMethodes
    {
        public static string UniteTempsToString(UnitesTemps ut, int nb)
        {
            switch (ut)
            {
                case UnitesTemps.heure:
                    return (nb == 1) ? "heure" : "heures" ;
                case UnitesTemps.jour:
                    return (nb == 1) ? "jour": "jours";
                case UnitesTemps.semaine:
                    return (nb == 1) ? "semaine" : "semaines" ;
                case UnitesTemps.mois:
                    return "mois";
                default:
                    throw new UnitesTempsException("Unité de temps inconnue.");
            }
        }
    }
}
