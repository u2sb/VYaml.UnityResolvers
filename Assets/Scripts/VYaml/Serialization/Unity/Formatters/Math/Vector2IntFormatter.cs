using System.Collections.Generic;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
    public class Vector2IntFormatter : IYamlFormatter<Vector2Int>
    {
        public static readonly Vector2IntFormatter Instance = new();

        public void Serialize(ref Utf8YamlEmitter emitter, Vector2Int value, YamlSerializationContext context)
        {
            var f = new[] { value.x, value.y };
            Utils.WriteIntArrayWithFlowStyle(ref emitter, f, context);
        }

        public Vector2Int Deserialize(ref YamlParser parser, YamlDeserializationContext context)
        {
            if (parser.IsNullScalar())
            {
                parser.Read();
                return default;
            }

            var formatter = context.Resolver.GetFormatterWithVerify<List<int>>();

            var list = context.DeserializeWithAlias(formatter, ref parser);
            if (list.Count == 2) return new Vector2Int(list[0], list[1]);

            return default;
        }
    }
}