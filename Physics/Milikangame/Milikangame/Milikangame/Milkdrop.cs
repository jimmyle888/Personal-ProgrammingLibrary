using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Milikangame
{
    class Milkdrop
    {
        private int xPos;
        private int yPos;
        private int speed;
        private int dir;

        private const int UP = 1;
        private const int DOWN = 2;

        private Texture2D img;
        private Rectangle bounds;
        private Rectangle src;
        

        public Milkdrop(Texture2D img, Rectangle bounds, Rectangle src, int xPos, int yPos, int speed, int dir)
        {
            this.img = img;
            this.bounds = bounds;
            this.xPos = xPos;
            this.yPos = yPos;
            this.speed = speed;
            this.dir = dir;
        }

        public void Move()
        {
            if (dir == UP)
            {
                yPos -= speed;
            }
            else if (dir == DOWN)
            {
                yPos += speed;
            }
            else
            {
                dir = UP;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, bounds, src, Color.White);
        }
    }
}
