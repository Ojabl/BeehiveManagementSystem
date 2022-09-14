using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    abstract class Bee : IWorker
    {
        /// <summary>
        /// lets each Bee subclas define the amount of honey it consumes during the shift.
        /// </summary>
        public abstract float CostPerShift { get; } //if it has no setter then it is read-only(?)
        public string Job { get; private set; }

        /// <summary>
        /// Constructor for this class, takes string which it uses to set the read-only Job property.
        /// available jobs are: "Queen", "Nectar Collector", "Honey Manufacturer" and "Egg Care"
        /// </summary>
        /// <param name="job">which job to assign next bee</param>
        public Bee(string job)
        {
           Job = job;
        }

        /// <summary>
        /// Passes HoneyConsumed to the HoneyVault.ConsumeHoney method
        /// </summary>
        public void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift)) // if there is enough honey in the hive, then call DoJob method.
                DoJob();
            // if there is not, do nothing.
        }
        protected abstract void DoJob(); // will be overriden by subclasses.
    }
}
