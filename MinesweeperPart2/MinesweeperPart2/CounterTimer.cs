using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperPart2
{
    class CounterTimer : System.Windows.Forms.Timer
    {
        public CounterTimer()
        {
            this.Interval = 1000;
            this.seconds = 0;
        }

        private int seconds;
        public int Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }

        public void IncrementTimer()
        {
            seconds += this.Interval / 1000;
        }

        public string GetUITime()
        {
            return (this.Seconds % 60) < 10 ? (this.Seconds / 60).ToString() + ":0" + (this.Seconds % 60).ToString() : (this.Seconds / 60).ToString() + ":" + (this.Seconds % 60).ToString();
        }
    }
}
