using System;
using System.Collections.Generic;
using VYaml.Emitter;

namespace VYaml.Serialization.Unity
{
  internal static class Utils
  {
    public static void WriteFloatArrayWithFlowStyle(this IEnumerable<float> value, ref Utf8YamlEmitter emitter)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var f in value) emitter.WriteFloat(f);
      emitter.EndSequence();
    }

    public static void WriteByteArrayWithFlowStyle(this IEnumerable<byte> value, ref Utf8YamlEmitter emitter)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var b in value) emitter.WriteInt32(b);
      emitter.EndSequence();
    }

    public static void WriteIntArrayWithFlowStyle(this IEnumerable<int> value, ref Utf8YamlEmitter emitter)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var i in value) emitter.WriteInt32(i);
      emitter.EndSequence();
    }

    public static bool EqualsKey(this string s1, string s2)
    {
      return s1.Equals(s2, StringComparison.OrdinalIgnoreCase);
    }
  }
}