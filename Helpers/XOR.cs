using System;
using System.Text;
using System.IO;

namespace ConsoleApplication.Helpers
{
  public static class XOR
  {
    public static String XORInputs(String input1, String input2)
    {
      if(input1.Length != input2.Length)
      {
        throw new InvalidDataException(String.Format("Input lengths are not the same. Length of first input: {0} Length of second input: {1}", input1.Length, input2.Length));
      }

      var output = new StringBuilder();

      for(var i = 0; i < input1.Length; i++)
      {
        var a = Convert.ToByte(input1[i].ToString(), 16);

        var b = Convert.ToByte(input2[i].ToString(), 16);

        output.AppendFormat("{0:x}", a ^ b);
      }

      return output.ToString();
    }
  }
}