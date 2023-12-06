using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump.Classes
{
    public class Bullet
    {
        public Physics physics;
        public Image sprite;

        public Bullet(PointF pos)
        {
            sprite = Properties.Resources.stone;
            physics = new Physics(pos, new Size(12, 12));
        }

        public void MoveUp()
        {
            physics.transform.position.Y -= 15;
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(sprite, physics.transform.position.X, physics.transform.position.Y, physics.transform.size.Width, physics.transform.size.Height);
        }
    }
}
