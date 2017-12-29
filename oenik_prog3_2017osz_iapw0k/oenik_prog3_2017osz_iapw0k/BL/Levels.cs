namespace Oenik_prog3_2017osz_iapw0k
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Levels
    {
        public Levels()
        {
            this.Level = 1;
            this.GetNrofColors();
        }

        public int Level { get; set; }

        public int NrofColors { get; private set; }

        public void GetNrofColors()
        {
            if (this.Level < 3)
            {
                this.NrofColors = 3;
            }
            else if (this.Level < 5)
            {
                this.NrofColors = 4;
            }
            else if (this.Level < 7)
            {
                this.NrofColors = 5;
            }
            else if (this.Level < 9)
            {
                this.NrofColors = 6;
            }
            else
            {
                this.NrofColors = 7;
            }
        }

        public void LevelUp()
        {
            this.Level++;
            this.GetNrofColors();
        }

        public void ResetLevel()
        {
            this.Level = 1;
            this.GetNrofColors();
        }
    }
}
