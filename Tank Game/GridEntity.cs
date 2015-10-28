using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    public class GridEntity
    {
        private Point position;
        private int lifeTime = -1;
        private int appearTime = -1;
        private int disappearTime = -1;

        public GridEntity(Point p, int lifeTime, int appearTime)
        {
            this.position = p;
            this.setLifeTime(lifeTime);
            this.setAppearTime(appearTime);
            this.setDisappearTime(appearTime, lifeTime);
        }

        public void setPosition(Point value)
        {
            this.position = value;
        }

        public Point getPosition()
        {
            return position;
        }

        public void setLifeTime(int value)
        {
            this.lifeTime = value;
        }

        public int getLifeTime()
        {
            return lifeTime;
        }

        public int getAppearTime()
        {
            return appearTime;
        }

        public void setAppearTime(int value)
        {
            this.appearTime = value;
        }

        public int getDisappearTime()
        {
            return disappearTime;
        }

        public void setDisappearTime(int appearTime, int lifeTime)
        {
            this.disappearTime = appearTime + lifeTime;
        }


    }
}
