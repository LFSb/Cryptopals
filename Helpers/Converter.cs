using System;
using System.Linq;

namespace ConsoleApplication.Helpers
{
    public static class Converter
    {
        public static String ConvertHexToBase64(String hex)
        {
            var bytes = Enumerable.Range(0, hex.Length / 2).Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16)).ToArray();

            return System.Convert.ToBase64String(bytes);
        }

        public static String ConvertBase64ToHex(String base64)
        {
          var bytes = Convert.FromBase64String(base64);

          return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }
    }
}