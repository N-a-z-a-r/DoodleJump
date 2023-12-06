using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Enemy : Player
    {
        public Enemy(PointF pos, int type)
        {
            switch (type)
            {
                case 1:
                    sprite = Properties.Resources.monster3;
                    physics = new Physics(pos, new Size(40, 40));
                    break;
                case 2:
                    sprite = Properties.Resources.monster1;
                    physics = new Physics(pos, new Size(60, 50));
                    break;
                case 3:
                    sprite = Properties.Resources.monster2;
                    physics = new Physics(pos, new Size(50, 40));
                    break;
            }
        }
    }
}
