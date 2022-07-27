using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    static class HoneyVault
    {
        public const float NECTAR_CONVERSION_RATIO = .19f;
        public const float LOW_LEVEL_WARNING = 10f;
        private static float honey = 25f;
        private static float nectar = 100f;

        /// <summary>
        /// Status shown at the end of each shift. May show warnings if nectar/honey level is low.
        /// </summary>
        public static string StatusReport
        {
            get
            {
                string status = $"{honey:0.0} units of honey\n" + $"{nectar:0.0} units of nectar"; // base information - how much honey and nectar there is in the beehive.

                //if honey or nectar level is low, it will add a warning to the base information
                string warnings = "";
                if (honey < LOW_LEVEL_WARNING)
                    warnings += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                if (nectar < LOW_LEVEL_WARNING)
                    warnings += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";

                return status + warnings; // return base massage with possible warnings
            }
        }


        /// <summary>
        /// Called by NectarCollector bee each shift. Collects nectar from outside the beehive into it.
        /// </summary>
        /// <param name="amount">amount of nectar to collect</param>
        public static void CollectNectar(float amount)
        {
            if (amount > 0f) nectar += amount;
        }

        /// <summary>
        /// Converts acquired nectar into honey
        /// </summary>
        /// <param name="amount">amount of nectar you want to convert into honey</param>
        public static void ConvertNectarToHoney(float amount)
        {
            float nectarToConvert = amount; //new variable for better readability
            if(nectarToConvert > nectar) nectarToConvert = nectar; // if the amount of nectar to convert is smaller than amount of nectar, then convert all of the remaining nectar
            nectar -= nectarToConvert; // subtracting the amount of nectar to convert from all of the nectar in the beehive
            honey += nectarToConvert * NECTAR_CONVERSION_RATIO; // the amount of acquired nectar is nectarToConvert multiplied by NECTAR_CONVERSION_RATIO
        }

        /// <summary>
        /// Every action requires honey to be done, this method is consuming honey due to doing another actions by bees in the beehive
        /// </summary>
        /// <param name="amount">amount of honey to be consumed due to doing an action</param>
        /// <returns></returns>
        public static bool ConsumeHoney(float amount) 
        {
            if (honey >= amount) // if there is enough honey to consume then do it and return true
            {
                honey -= amount;
                return true;
            }
            return false; // if there is not enough honey to consume, return false
        }
    }
}
