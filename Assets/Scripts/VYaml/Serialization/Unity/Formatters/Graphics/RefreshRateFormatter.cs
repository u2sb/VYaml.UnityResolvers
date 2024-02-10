using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Graphics
{
  public class RefreshRateFormatter : IYamlFormatter<RefreshRate>
  {
    public static readonly RefreshRateFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, RefreshRate value, YamlSerializationContext context)
    {
      emitter.BeginMapping();

      emitter.WriteString(nameof(RefreshRate.denominator));
      emitter.WriteUInt32(value.denominator);

      emitter.WriteString(nameof(RefreshRate.numerator));
      emitter.WriteUInt32(value.numerator);

      emitter.EndMapping();
    }

    public RefreshRate Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var refreshRate = new RefreshRate();
      parser.ReadWithVerify(ParseEventType.MappingStart);

      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
      {
        var key = parser.ReadScalarAsString();
        var value = parser.ReadScalarAsUInt32();

        if (key != null)
        {
          if (key.EqualsKey(nameof(RefreshRate.denominator)))
            refreshRate.denominator = value;
          else if (key.EqualsKey(nameof(RefreshRate.numerator)))
            refreshRate.numerator = value;
        }
      }

      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return refreshRate;
    }
  }
}