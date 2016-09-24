using System;
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

    #endregion

    public static void Main(string[] args)
    {
      if(Initialize())
      {
        var keyScores = new Dictionary<byte, int>();

        var keyOutput = new Dictionary<byte, string>();

        for(var idx = byte.MinValue; idx < byte.MaxValue; idx++)
        {
          var xorOutput = XOR.XORInputToByte(xord, idx, false);

          keyOutput.Add(idx, xorOutput);

          keyScores.Add(idx, Frequency.ScoreFrequencies(xorOutput));
        }

        var topScores = keyScores.OrderByDescending(x => x.Value).Take(1);

        System.Console.WriteLine("Top 1 scores:\n{0}", String.Join(Environment.NewLine, topScores));

        foreach(var key in topScores.Select(x => x.Key))
        {
          System.Console.WriteLine("{0} output: {1}", key, keyOutput[key]);
        }
      }
    }

    public static bool Initialize()
    {
      if(String.Equals(Converter.ConvertHexToBase64(HexToConvert), HexConvertTarget))
      {
        System.Console.WriteLine("TEST: Converting hex to base64 successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: Converting hex to base64 failed! Exiting...");
        return false;
      }

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

      return true;
    }
  }
}