using Unity.Collections;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.NativeArray
{
  public class NativeArrayFormatter<T> : IYamlFormatter<NativeArray<T>> where T : struct
  {
    public void Serialize(ref Utf8YamlEmitter emitter, NativeArray<T> value, YamlSerializationContext context)
    {
      var elementFormatter = context.Resolver.GetFormatterWithVerify<T>();
      emitter.BeginSequence();
      foreach (var x in value) elementFormatter.Serialize(ref emitter, x, context);
      emitter.EndSequence();
    }

    public NativeArray<T> Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      throw new YamlSerializerException(
        "Deserializing NativeArray<> and NativeSlice<> is disabled to not cause accidental memory leaks. Use regular List<> or array types instead in your JSON models. Due to technical limitations, we cannot even populate existing NativeArray<> or NativeSlice<> values. You simply have to use other collection types.");
    }
  }
}