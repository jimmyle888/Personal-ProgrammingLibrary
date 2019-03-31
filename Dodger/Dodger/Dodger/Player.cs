using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Dodger
{
    class Player
    {
        //private MouseState mouse = Mouse.GetState();
        public int x = Mouse.GetState().X;
        public int y = Mouse.GetState().Y;

        public Texture2D img;
        public Rectangle dest;

        public Player(Texture2D img)
        {
            this.img = img;
            //this.dest = dest;
        }

        public void Update()
        {
            x = Mouse.GetState().X;
            y = Mouse.GetState().Y;

            dest = new Rectangle(x, y, 25, 25);
            
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


    }
}
