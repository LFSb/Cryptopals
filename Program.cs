using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ConsoleApplication.Helpers;

namespace ConsoleApplication
{
  public class Program
  {
    #region Challenge 1: Hex Conversion
    private const string HexConvertTarget = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

    private const string HexToConvert = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";

    #endregion

    #region Challenge 2: XOR

    private const string ExpectedXor = "746865206b696420646f6e277420706c6179"; 

    private const string Xor1 = "1c0111001f010100061a024b53535009181c";

    private const string Xor2 = "686974207468652062756c6c277320657965";

    #endregion

    #region Challenge 3: Single-byte XOR Cipher

    private const string xord = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";

    private const string ex3ExpectedAnswer = "Cooking MC's like a pound of bacon";

    #endregion

    #region Challenge 4: Detect single-character XOR

    private const string ex4ExpectedAnswer = "Now that the party is jumping\n";

    #endregion

    public static void Main(string[] args)
    {
      if(Initialize())
      {
        
      }
    }

    public static bool Initialize()
    {

      //#1 https://cryptopals.com/sets/1/challenges/1
      if(String.Equals(Converter.ConvertHexToBase64(HexToConvert), HexConvertTarget))
      {
        System.Console.WriteLine("TEST: Converting hex to base64 successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: Converting hex to base64 failed! Exiting...");
        return false;
      }

      //#2 https://cryptopals.com/sets/1/challenges/2
      var output = XOR.XOREqualLengthInputs(Xor1, Xor2);

      if(String.Equals(output, ExpectedXor))
      {
        System.Console.WriteLine("TEST: XOR Successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: XOR failed! Output: {0}", output);
        return false;
      }

      //#3 https://cryptopals.com/sets/1/challenges/3
      var keyScores = new Dictionary<byte, int>();

      var keyOutput = new Dictionary<byte, string>();

      for(var idx = byte.MinValue; idx < byte.MaxValue; idx++)
      {
        var xorOutput = XOR.XORInputToByte(xord, idx, false);

        keyOutput.Add(idx, xorOutput);

        keyScores.Add(idx, Frequency.ScoreFrequencies(xorOutput));
      }

      var topScores = keyScores.OrderByDescending(x => x.Value).First();

      var topOutput = keyOutput[topScores.Key];

      if(String.Equals(topOutput,ex3ExpectedAnswer))
      {
        System.Console.WriteLine("TEST: Single byte XOR successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: Single byte XOR unsuccesful, output was {0}", topOutput);
      }

      //#4 https://cryptopals.com/sets/1/challenges/4

      var lines = File.ReadLines("TestFiles/4.txt");

      var lineScores = new Dictionary<int, KeyValuePair<int, string>>();

      var lineNr = 0;

      foreach(var line in lines)
      {
        var byteScores = new Dictionary<byte, int>();

        var byteOutput = new Dictionary<byte, string>();

        for(var idx = byte.MinValue; idx < byte.MaxValue; idx++)
        {
          var xorOutput = XOR.XORInputToByte(line, idx, false);

          byteOutput.Add(idx, xorOutput);

          byteScores.Add(idx, Frequency.ScoreFrequencies(xorOutput));
        }

        var highestScore = byteScores.OrderByDescending(x => x.Value).First();

        lineScores.Add(lineNr, new KeyValuePair<int, string>(highestScore.Value, byteOutput[highestScore.Key]));

        lineNr++;
      }

      var highestScoringLine = lineScores.OrderByDescending(x => x.Value.Key).First();
      
      if(String.Equals(highestScoringLine.Value.Value, ex4ExpectedAnswer))
      {
        System.Console.WriteLine("TEST: Detect single character XOR successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: Detect single character XOR not successful, output was: {0}", highestScoringLine.Value.Value);
        return false;
      }

      return true;
    }
  }
}