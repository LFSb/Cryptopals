using System;
using System.IO;
using System.Text;
using System.Linq;

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

    public static Int32 CalculateHammingDistance(byte[] input1, byte[] input2)
    {
      var score = 0;

      if(input1.Length != input2.Length)
      {
        throw new InvalidDataException("Input lengths are not equal. Aborting...");
      }

      for(var idx = 0; idx < input1.Length; idx++)
      {
        var val = input1[idx] ^ input2[idx];

        while(val != 0)
        {
          score++;
          val &= val -1;
        }
      }

      return score;
    }
  }
}