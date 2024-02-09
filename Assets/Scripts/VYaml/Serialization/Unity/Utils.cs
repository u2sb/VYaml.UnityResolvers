using System.Collections.Generic;
using VYaml.Emitter;

namespace VYaml.Serialization.Unity
{
  internal static class Utils
  {
    public static void WriteFloatArrayWithFlowStyle(ref Utf8YamlEmitter emitter, IEnumerable<float> value,
      YamlSerializationContext context)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var f in value) emitter.WriteFloat(f);
      emitter.EndSequence();
    }

    public static void WriteByteArrayWithFlowStyle(ref Utf8YamlEmitter emitter, IEnumerable<byte> value,
      YamlSerializationContext context)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var b in value) emitter.WriteInt32(b);
      emitter.EndSequence();
    }

    public static void WriteIntArrayWithFlowStyle(ref Utf8YamlEmitter emitter, IEnumerable<int> value,
      YamlSerializationContext context)
    {
      emitter.BeginSequence(SequenceStyle.Flow);
      foreach (var i in value) emitter.WriteInt32(i);
      emitter.EndSequence();
    }
  }
}