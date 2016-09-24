using System;

namespace ConsoleApplication.Helpers
{
  public static class Frequency
  {
    private const string etaoinshrdlu = "etaoinshrdlumwfgypbvkjxqz";

    public static Int32 ScoreFrequencies(string input)
    {
      var score = 0;

      foreach(var character in input)
      {
        if((byte)character < 0x65)
        {
          score--;
        }
        switch(character)
        {
          case 'e':
          {
            score += 5;
            break;
          }
          case 't':
          {
            score += 5;
            break;
          }
          case 'a':
          {
            score += 4;
            break;
          }
          case 'o':
          {
            score += 4;
            break;
          }
          case 'i':
          {
            score += 4;
            break;
          }
        }     
      }

      return score;
    }
  }
}