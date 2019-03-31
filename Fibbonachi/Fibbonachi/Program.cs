using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibbonachi
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initilize variables we will need
            int size = 0;          //Size of sequence
            string choice = "Y";   //The user's choice. We don't need to initilize as Y, but I just put it here for debugging puposes
            int[] seq;             //The array that holds the sequence
            string dummy = "";     //Dummy variable to keep outer loop running

            //This loop runs continually until the user writes they do not want to continue. There are multiple break statements to break out of this loop
            while (dummy == "") 
            {
                //Reset the size and choice variables. Note here we HAVE to reset the variables to 0 and Y respectively
                //   This is because when we run the loop again without setting the variables to 0 and Y
                //   The loop will run continously
                size = 0;
                choice = "Y";

                //The loop has to be entered due to us setting the size to 0. This loop's purpose is to deal with the user
                //  potentially putting in the wrong SIZE.
                while (!(size >= 1))
                {
                    //Prompt and store for the size of the sequence
                    Console.WriteLine("Enter the size of the Fibbonacci Sequence:");
                    size = Convert.ToInt32(Console.ReadLine());

                    //When the size is not within the right limits, tell the user they have made an error
                    if (!(size >= 1))
                    {
                        Console.WriteLine("Error: Fibbonacci Sequence must have size >= 1");
                    }
                    else //Otherwise, break out of this loop. There is no need to stay in it if the user has given a correct size
                    {
                        break;
                    }

                    //Prompt and store whether or not the user wants to stay inside the loop.
                    Console.WriteLine("Would you like to continue? (Y/N)");
                    choice = Console.ReadLine();

                    //If they choose not the stay within the loop, then break out of it.
                    if (choice == "N")
                    {
                        break;
                    }
                }

                //This part is why we reset the choice variable in line 26. If we choose to stop running the program in line 51-54,
                //  Then we must also break out of this outer loop
                if (choice == "N")
                {
                    break;
                }

                //We now initilize the blank array with the users VALID size. Remember, if they put in a invalid size,
                // our program would've kept them in the inner while loop above.
                seq = new int[size];

                //Special cases of the sequence: If the user has sequence of size 1 and 2, the only values of the sequence are <1,1>
                if (size == 1)
                {
                    seq[0] = 1;
                }
                else if (size == 2)
                {
                    seq[0] = 1;
                    seq[1] = 1;
                }
                else //Otherwise, proceed as normal
                {
                    //Set the first 2 values as 1
                    seq[0] = 1;
                    seq[1] = 1;

                    for (int i = 2; i < size; i++)
                    {
                        //For every value past the 3rd element, add the previous 2 values. WE start at the 3rd element
                        //   because we already defind the first 2. Furthermore if  we were to start before there, we would get an error
                        seq[i] = seq[i - 1] + seq[i - 2];
                    }
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////OUTPUT////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.WriteLine("Fibbonacci sequence of size " + size + ":");
                Console.Write("<");

                for (int i = 0; i < size; i++)
                {
                    if (i != size - 1)
                    {
                        Console.Write(seq[i] + ", ");
                    }
                    else
                    {
                        Console.Write(seq[i] + ">\n");
                    }

                }

                Console.WriteLine("Would you like to continue? (Y/N)");
                choice = Console.ReadLine();
                if (choice == "N")
                {
                    break;
                }
            }

            Console.WriteLine("Bye!");

            Console.ReadLine();


            
        }
    }
}
