using System;
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

      var output = XOR.XORInputs(Xor1, Xor2);

      if(String.Equals(output, ExpectedXor))
      {
        System.Console.WriteLine("XOR Successful!");
      }
      else
      {
        System.Console.WriteLine("XOR failed! Output: {0}", output);
      }
    }
  }
}