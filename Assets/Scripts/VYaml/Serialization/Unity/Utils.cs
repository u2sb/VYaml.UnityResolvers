using System;

namespace VYaml.Serialization.Unity
{
  internal static class Utils
  {
    public static bool EqualsKey(this string s1, string s2)
    {
      return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
    }
  }
}