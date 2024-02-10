using System;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Geometry
{
    public class BoundsIntFormatter : IYamlFormatter<BoundsInt>
    {
        public static readonly BoundsIntFormatter Instance = new();

        public void Serialize(ref Utf8YamlEmitter emitter, BoundsInt value, YamlSerializationContext context)
        {
            emitter.BeginMapping();
            emitter.WriteString(nameof(BoundsInt.position));
            context.Serialize(ref emitter, value.position);

            emitter.WriteString(nameof(BoundsInt.size));
            context.Serialize(ref emitter, value.size);
            emitter.EndMapping();
        }

        public BoundsInt Deserialize(ref YamlParser parser, YamlDeserializationContext context)
        {
            if (parser.IsNullScalar())
            {
                parser.Read();
                return default;
            }
            
            var boundsInt = new BoundsInt();
            var vector3IntFormatter = context.Resolver.GetFormatterWithVerify<Vector3Int>();

            parser.ReadWithVerify(ParseEventType.MappingStart);

            while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
            {
                var key = parser.ReadScalarAsString();
                var value = context.DeserializeWithAlias(vector3IntFormatter, ref parser);

                if (key != null)
                {
                    if (key.EqualsKey(nameof(BoundsInt.position)))
                        boundsInt.position = value;
                    else if (key.EqualsKey(nameof(BoundsInt.size)))
                        boundsInt.size = value;
                }
            }

            parser.ReadWithVerify(ParseEventType.MappingEnd);

            return boundsInt;
        }
    }
}