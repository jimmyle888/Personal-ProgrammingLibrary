using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOOcompCC
{
    class Program
    {
        static int W;
        static int H;
        static int w;
        static int h;
        static int s;
        static bool[,] a;
        

        static void Main(string[] args)
        {
            W = Convert.ToInt32(Console.ReadLine()) - 1;
            H = Convert.ToInt32(Console.ReadLine()) - 1;
            w = Convert.ToInt32(Console.ReadLine());
            h = Convert.ToInt32(Console.ReadLine());
            s = Convert.ToInt32(Console.ReadLine());

            a = new bool[W + 1, H + 1];

            var tuple = new Tuple<int, int>(0, 0);

            for (int i = 0; i < w; i++)
            {
                for (int n = 0; n < h; n++)
                {
                    a[i, n] = true;
                    a[i, H - n] = true;
                    a[W - i, n] = true;
                    a[W - i, H - n] = true;

                }

            }

            tuple = FindL(s, w, 0);

            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item2);
            Console.ReadLine();
            
        }

        static private Tuple<int, int> FindL(int count, int xL, int yL)
        {
            if (count < 1)
            {
                return Tuple.Create(xL + 1, yL + 1);
            }
            else
            {
                //right
                if ((yL - 1 < 0 && xL != W - w) || (yL - 1 >= 0 && a[xL, yL - 1] == true && xL + 1 <= W && a[xL + 1, yL] == false))
                {
                    if (a[xL + 1, yL] == false)
                    {
                        a[xL, yL] = true;
                        return FindL(count - 1, xL + 1, yL);
                    }
                    else
                    {
                        return Tuple.Create(-1, -1);

                    }
                }
                //down
                else if((xL + 1 > W && yL != H - h) || (xL + 1 <= W && a[xL + 1, yL] == true && yL + 1 <= H && a[xL, yL + 1] == false))
                    
                {
                    if (a[xL, yL + 1] == false)
                    {
                        a[xL, yL] = true;
                        return FindL(count - 1, xL, yL + 1);
                    }
                    else
                    {
                        return Tuple.Create(-1, -1);

                    }
                }
                //left
                else if ((yL + 1 > H && xL != w) || (yL + 1 <= H && a[xL, yL + 1] == true && xL - 1 >= 0 && a[xL - 1, yL] == false))
                {
                    if (a[xL - 1, yL] == false)
                    {
                        a[xL, yL] = true;
                        return FindL(count - 1, xL - 1, yL);
                    }
                    else
                    {
                        return Tuple.Create(-1, -1);

                    }
                }
                //up
                else if((xL - 1 < 0 && yL != h) || (xL - 1 >= 0 && a[xL - 1, yL] == true && yL - 1 >= 0 && a[xL, yL - 1] == false))
                    
                {
                    if (a[xL, yL - 1] == false)
                    {
                        a[xL, yL] = true;
                        return FindL(count - 1, xL, yL - 1);
                    }
                    else
                    {
                        return Tuple.Create(-1, -1);

                    }

                }
                else
                {
                    return Tuple.Create(-1, -1);
                }
            }
        }

        
    }
}
