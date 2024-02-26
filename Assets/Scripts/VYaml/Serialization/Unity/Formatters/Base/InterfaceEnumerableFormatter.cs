#nullable enable
using System.Collections.Generic;
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Base
{
  public class InterfaceEnumerableFormatter<TR, T>
    : IYamlFormatter<TR?> where TR : IEnumerable<T>
  {
    public virtual void Serialize(ref Utf8YamlEmitter emitter, TR? value, YamlSerializationContext context)
    {
      if (value == null)
      {
        emitter.WriteNull();
        return;
      }

      var elementFormatter = context.Resolver.GetFormatterWithVerify<T>();
      emitter.BeginSequence();
      foreach (var x in value) elementFormatter.Serialize(ref emitter, x, context);
      emitter.EndSequence();
    }

    public virtual TR? Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      throw new YamlSerializerException($"Please override the {typeof(TR).Name} deserialize method");
    }
  }
}