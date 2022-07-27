using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    class EggCare : Bee
    {
        public const float CARE_PROGRESS_PER_SHIFT = 0.15f; // one egg care bee is capable of growing 0.15 adult unassigned workers
        public override float CostPerShift { get { return 1.35f; } }

        private Queen queen;

        // constructor
        public EggCare(Queen queen) : base("Egg Care")
        {
            this.queen = queen;
        }

        /// <summary>
        /// Egg Care's job is to care for eggs
        /// </summary>
        protected override void DoJob()
        {
            queen.CareForEggs(CARE_PROGRESS_PER_SHIFT); //EggCare uses a reference to the Queen object to call her CareForEggs method to turn eggs into workers 
        }
    }
}
