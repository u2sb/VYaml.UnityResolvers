using System.Collections.Generic;
using UnityEngine;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Graphics
{
  public class Texture3DFormatter : IYamlFormatter<Texture3D?>
  {
    public static readonly Texture3DFormatter Instance = new();

    public void Serialize(ref Utf8YamlEmitter emitter, Texture3D? value, YamlSerializationContext context)
    {
      if (value == null)
      {
        emitter.WriteNull();
        return;
      }

      emitter.BeginMapping();

      emitter.WriteString(nameof(Texture3D.width));
      emitter.WriteInt32(value.width);

      emitter.WriteString(nameof(Texture3D.height));
      emitter.WriteInt32(value.height);

      emitter.WriteString(nameof(Texture3D.depth));
      emitter.WriteInt32(value.depth);

      emitter.WriteString(nameof(Texture3D.format));
      context.Serialize(ref emitter, value.format);

      emitter.WriteString(nameof(Texture3D.mipmapCount));
      emitter.WriteInt32(value.mipmapCount);

      emitter.WriteString(nameof(Texture3D.filterMode));
      context.Serialize(ref emitter, value.filterMode);

      emitter.WriteString(nameof(Texture3D.anisoLevel));
      emitter.WriteInt32(value.anisoLevel);

      emitter.WriteString(nameof(Texture3D.mipMapBias));
      emitter.WriteFloat(value.mipMapBias);

      emitter.WriteString(nameof(Texture3D.wrapModeU));
      context.Serialize(ref emitter, value.wrapModeU);

      emitter.WriteString(nameof(Texture3D.wrapModeV));
      context.Serialize(ref emitter, value.wrapModeV);

      emitter.WriteString(nameof(Texture3D.wrapModeW));
      context.Serialize(ref emitter, value.wrapModeW);

      emitter.WriteString("data");
      emitter.BeginSequence();
      for (var i = 0; i < value.mipmapCount; i++) context.Serialize(ref emitter, value.GetPixelData<byte>(i));
      emitter.EndSequence();

      emitter.EndMapping();
    }

    public Texture3D? Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      int width = 0, height = 0, depth = 0, mipmapCount = 0, anisoLevel = 0;
      float mipMapBias = 0;
      var format = TextureFormat.Alpha8;
      var filterMode = FilterMode.Point;
      TextureWrapMode wrapModeU = TextureWrapMode.Clamp,
        wrapModeV = TextureWrapMode.Clamp,
        wrapModeW = TextureWrapMode.Clamp;


      var data = new List<byte[]>();

      var wrapModeFormatter = context.Resolver.GetFormatterWithVerify<TextureWrapMode>();
      parser.ReadWithVerify(ParseEventType.MappingStart);

      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd)
      {
        var key = parser.ReadScalarAsString();

        if (key == null) continue;

        switch (key)
        {
          case not null when key.EqualsKey(nameof(Texture3D.width)):
            width = parser.ReadScalarAsInt32();
            break;
          case not null when key.EqualsKey(nameof(Texture3D.height)):
            height = parser.ReadScalarAsInt32();
            break;
          case not null when key.EqualsKey(nameof(Texture3D.depth)):
            depth = parser.ReadScalarAsInt32();
            break;
          case not null when key.EqualsKey(nameof(Texture3D.mipmapCount)):
            mipmapCount = parser.ReadScalarAsInt32();
            break;
          case not null when key.EqualsKey(nameof(Texture3D.format)):
            format = context.DeserializeWithAlias<TextureFormat>(ref parser);
            break;
          case not null when key.EqualsKey(nameof(Texture3D.filterMode)):
            filterMode = context.DeserializeWithAlias<FilterMode>(ref parser);
            break;
          case not null when key.EqualsKey(nameof(Texture3D.anisoLevel)):
            anisoLevel = parser.ReadScalarAsInt32();
            break;
          case not null when key.EqualsKey(nameof(Texture3D.mipMapBias)):
            mipMapBias = parser.ReadScalarAsFloat();
            break;
          case not null when key.EqualsKey(nameof(Texture3D.wrapModeU)):
            wrapModeU = context.DeserializeWithAlias(wrapModeFormatter, ref parser);
            break;
          case not null when key.EqualsKey(nameof(Texture3D.wrapModeV)):
            wrapModeV = context.DeserializeWithAlias(wrapModeFormatter, ref parser);
            break;
          case not null when key.EqualsKey(nameof(Texture3D.wrapModeW)):
            wrapModeW = context.DeserializeWithAlias(wrapModeFormatter, ref parser);
            break;
          case not null when key.EqualsKey("data"):
            data = context.DeserializeWithAlias<List<byte[]>>(ref parser);
            break;
        }
      }

      parser.ReadWithVerify(ParseEventType.MappingEnd);

      var texture = new Texture3D(width, height, depth, format, mipmapCount > 1)
      {
        filterMode = filterMode,
        anisoLevel = anisoLevel,
        mipMapBias = mipMapBias,
        wrapModeU = wrapModeU,
        wrapModeV = wrapModeV,
        wrapModeW = wrapModeW
      };

      if (data is { Count: > 0 })
      {
        for (var i = 0; i < data.Count; i++)
          texture.SetPixelData(data[i], i);

        texture.Apply();
      }

      return texture;
    }
  }
}