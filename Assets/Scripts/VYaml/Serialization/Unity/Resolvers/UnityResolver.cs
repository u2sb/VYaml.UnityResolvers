using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using VYaml.Serialization.Unity.Formatters;
using VYaml.Serialization.Unity.Formatters.Geometry;
using VYaml.Serialization.Unity.Formatters.Graphics;
using VYaml.Serialization.Unity.Formatters.Hash;
using VYaml.Serialization.Unity.Formatters.Math;
using VYaml.Serialization.Unity.Formatters.NativeArray;

namespace VYaml.Serialization.Unity.Resolvers
{
  public class UnityResolver : IYamlFormatterResolver
  {
    public static readonly UnityResolver Instance = new();

    public static readonly Dictionary<Type, IYamlFormatter> FormatterMap = new()
    {
      { typeof(Color), ColorFormatter.Instance },
      { typeof(Color32), Color32Formatter.Instance },
      { typeof(Matrix4x4), Matrix4x4Formatter.Instance },
      { typeof(Quaternion), QuaternionFormatter.Instance },
      { typeof(Vector2), Vector2Formatter.Instance },
      { typeof(Vector2Int), Vector2IntFormatter.Instance },
      { typeof(Vector3), Vector3Formatter.Instance },
      { typeof(Vector3Int), Vector3IntFormatter.Instance },
      { typeof(Vector4), Vector4Formatter.Instance },

#if UNITY_2022_2_OR_NEWER
      { typeof(RefreshRate), RefreshRateFormatter.Instance },
#endif
      { typeof(Resolution), ResolutionFormatter.Instance },
      { typeof(Texture3D), Texture3DFormatter.Instance },

      { typeof(Hash128), Hash128Formatter.Instance },

      { typeof(Bounds), BoundsFormatter.Instance },
      { typeof(BoundsInt), BoundsIntFormatter.Instance },
      { typeof(Plane), PlaneFormatter.Instance },
      { typeof(Rect), RectFormatter.Instance },
      { typeof(RectInt), RectIntFormatter.Instance },
      { typeof(RectOffset), RectOffsetFormatter.Instance },

      { typeof(NativeArray<byte>), NativeByteArrayFormatter.Instance }
    };

    public static readonly Dictionary<Type, Type> KnownGenericTypes = new()
    {
      { typeof(HashSet<>), typeof(HashSetFormatter<>) },

      { typeof(NativeArray<>), typeof(NativeArrayFormatter<>) }
    };

    public IYamlFormatter<T>? GetFormatter<T>()
    {
      return FormatterCache<T>.Formatter;
    }

    private static object? TryCreateGenericFormatter(Type type)
    {
      Type? formatterType = null;

      if (type.IsArray)
      {
        if (type.IsSZArray)
        {
          formatterType = typeof(ArrayFormatter<>).MakeGenericType(type.GetElementType()!);
        }
        else
        {
          var rank = type.GetArrayRank();
          switch (rank)
          {
            // case 2:
            //     formatterType = typeof(TwoDimensionalArrayFormatter<>).MakeGenericType(type.GetElementType()!);
            //     break;
            // case 3:
            //     formatterType = typeof(ThreeDimensionalArrayFormatter<>).MakeGenericType(type.GetElementType()!);
            //     break;
            // case 4:
            //     formatterType = typeof(FourDimensionalArrayFormatter<>).MakeGenericType(type.GetElementType()!);
            //     break;
            // default:
            //     break; // not supported
          }
        }
      }
      else if (type.IsEnum)
      {
        formatterType = typeof(EnumAsStringFormatter<>).MakeGenericType(type);
      }
      else
      {
        formatterType = TryCreateGenericFormatterType(type, KnownGenericTypes);
      }

      if (formatterType != null) return Activator.CreateInstance(formatterType);
      return null;
    }

    private static Type? TryCreateGenericFormatterType(Type type, IDictionary<Type, Type> knownTypes)
    {
      if (type.IsGenericType)
      {
        var genericDefinition = type.GetGenericTypeDefinition();

        if (knownTypes.TryGetValue(genericDefinition, out var formatterType))
          return formatterType.MakeGenericType(type.GetGenericArguments());
      }

      return null;
    }

    private static class FormatterCache<T>
    {
      public static readonly IYamlFormatter<T>? Formatter;

      static FormatterCache()
      {
        if (FormatterMap.TryGetValue(typeof(T), out var formatter) && formatter is IYamlFormatter<T> value)
        {
          Formatter = value;
          return;
        }

        if (TryCreateGenericFormatter(typeof(T)) is IYamlFormatter<T> f)
        {
          Formatter = f;
          return;
        }

        Formatter = null;
      }
    }
  }
}