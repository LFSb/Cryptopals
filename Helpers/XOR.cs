using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication.Helpers
{
  public static class XOR
  {
    private const String OutputFormat = "{0:x2}";

    public static String XOREqualLengthInputs(byte[] input1, byte[] input2, bool outputIsHex = true)
    {
      if(input1.Length != input2.Length)
      {
        throw new InvalidDataException(String.Format("Input lengths are not the same. Length of first input: {0} Length of second input: {1}", input1.Length, input2.Length));
      }    

      return DoXOR(
          input1,
          input2,
          outputIsHex
      );
    }

    public static String XORInputToByte(byte[] input, byte key)
    {
      return DoXOR(input, input.Select(inputByte => key).ToArray(), false);
    }

    public static String RepeatingKeyXOR(byte[] input, byte[] key, bool outputIsHex = true)
    {
      var constructedKeyList = new List<byte>();

      while(constructedKeyList.Count < input.Length)
      {
        constructedKeyList.AddRange(key);
      }

      if(constructedKeyList.Count != input.Length)
      {
        constructedKeyList = constructedKeyList.Take(input.Length).ToList();
      }
      
      return DoXOR(input, constructedKeyList.ToArray(), outputIsHex);
    }

    private static String DoXOR(byte[] input1, byte[] input2, bool outputIsHex)
    {
      var output = new StringBuilder();

      for(var idx = 0; idx < input1.Length; idx++)
      {
        if(outputIsHex)
        {
          output.AppendFormat(OutputFormat, input1[idx] ^ input2[idx]);
        }
        else
        {
          output.Append(
            Encoding.ASCII.GetString(new byte[]{(byte)(input1[idx] ^ input2[idx])})
          );
        }
      }
    
      return output.ToString();
    }
  }
}