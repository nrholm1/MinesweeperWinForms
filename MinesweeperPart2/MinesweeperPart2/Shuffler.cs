using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperPart2
{
    class Shuffler
    {
        private Random _rng;
        private int maxVal;
        public Shuffler(int MaxVal)
        {
            _rng = new Random();
            maxVal = MaxVal;
        }

        public void Shuffle<T>(IList<T> array)
        {
            for (int n = array.Count; n > 1; )
            { 
            int k = _rng.Next(n);
            --n;
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
            }
        }
    }
}
