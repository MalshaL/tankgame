using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    class LifePack : GridEntity
    {
        public LifePack(Point p, int lifeTime, int appearTime)
            : base(p, lifeTime, appearTime)
        {

        }
    }
}
