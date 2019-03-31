using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DodgerMonoGamePort
{
    class Enemy
    {
        public Texture2D img;
        public Rectangle dest;

        private int length;
        private int rand;
        GraphicsDevice gd;

        Random rng = new Random();

        public Enemy(Texture2D img, GraphicsDevice gd)
        {
            this.img = img;
            this.gd = gd;

            rand = rng.Next(1, 4);
            int x = rng.Next(1, gd.DisplayMode.Width - 100);

            switch (rand)
            {
                case 1:
                    dest = new Rectangle(x, 0, 20, 20);
                    break;
                case 2:
                    dest = new Rectangle(x, 0, 30, 30);
                    break;
                case 3:
                    dest = new Rectangle(x, 0, 40, 40);
                    break;
                default:
                    dest = new Rectangle(x, 0, 10, 10);
                    break;
            }

        }

        public void Update()
        {
            dest.Y += (int)1;
            Respawn();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, dest, Color.White);
        }

        public bool CheckCollsion(Rectangle rect)
        {
            if (dest.X < rect.X + rect.Width
                && dest.X + dest.Width > rect.X
                && dest.Y < rect.Y + rect.Height
                && dest.Height + dest.Y > rect.Y)
            {
                return true;
            }

            return false;
        }

        private void Respawn()
        {
            if (dest.Y > 100)
            {
                dest.X = rng.Next(1, gd.DisplayMode.Width - 100);
                dest.Y = 0;
            }
        }
    }
}
