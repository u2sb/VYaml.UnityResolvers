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

    public IYamlFormatter<T>? GetFormatter<T>()
    {
      return FormatterCache<T>.Formatter;
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
#if !VYAML_UNITYRESOLVER_AUTOINIT
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#if UNITY_EDITOR
    [InitializeOnLoadMethod]
#endif
    internal static void Init()
    {
      YamlSerializer.DefaultOptions.Resolver = CompositeResolver.Create(new IYamlFormatterResolver[]
      {
        StandardResolver.Instance,
        Instance
      });
    }
  }
#endif
}