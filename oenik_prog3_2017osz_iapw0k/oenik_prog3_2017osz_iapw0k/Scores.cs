using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oenik_prog3_2017osz_iapw0k
{
    public class Scores
    {
        private static int point = 10;

        public Scores()
        {
            this.Score = 0;
        }

        public int Score { get; set; }

        public void AddtoScore()
        {
            this.Score += point;
        }
    }
}
