namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Levels
    {
        public Levels()
        {
            this.Level = 1;
            this.GetNrofColors();
            this.NrofRounds = 5;
        }

        public int Level { get; set; }

        public int NrofColors { get; private set; }

        public int NrofRounds { get; private set; }

        public void LevelUp()
        {
            this.Level++;
            this.GetNrofColors();
            this.GetNrofRounds();
        }

        public void ResetLevel()
        {
            this.Level = 1;
            this.GetNrofColors();
            this.NrofRounds = 5;
        }

        private void GetNrofColors()
        {
            if (this.Level < 5)
            {
                this.NrofColors = 3;
            }
            else if (this.Level < 9)
            {
                this.NrofColors = 4;
            }
            else if (this.Level < 13)
            {
                this.NrofColors = 5;
            }
            else if (this.Level < 17)
            {
                this.NrofColors = 6;
            }
            else
            {
                this.NrofColors = 7;
            }
        }

        private void GetNrofRounds()
        {
            if (this.Level < 17 && this.NrofColors > 2)
            {
                if (this.Level % 5 == 0)
                {
                    this.NrofRounds = 5;
                }
                else
                {
                    this.NrofRounds--;
                }
            }
        }
   }
}
