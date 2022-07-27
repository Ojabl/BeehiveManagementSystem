using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    class Queen : Bee
    {
        public const float EGGS_PER_SHIFT = 0.45f; //Queen bee is making 0.45 egg per shift
        public const float HONEY_PER_UNASSIGNED_WORKER = 0.5f; // unassigned bee worker is consuming 0.5 honey per shift

        private Bee[] workers = new Bee[0]; // declaring new, empty array of bee workers, now it'e length is equal to 0, but can be extended by AddWorker()
        private float eggs = 0; // indicates how many eggs beehive has at the start
        private float unassignedWorkers = 3; // indicates how many bees are in the beehive

        public string StatusReport { get; private set; }
        public override float CostPerShift { get { return 2.15f; } } // Queen bee is consuming 2.15 honey per shift

        //constructor
        //calls 3 unassigned bee workers to work, one for each position
        public Queen() : base("Queen")
        {
            AssignBee("Nectar Collector");
            AssignBee("Honey Manufacturer");
            AssignBee("Egg Care");
        }
        
        /// <summary>
        /// Expand the workers array by one slot and add a Bee reference.
        /// </summary>
        /// <param name="worker">Worker to add to the workers array.</param>
        private void AddWorker(Bee worker)
        {
            if(unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }

        /// <summary>
        /// Updates StatusReport (which contains complete information about beehive), is called after any changes are made to beehive
        /// </summary>
        public void UpdateStatusReport()
        {
            StatusReport = $"Vault report:\n{HoneyVault.StatusReport}\n" +
            $"\nEgg Count: {eggs:0.0}\nUnassigned workers: {unassignedWorkers:0.0}\n" +
            $"{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}" +
            $"\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";
        }

        /// <summary>
        /// Called by EggCare bees, converts eggs into unassignedWorkers
        /// </summary>
        /// <param name="eggsToConvert">Number of eggs to convert into bees</param>
        public void CareForEggs(float eggsToConvert)
        {
            if(eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }

        /// <summary>
        /// Returns info about current worker status of every bee
        /// </summary>
        /// <param name="job">one of 4 available jobs: "queen", "Honey Manufacturer", "Nectar Collector" or "Egg Care"</param>
        /// <returns></returns>
        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (Bee worker in workers)
                if (worker.Job == job) count++;
            string s = "s";
            if (count == 1) s = "";
            return $"{count} {job} bee{s}";
        }

        /// <summary>
        /// Used to assign unassigned bees to one of 3 jobs. Calls AddWorker()
        /// </summary>
        /// <param name="job">Honey Manufacturer / Nectar Collector / Egg Care</param>
        public void AssignBee(string job)
        {
            switch(job)
            {
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
            }
            UpdateStatusReport();
        }

        /// <summary>
        /// Makes bees work their jobs
        /// </summary>
        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT; //Queen is lying eggs
            foreach(Bee worker in workers) // makes every worker work his shift
            {
                worker.WorkTheNextShift();
            }
            HoneyVault.ConsumeHoney(unassignedWorkers * HONEY_PER_UNASSIGNED_WORKER); // unassigned workers are consuming honey
            UpdateStatusReport(); // changes were made, update StatusReport
        }
    }
}
