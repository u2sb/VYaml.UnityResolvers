using System.Collections.Generic;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
    public class ColorFormatter : IYamlFormatter<Color>
    {
        public static readonly ColorFormatter Instance = new();

        public void Serialize(ref Utf8YamlEmitter emitter, Color value, YamlSerializationContext context)
        {
            var f = new[] { value.r, value.g, value.b, value.a };
            Utils.WriteFloatArrayWithFlowStyle(ref emitter, f, context);
        }

        public Color Deserialize(ref YamlParser parser, YamlDeserializationContext context)
        {
            if (parser.IsNullScalar())
            {
                parser.Read();
                return default;
            }

            var formatter = context.Resolver.GetFormatterWithVerify<List<float>>();

            var list = context.DeserializeWithAlias(formatter, ref parser);

            if (list.Count == 4) return new Color(list[0], list[1], list[2], list[3]);
            if (list.Count == 3) return new Color(list[0], list[1], list[2]);

            return default;
        }
    }
}