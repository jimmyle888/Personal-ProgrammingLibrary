using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangHouse
{
    class House
    {
        private byte strikes = 0;

        public House()
        {
        }

        public void DrawHouse()
        {
            switch (strikes)
            {
                case 1:
                    Console.WriteLine(" _________     ");
                    Console.WriteLine( "|         |    ");
                    Console.WriteLine( "|         0    ");
                    Console.WriteLine( "|          ");
                    Console.WriteLine( "|          ");
                    Console.WriteLine( "|              ");
                    Console.WriteLine( "|              ");
                    break;
                case 2:
                    Console.WriteLine(" _________     ");
                    Console.WriteLine("|         |    ");
                    Console.WriteLine("|         0    ");
                    Console.WriteLine("|         |  ");
                    Console.WriteLine("|           ");
                    Console.WriteLine("|              ");
                    Console.WriteLine("|              ");
                    break;
                case 3:
                    Console.WriteLine(" _________     ");
                    Console.WriteLine("|         |    ");
                    Console.WriteLine("|         0    ");
                    Console.WriteLine("|        /|  ");
                    Console.WriteLine("|          ");
                    Console.WriteLine("|              ");
                    Console.WriteLine("|              ");
                    break;
                case 4:
                    Console.WriteLine(" _________     ");
                    Console.WriteLine("|         |    ");
                    Console.WriteLine("|         0    ");
                    Console.WriteLine("|        /|\\  ");
                    Console.WriteLine("|          ");
                    Console.WriteLine("|              ");
                    Console.WriteLine("|              ");
                    break;
                case 5:
                    Console.WriteLine(" _________     ");
                    Console.WriteLine("|         |    ");
                    Console.WriteLine("|         0    ");
                    Console.WriteLine("|        /|\\  ");
                    Console.WriteLine("|        /  ");
                    Console.WriteLine("|              ");
                    Console.WriteLine("|              ");
                    break;
                case 6:
                    Console.WriteLine(" _________     ");
                    Console.WriteLine("|         |    ");
                    Console.WriteLine("|         0    ");
                    Console.WriteLine("|        /|\\  ");
                    Console.WriteLine("|        / \\  ");
                    Console.WriteLine("|              ");
                    Console.WriteLine("|              ");
                    break;
                default:
                    Console.WriteLine(" _________     ");
                    Console.WriteLine("|         |    ");
                    Console.WriteLine("|             ");
                    Console.WriteLine("|          ");
                    Console.WriteLine("|          ");
                    Console.WriteLine("|              ");
                    Console.WriteLine("|              ");
                    break;
            }
        }

        public void Increment()
        {
            strikes++;
        }

        public byte GetStrikes()
        {
            return strikes;
        }
    }
}
