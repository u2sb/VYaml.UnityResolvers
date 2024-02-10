using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Hash
{
  public class Hash128Formatter : IYamlFormatter<Hash128>
  {
    public static readonly Hash128Formatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Hash128 value, YamlSerializationContext context)
    {
      emitter.WriteString(value.ToString());
    }

    public Hash128 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var hash128 = parser.ReadScalarAsString();
      if (hash128 != null) return Hash128.Parse(hash128);

      return default;
    }
  }
}