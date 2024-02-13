using System;
using System.Collections.Generic;
using VYaml.Emitter;
using VYaml.Parser;

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

    public static byte[] ReadScalarAsByteArray(this ref YamlParser parser, int l = 64)
    {
      Span<byte> list = stackalloc byte[l];
      var i = 0;
      parser.ReadWithVerify(ParseEventType.SequenceStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
        if (i < list.Length)
        {
          list[i] = checked((byte)parser.ReadScalarAsUInt32());
          i++;
        }
        else
        {
          parser.Read();
        }

      parser.ReadWithVerify(ParseEventType.SequenceEnd);
      return list[..i].ToArray();
    }

    public static int[] ReadScalarAsIntArray(this ref YamlParser parser, int l = 64)
    {
      Span<int> list = stackalloc int[l];
      var i = 0;
      parser.ReadWithVerify(ParseEventType.SequenceStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
        if (i < list.Length)
        {
          list[i] = parser.ReadScalarAsInt32();
          i++;
        }
        else
        {
          parser.Read();
        }

      parser.ReadWithVerify(ParseEventType.SequenceEnd);
      return list[..i].ToArray();
    }

    public static float[] ReadScalarAsFloatArray(this ref YamlParser parser, int l = 64)
    {
      Span<float> list = stackalloc float[l];
      var i = 0;
      parser.ReadWithVerify(ParseEventType.SequenceStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
        if (i < list.Length)
        {
          list[i] = parser.ReadScalarAsFloat();
          i++;
        }
        else
        {
          parser.Read();
        }

      parser.ReadWithVerify(ParseEventType.SequenceEnd);
      return list[..i].ToArray();
    }
  }
}