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
    class Square : Block
    {
        public Square(GraphicsDeviceManager gd, Vector2 co, Color color) : base(gd, co, color)
        {
        }

        private new void CreateBlock(GraphicsDeviceManager gd)
        {
            bound = new Texture2D(gd.GraphicsDevice, 30, 30);
            data = new Color[0 * 0];

            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            bound.SetData(data);
        }

    }
}
