using System;
using System.Linq;
using System.Collections;

namespace ConsoleApplication.Helpers
{
  public static class Crypto
  {
    public static byte[] DecryptAES128ECB(byte[] input, byte[] key, int blockSize)
    {
      //Step 1: KeyExpansions.


      //Step 2: Initial round, we'll xor the blocks of blockSize with the key.

      var initialRound = XOR.DoXOR(input.Take(blockSize).ToArray(), key);

      return null;
    }

    public static class KeySchedule
    {
      private const int n = 16;

      private const int b = 176;

      public static byte[] ExpandKey(byte[] input)
      {
        var expandedKey = new byte[b];
        
        //The first n bytes of the expandedKey are the encryption key.

        for(var idx = 0; idx < n; idx++)
        {
          expandedKey[idx] = input[idx];
        }

        var i = 1; //The initial recon iteration value is set to 1.

        for(var idx = n; idx < b; idx += 4)
        {
          var t = new byte[4];

          for(var b1 = 4; b1 > 0; b1--) //Assign the value of the previous four bytes from the expandedkey to t.
          {
            t[4 - b1] = expandedKey[idx - b1];  
          }

        }

        return expandedKey;
      }

      public static byte[] ScheduleCore(byte[] input, int rcon)
      {
        return null;
      }

      public static byte[] RotateLeft(byte[] input, int shifts)
      {
        var bits = new BitArray(input);
        
        while(shifts > 0)
        {
          var tmp = bits[0];

          for(var idx = 0; idx < bits.Length - 1; idx++)
          {
            bits[idx] = bits[idx + 1];
          }

          bits[bits.Length - 1] = tmp;

          shifts--;
        }

        throw new NotImplementedException();     
      }
    }
  }
}