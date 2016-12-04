using System;
using System.IO;
using System.Linq;
using System.Text;
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

    #region Challenge 5: Repeating key XOR

    private const string ex5Input = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";

    private const string ex5Key = "ICE";

    private const string ex5ExpectedOutput = "0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f";

    #endregion

    #region Challenge 6: The real shit

    private const string hammingDistanceInput1 = "this is a test";

    private const string hammingDistanceInput2 = "wokka wokka!!!";

    private const Int32 hammingDistanceCheck = 37;

    //Base 64 representation of the expected key.
    private const string ex6ExpectedKey = "VGVybWluYXRvciBYOiBCcmluZyB0aGUgbm9pc2U=";

    #endregion

    #region Challenge 7: AES in ECB mode

    private const string challenge7Key = "YELLOW SUBMARINE";

    #endregion

    public static void Main(string[] args)
    {
      if(Initialize())
      {
        var input = Convert.FromBase64String(File.ReadAllText("TestFiles/7.txt"));

        var output = Crypto.DecryptAES128ECB(input, Convert.FromBase64String(challenge7Key));
      }
    }

    public static bool Initialize()
    {
      //#1 https://cryptopals.com/sets/1/challenges/1
      if(string.Equals(Converter.ConvertHexToBase64(HexToConvert), HexConvertTarget)) 
      {
        System.Console.WriteLine("TEST: EX1: Converting hex to base64 successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: EX1: Converting hex to base64 failed! Exiting...");
        return false;
      }

      //#2 https://cryptopals.com/sets/1/challenges/2
      var output = XOR.XOREqualLengthInputs(Convert.FromBase64String(Converter.ConvertHexToBase64(Xor1)), Convert.FromBase64String(Converter.ConvertHexToBase64(Xor2)));

      if(String.Equals(output, ExpectedXor))
      {
        System.Console.WriteLine("TEST: EX2: XOR Successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: EX2: XOR failed! Output: {0}", output);
        return false;
      }

      //#3 https://cryptopals.com/sets/1/challenges/3
      var keyScores = new Dictionary<byte, int>();

      var keyOutput = new Dictionary<byte, string>();

      for(var idx = byte.MinValue; idx < byte.MaxValue; idx++)
      {
        var xorOutput = XOR.XORInputToByte(Convert.FromBase64String(Converter.ConvertHexToBase64(xord)), idx);

        keyOutput.Add(idx, xorOutput);

        keyScores.Add(idx, Frequency.ScoreFrequencies(xorOutput));
      }

      var topScores = keyScores.OrderByDescending(x => x.Value).First();

      var topOutput = keyOutput[topScores.Key];

      if(String.Equals(topOutput,ex3ExpectedAnswer))
      {
        System.Console.WriteLine("TEST: EX3: Single byte XOR successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: EX3: Single byte XOR unsuccesful, output was {0}", topOutput);
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
          var xorOutput = XOR.XORInputToByte(Convert.FromBase64String(Converter.ConvertHexToBase64(line)), idx);

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
        System.Console.WriteLine("TEST: EX4: Detect single character XOR successful!");
      }
      else
      {
        System.Console.WriteLine("TEST: EX4: Detect single character XOR not successful, output was: {0}", highestScoringLine.Value.Value);
        return false;
      }

      var ex5Output = XOR.RepeatingKeyXOR(Encoding.ASCII.GetBytes(ex5Input), Encoding.ASCII.GetBytes(ex5Key));
      var reversed = XOR.RepeatingKeyXOR(Convert.FromBase64String(Converter.ConvertHexToBase64(ex5Output)), Encoding.ASCII.GetBytes(ex5Key), false);
      
      if(string.Equals(ex5Output, ex5ExpectedOutput) && string.Equals(reversed, ex5Input))
      {
        System.Console.WriteLine("TEST: EX5: Repeating character XOR successful! It even works both ways!");
      }
      else
      {
        System.Console.WriteLine("TEST: EX5: Repeating character XOR not successful, output: {0}", ex5Output);
        return false;
      }

      var hammingDistance = Frequency.CalculateHammingDistance(Encoding.ASCII.GetBytes(hammingDistanceInput1), Encoding.ASCII.GetBytes(hammingDistanceInput2));

      if(hammingDistance == hammingDistanceCheck)
      {
        System.Console.WriteLine("TEST: EX6.1: Hamming distance check completed.");
      }
      else
      {
        System.Console.WriteLine("TEST: EX6.1: Hamming distance check failed. Output: {0}", hammingDistance);
        return false;
      }

      var ex6InputBytes = Convert.FromBase64String(File.ReadAllText("TestFiles/6.txt"));
      
      //The most probable key size has the smallest hamming distance between the first two keysize sized blocks of bytes.
      var probableKeySize = XOR.FindProbableKeySize(ex6InputBytes);

      //Determine the key by transposing the blocks and executing single byte XOR operations on said blocks, scoring the outputs. The single bytes that output the highest scores will probably be part of the key.  
      var probableKey = XOR.ReturnRepeatingXORKey(ex6InputBytes, probableKeySize);

      //After this, execute repeating key XOR.
      var ex6Output = XOR.RepeatingKeyXOR(ex6InputBytes, probableKey, false);

      if(string.Equals(System.Convert.ToBase64String(probableKey), System.Convert.ToBase64String(probableKey)))
      {
        System.Console.WriteLine("TEST: EX6: Breaking repeating key XOR solved!");
      }
      else
      {
        System.Console.WriteLine("Unable to solve EX6, something went wrong.");
        return false;
      }

      return true;
    }
  }
}