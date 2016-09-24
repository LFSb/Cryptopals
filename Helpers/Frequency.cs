using System;

namespace ConsoleApplication.Helpers
{
  public static class Frequency
  {
    private const string etaoinshrdlu = "etaoinshrdlumwfgypbvkjxqz";

    public static Int32 ScoreFrequencies(string input)
    {
      var score = 0;

      foreach(var character in input)
      {
        if(char.IsLetter(character))
        {
          if(etaoinshrdlu.Contains(character.ToString()))
          {
            score += etaoinshrdlu.Length - etaoinshrdlu.IndexOf(character);
          }
          else
          {
            score++;
          }
        }
        else
        {
          score--;
        }    
      }

      return score;
    }
  }
}