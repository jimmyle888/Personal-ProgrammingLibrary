using System;

public class Word
{
    private string word;
    private bool[] found;

    public Word(string word)
    {
        this.word = word;
        found = new bool[word.Length];
    }


    public void MatchLetter(char guess)
    {
        for (int i = 0; i < word.Length; i++)
        {
            if (guess == word[i])
            {
                found[i] = true;
            }
        }
    }

}
