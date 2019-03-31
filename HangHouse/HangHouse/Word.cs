using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Word
{
    private string word;
    private bool[] found;
    List<char> guessed = new List<char>();


    public Word(string word)
    {
        this.word = word;
        found = new bool[word.Length];
    }


    public bool MatchLetter(char guess)
    {

        bool result = false;

        for (int i = 0; i < word.Length; i++)
        {
            if (guess == word[i] && found[i] != true)
            {
                found[i] = true;
                result = true;
            }
        }

        AddGuessedLetters(guess);

        return result;

    }


    public string GetWord()
    {
        return word;
    }

    public bool[] GetFoundIndex()
    {
        return found;
    }

    public char[] GetGuessedLetters()
    {
        return guessed.ToArray();
    }

    public void AddGuessedLetters(char guess)
    {
        if (!guessed.Contains(guess))
        {
            guessed.Add(guess);
        }
    }


    public void WriteWord()
    {
        Console.WriteLine("\nYour Word To Guess:");
        for (int i = 0; i < word.Length; i++)
        {
            if (found[i] == true)
            {
                Console.Write(word[i]);
            }
            else
            {
                Console.Write("_");
            }
        }
    }

    public bool Complete()
    {
        if (found.All(x => x))
        {
            return true;
        }
        return false;
    }

}
