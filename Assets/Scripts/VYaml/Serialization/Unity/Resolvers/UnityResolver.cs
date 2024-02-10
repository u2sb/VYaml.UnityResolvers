using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VYaml.Serialization.Unity.Formatters.Geometry;
using VYaml.Serialization.Unity.Formatters.Math;

namespace VYaml.Serialization.Unity.Resolvers
{
  public class UnityResolver : IYamlFormatterResolver
  {
    public static readonly UnityResolver Instance = new();

    private static readonly Dictionary<Type, IYamlFormatter> FormatterMap = new()
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

      { typeof(Bounds), BoundsFormatter.Instance },
      { typeof(BoundsInt), BoundsIntFormatter.Instance },
      { typeof(Plane), PlaneFormatter.Instance },
      { typeof(Rect), RectFormatter.Instance },
      { typeof(RectInt), RectIntFormatter.Instance },
      { typeof(RectOffset), RectOffsetFormatter.Instance }
    };

    private static readonly Dictionary<Type, Type> KnownGenericTypes = new();

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

        Formatter = null;
      }
    }
  }
}