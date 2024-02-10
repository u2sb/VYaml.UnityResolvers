using Unity.Collections;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.NativeArray
{
  public class NativeArrayFormatter<T> : InterfaceEnumerableFormatter<NativeArray<T>, T> where T : struct
  {
    public override NativeArray<T> Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      throw new YamlSerializerException(
        "Deserializing NativeArray<> and NativeSlice<> is disabled to not cause accidental memory leaks. Use regular List<> or array types instead in your Yaml models. Due to technical limitations, we cannot even populate existing NativeArray<> or NativeSlice<> values. You simply have to use other collection types.");
    }
  }
}