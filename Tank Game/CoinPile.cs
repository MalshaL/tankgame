using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    class CoinPile : GridEntity
    {
        private int value = -1;

        public CoinPile(Point p, int lifeTime, int appearTime, int value)
            : base(p, lifeTime, appearTime)
        {
            this.setValue(value);
        }

        public int getValue()
        {
            return value;
        }

        public void setValue(int value)
        {
            this.value = value;
        }
    }
}
