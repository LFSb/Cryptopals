namespace ConsoleApplication.Helpers
{
  public static class CharHelper
  {
    public static bool IsHex(this char input)
    {
      return ((input >= '0' && input <= '9') || 
                 (input >= 'a' && input <= 'f') || 
                 (input >= 'A' && input <= 'F'));
    }
  }
}