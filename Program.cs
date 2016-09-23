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
      if(String.Equals(Converter.ConvertHexToBase64(HexToConvert), HexConvertTarget))
      {
        System.Console.WriteLine("Converting hex to base64 successful!");
      }
      else
      {
        System.Console.WriteLine("Converting hex to base64 failed! Exiting...");
        return;
      }

      var output = XOR.XOREqualLengthInputs(Converter.ConvertHexToBase64(Xor1), Converter.ConvertHexToBase64(Xor2));

      if(String.Equals(output, ExpectedXor))
      {
        System.Console.WriteLine("XOR Successful!");
      }
      else
      {
        System.Console.WriteLine("XOR failed! Output: {0}", output);
      }

      var keyFrequencies = new Dictionary<char, Dictionary<char,int>>();

      for(var idx = char.MinValue; idx < char.MaxValue; idx++)
      {
        var frequency = new Dictionary<char,int>{
                {'e',0},
                {'t',0},
                {'a',0},
                {'o',0},
                {'i',0},
                {'n',0},
                {'s',0},
                {'h',0},
                {'r',0},
                {'d',0},
                {'l',0},
                {'u',0}
              };

        if(char.IsLetterOrDigit(idx) && idx.IsHex())
        {
          System.Console.WriteLine("XOR'ing using key {0}", idx);

          var xorOutput = XOR.XOREqualLengthInputs(Converter.ConvertHexToBase64(xord), Converter.ConvertHexToBase64(new String(idx , xord.Length)));

          foreach(var character in xorOutput)
          {
            if(frequency.ContainsKey(character))
            {
              frequency[character]++;
            }
          }

          keyFrequencies.Add(idx, frequency);

          System.Console.WriteLine(xorOutput);

          System.Console.WriteLine("Frequency was: {0}", String.Join(",", frequency.Select(x => x)));
        }     
      }
    }
  }
}