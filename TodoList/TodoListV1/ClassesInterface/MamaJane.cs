using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListV1.ClassesInterface
{
    /// <summary>
    /// MamaJane est la classe statique de gestion de l'interface.
    /// Elle regroupe les méthodes et attributs communs aux différents éléments qui ne concernent pas la communication avec le modèle.
    /// </summary>
    public static class MamaJane
    {
        /// <summary>
        /// Sert à obtenir des nombres ronds et simplifiés d'heures et de minutes correspondants à un entier donné.
        /// </summary>
        /// <param name="dr">Entier qui sera convertit selon une échelle en tableau d'entiers</param>
        /// <returns>Un tableau d'entiers {heures, minutes}</returns>
        public static int[] CalculDuree(int dr) 
        {
            // Réglages de la granularité du slider
            int[] hrsMns = { 0, 0 };
            if (dr < 12) // Granularité de 5 mn en-dessous d'une heure
            {
                hrsMns[0] = 0;
                hrsMns[1] = dr * 5;
            }
            else if (dr >= 12 && dr < 24) // Passage à 15 mn entre 1 et 4h
            {
                hrsMns[0] = (int)(1 + (dr - 12) / 4);
                hrsMns[1] = (int)(((dr - 12) % 4) * 15);
            }
            else if (dr >= 24 && dr < 32) // Passage à demi-heures entre 4 et 8h
            {
                hrsMns[0] = (int)(4 + (dr - 24) / 2);
                hrsMns[1] = (int)(((dr - 24) % 2) * 30);
            }
            else // Passage à heures au-delà de 8h
            {
                hrsMns[0] = 8 + (dr - 32);
                hrsMns[1] = 0;
            }
            return hrsMns;
        }

    }
}
