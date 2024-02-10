using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Math
{
    public class Matrix4x4Formatter : IYamlFormatter<Matrix4x4>
    {
        public static readonly Matrix4x4Formatter Instance = new();

        public void Serialize(ref Utf8YamlEmitter emitter, Matrix4x4 value, YamlSerializationContext context)
        {
            var fs = new[]
            {
                new[] { value.m00, value.m10, value.m20, value.m30 },
                new[] { value.m01, value.m11, value.m21, value.m31 },
                new[] { value.m02, value.m12, value.m22, value.m32 },
                new[] { value.m03, value.m13, value.m23, value.m33 }
            };

            emitter.BeginSequence();
            emitter.WriteRaw(ReadOnlySpan<byte>.Empty, false, true);
            foreach (var f in fs) Utils.WriteFloatArrayWithFlowStyle(ref emitter, f, context);
            emitter.EndSequence();
        }

        public Matrix4x4 Deserialize(ref YamlParser parser, YamlDeserializationContext context)
        {
            if (parser.IsNullScalar())
            {
                parser.Read();
                return default;
            }

            var formatter = context.Resolver.GetFormatterWithVerify<List<Vector4>>();

            var list = context.DeserializeWithAlias(formatter, ref parser);

            if (list.Count == 4) return new Matrix4x4(list[0], list[1], list[2], list[3]);

            return default;
        }
    }
}