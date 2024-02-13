using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Graphics
{
  public class ResolutionFormatter : IYamlFormatter<Resolution>
  {
    public static readonly ResolutionFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Resolution value, YamlSerializationContext context)
    {
      emitter.BeginMapping();

      emitter.WriteString(nameof(Resolution.width));
      emitter.WriteInt32(value.width);

      emitter.WriteString(nameof(Resolution.height));
      emitter.WriteInt32(value.height);

#if UNITY_2022_2_OR_NEWER
      emitter.WriteString(nameof(Resolution.refreshRateRatio));
      context.Serialize(ref emitter, value.refreshRateRatio);
#else
      emitter.WriteString(nameof(Resolution.refreshRate));
      context.Serialize(ref emitter, value.width);
#endif
      emitter.EndMapping();
    }

    public Resolution Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      var resolution = new Resolution();
      var refreshRateFormatter = context.Resolver.GetFormatterWithVerify<RefreshRate>();

      parser.ReadWithVerify(ParseEventType.MappingStart);

      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
      {
        var key = parser.ReadScalarAsString();

        if (key != null)
        {
          if (key.EqualsKey(nameof(Resolution.width)))
            resolution.width = parser.ReadScalarAsInt32();
          else if (key.EqualsKey(nameof(Resolution.height)))
            resolution.height = parser.ReadScalarAsInt32();
          else if (key.EqualsKey(nameof(Resolution.refreshRate)))
            resolution.refreshRate = parser.ReadScalarAsInt32();
#if UNITY_2022_2_OR_NEWER
          else if (key.EqualsKey(nameof(Resolution.refreshRateRatio)))
            resolution.refreshRateRatio = context.DeserializeWithAlias(refreshRateFormatter, ref parser);
#endif
        }
      }

      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return resolution;
    }
  }
}