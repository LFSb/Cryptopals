using System;
using System.Text;
using System.IO;

namespace ConsoleApplication.Helpers
{
  public static class XOR
  {
    private const String OutputFormat = "{0:x}";

    public static String XOREqualLengthInputs(String input1, String input2)
    {
      if(input1.Length != input2.Length)
      {
        throw new InvalidDataException(String.Format("Input lengths are not the same. Length of first input: {0} Length of second input: {1}", input1.Length, input2.Length));
      }    

      return DoXOR(input1, input2);
    }

    public static String XORInputToChar(String input, char key)
    {
     return DoXOR(input, new String(key, input.Length));
    }

    private static String DoXOR(String input1, String input2)
    {
      var output = new StringBuilder();

      var input1Bytes = Convert.FromBase64String(input1.ToString());

      var input2Bytes = Convert.FromBase64String(input2.ToString());

      for(var idx = 0; idx < input1Bytes.Length; idx++)
      {
        output.AppendFormat(OutputFormat, input1Bytes[idx] ^ input2Bytes[idx]);
      }
    
      return output.ToString();
    }
  }
}