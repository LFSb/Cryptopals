using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication.Helpers
{
  public static class Frequency
  {
    private const string etaoinshrdlu = "etaoinshrdlumwfgypbvkjxqz";

    //Takes a byte array as an input, and proceeds to score the output of xor'ing said input on a random byte. The score, and the output of each byte will be returned.
    public static Dictionary<byte, int> ScoreInput(byte[] input, out Dictionary<byte, string> byteOutput)
    {
      var byteScores = new Dictionary<byte, int>();

      byteOutput = new Dictionary<byte, string>();

      for(var idx = byte.MinValue; idx < byte.MaxValue; idx++)
      {
        var xorOutput = XOR.XORInputToByte(input, idx);

        byteOutput.Add(idx, xorOutput);

        byteScores.Add(idx, Frequency.ScoreFrequencies(xorOutput));
      }

      return byteScores; 
    }

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