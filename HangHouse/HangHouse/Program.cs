using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HangHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Word> words = new List<Word>();
            string cont;

            try
            {
                using (StreamReader sr = new StreamReader("d:/bitchtits.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        words.Add(new Word(line));
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Oh Jimmy you fucked up again");
                Console.WriteLine(e.Message);
            }

            //for (int i = 0; i < words.Count; i++)
            //{
            //    Console.WriteLine(words[i] + "   " + i);
            //}

            for (int i = 0; i < words.Count; i++)
            {
                bool loop = true;
                char guess;
                char[] guessed = words[i].GetGuessedLetters();
                House house = new House();
                bool r;

                while (loop)
                {
                    words[i].WriteWord();

                    Console.WriteLine("\n\nGussed letters: ");
                    foreach (char c in guessed)
                    {
                        Console.Write(c + " ");
                    }
                    Console.WriteLine("");

                    house.DrawHouse();

                    Console.WriteLine("\nGuess a letter: ");


                    try
                    {
                        guess = Convert.ToChar(Console.ReadLine());
                        r = words[i].MatchLetter(guess);

                        if (!r)
                        {
                            house.Increment();
                        }


                        guessed = words[i].GetGuessedLetters();


                        Console.WriteLine("\n");

                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error! " + e + " Try again with a single character");
                    }


                    if (house.GetStrikes() == 7)
                    {
                        Console.WriteLine("You lose!! The word was " + words[i].GetWord() + " Better luck on the next Word!");
                        loop = false;
                    }
                    else if (words[i].Complete())
                    {
                        Console.WriteLine("You win!! Lets do the next word!");
                    }
                }
            }

            

            Console.ReadKey();
        }

    }
}
