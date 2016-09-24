using System;
using System.IO;

namespace ConsoleApplication.Helpers
{
  public static class Frequency
  {
    private const string etaoinshrdlu = "etaoinshrdlumwfgypbvkjxqz";

    //Score the input string based on the occurrance of the most frequently occurring letters in the english language.
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

    //Calculate the Hamming Distance, bitwise.
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
          val &= val - 1;
        }
      }

      return score;
    }
  }
}