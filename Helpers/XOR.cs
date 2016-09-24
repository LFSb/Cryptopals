using System;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication.Helpers
{
  public static class XOR
  {
    private const String OutputFormat = "{0:x}";

    public static String XOREqualLengthInputs(String input1, String input2, bool outputIsHex = true)
    {
      if(input1.Length != input2.Length)
      {
        throw new InvalidDataException(String.Format("Input lengths are not the same. Length of first input: {0} Length of second input: {1}", input1.Length, input2.Length));
      }    

      return DoXOR(
          Convert.FromBase64String(Converter.ConvertHexToBase64(input1)),
          Convert.FromBase64String(Converter.ConvertHexToBase64(input2)),
          outputIsHex
      );
    }

    public static String XORInputToByte(String input, byte key, bool outputIsHex = true)
    {
      var inputBytes = Convert.FromBase64String(Converter.ConvertHexToBase64(input));

      return DoXOR(inputBytes, inputBytes.Select(inputByte => key).ToArray(),outputIsHex);
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