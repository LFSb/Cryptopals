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

    //This method will return the probable key size used in a repeating key XOR encryption.
    public static int FindProbableKeySize(byte[] input)
    {
      var hammingDistances = new Dictionary<int, int>();
      
      //First, Find the most probable size of the key.
      for(byte keySize = 2; keySize <= 40; keySize++)
      {
        var keySizeDistances = new List<int>(); 

        //Repeat the hamming distance calculation for an arbitrary amount, more repeats will cause more accurate results.

        for(var idx = 0; idx < 40; idx += 2)
        {
          var first = input.Skip(idx * keySize).Take(keySize).ToArray();

          var second = input.Skip((idx + 1) * keySize).Take(keySize).ToArray(); 

          keySizeDistances.Add(Frequency.CalculateHammingDistance(first, second));
        }

        //Normalise the sum of all the distances by dividing by keySize.
        hammingDistances.Add(keySize, keySizeDistances.Sum() / keySize);
      }

      //The most probable key size has the smallest hamming distance between the first two keysize sized blocks of bytes.
      return hammingDistances.OrderBy(x => x.Value).First().Key;
    }

    public static byte[] ReturnRepeatingXORKey(byte[] input,int probableKeySize)
    {
      //Create blocks of bytes.
      var blockAmount = input.Length / probableKeySize;

      var byteBlocks = new byte[blockAmount][];

      for(var blockNumber = 0; blockNumber < blockAmount; blockNumber++)
      {
        byteBlocks[blockNumber] = input.Skip(blockNumber * probableKeySize).Take(probableKeySize).ToArray();
      }

      //Next, transpose the previously made blocks. 
      //Make 5 "blocks", each containing all of the 1st, 2nd, 3rd, 4th and 5th byte of each of the previously made blocks.
      var transposedBlocks = new byte[probableKeySize][];

      for(var position = 0; position < probableKeySize; position++)
      {
        transposedBlocks[position] = byteBlocks.Select(block => block[position]).ToArray();
      }

      //Next, solve each block using single byte XOR.

      var probableKey = new byte[probableKeySize];

      for(var blockNumber = 0; blockNumber < transposedBlocks.Length; blockNumber++)
      {
        var ex6KeyScores = new Dictionary<byte, int>();

        var ex6KeyOutPut = new Dictionary<byte, string>();

        for(var idx = byte.MinValue; idx < byte.MaxValue; idx++)
        {
          var xorOutput = XOR.XORInputToByte(transposedBlocks[blockNumber], idx);

          ex6KeyOutPut.Add(idx, xorOutput);

          ex6KeyScores.Add(idx, Frequency.ScoreFrequencies(xorOutput));
        }

        var highestScore = ex6KeyScores.OrderByDescending(x => x.Value).First();

        //The key for each block is said position's character for the actual key.
        probableKey[blockNumber] = highestScore.Key;
      }

      return probableKey;
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