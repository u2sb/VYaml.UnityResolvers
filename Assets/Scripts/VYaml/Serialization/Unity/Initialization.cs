using UnityEditor;
using UnityEngine;
using VYaml.Serialization.Unity.Resolvers;

namespace VYaml.Serialization.Unity
{
  public static class Initialization
  {
#if !VYAML_UNITYRESOLVER_NOTAUTOINIT
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
#if UNITY_EDITOR
    [InitializeOnLoadMethod]
#endif
    public static void Init()
    {
      YamlSerializer.DefaultOptions.Resolver = CompositeResolver.Create(new IYamlFormatterResolver[]
      {
        StandardResolver.Instance,
        UnityResolver.Instance
      });
    }
  }
}