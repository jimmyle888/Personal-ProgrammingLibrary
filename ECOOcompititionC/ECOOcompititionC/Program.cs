using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOOcompititionC
{
    class Program
    {
        static void Main(string[] args)
        {
            int W = Convert.ToInt32(Console.ReadLine());
            int H = Convert.ToInt32(Console.ReadLine());
            int w = Convert.ToInt32(Console.ReadLine());
            int h = Convert.ToInt32(Console.ReadLine());
            int s = Convert.ToInt32(Console.ReadLine());

            int xL = w;
            int g = 1 + w;
            int yL = 0;

            bool moveLeft = false;
            bool moveRight = true;
            bool moveDown = false;
            bool moveUp = false;

            //When space is true you cant move there

            bool[,] a = new bool[W, H];

            for (int i = 0; i < w; i++)
            {
                for (int n = 0; n < h; n++)
                {
                    a[i, n] = true;
                    a[i, H - n - 1] = true;
                    a[W - i - 1, n] = true;
                    a[W - i - 1, H - n - 1] = true;
                    
                }
            }

            //for (int i = 0; i < s; i++)
            //{
            //    if (moveLeft && (xL + 1 < W) && (!a[xL + 1, yL] || (yL - 1 < 0))) //can move
            //    {
            //        xL++;
            //    }
            //    else if (moveLeft && (!(xL + 1 < W) || a[xL + 1, yL])) //cant move
            //    {
            //        yL++;
            //        moveLeft = false;
            //        moveDown = true;
            //    }
            //    else if (moveDown && (yL + 1 < H) && !a[xL, yL + 1]) //can move
            //    {
            //        yL++;
            //    }
            //    else if (moveDown && (!(yL+1 < H) || !a[xL, yL+ 1])) //cant move
            //    {
            //        xL--;
            //        moveDown = false;
            //        moveRight = true;
            //    }
            //}

            //for (int i = 0; i < s; i++)
            //{
            //    if (yL - 1 < 0 || (xL + 1 < W && a[xL + 1, yL] == false)) //can move
            //    {
            //        a[xL, yL] = true;
            //        xL++; //right
            //    }
            //    else if ((xL + 1 == W) || (yL + 1 < H && a[xL, yL + 1] == false))
            //    {
            //        a[xL, yL] = true;
            //        yL++; //down
            //    }
            //    else if ((yL + 1 == H) || (xL - 1 > 0 && a[xL - 1, yL] == false))
            //    {
            //        a[xL, yL] = true;
            //        xL--; //left
            //    }
            //    else if ((xL - 1 > 0) || (yL - 1 > 0 && a[xL, yL - 1] == false))
            //    {
            //        a[xL, yL] = true;
            //        yL--;
            //        //up
            //    }

            for (int i = 0; i < s; i++)
            {
                if (yL - 1 < 0 && xL != W - w - 1) //can move
                {
                    a[xL, yL] = true;
                    xL++; //right
                }
                else if (xL + 1 > W - 1 && yL != H - h - 1)
                {
                    a[xL, yL] = true;
                    yL++; //down
                }
                else if (yL + 1 > H - 1 && xL != w - 1)
                {
                    a[xL, yL] = true;
                    xL--; //left
                }
                else if (xL - 1 < 0 && yL != h - 1)
                {
                    a[xL, yL] = true;
                    yL--;
                    //up
                }
                else if (yL - 1 > 0 && a[xL, yL - 1] == true && xL + 1 <= W - 1)
                {
                    a[xL, yL] = true;
                    xL++; //right
                }
                else if (xL + 1 < W - 1 && a[xL + 1, yL] == true && yL + 1 <= H - 1)
                {
                    a[xL, yL] = true;
                    yL++; //down
                }
                else if (yL + 1 < H - 1 && a[xL, yL + 1] == true && xL - 1 >= 0)
                {
                    a[xL, yL] = true;
                    xL--; //left
                }
                else if (xL - 1 > 0 && a[xL - 1, yL] == true)
                {
                    a[xL, yL] = true;
                    yL--;
                    //up
                }
            }

            //xL++;
            //yL++;
            

            Console.WriteLine(xL);
            Console.WriteLine(yL);
            Console.ReadLine();



        }

    }
}
