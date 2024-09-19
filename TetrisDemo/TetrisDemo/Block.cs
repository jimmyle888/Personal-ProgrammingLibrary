using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TetrisDemo
{
    class Block
    {
        protected Texture2D bound;
        protected Vector2 co;
        protected Color[] data;
        protected Color color;
        protected int timer = 0;

        public Block(GraphicsDeviceManager gd, Vector2 co, Color color)
        {
            this.co = co;
            this.color = color;
            CreateBlock(gd);      

        }

        public void Update()
        {
            timer++;

            if (timer == 60)
            {
                co.Y += 40;
                timer = 0;
            }

            
        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(bound, co, color * 0.5f);
        }

        public virtual void CreateBlock(GraphicsDeviceManager gd)
        {
            bound = new Texture2D(gd.GraphicsDevice, 10, 10);
            data = new Color[10 * 10];

            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            bound.SetData(data);
        }
    }
}
