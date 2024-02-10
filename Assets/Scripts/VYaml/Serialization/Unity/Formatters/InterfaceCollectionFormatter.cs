using System.Collections.Generic;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters
{
  public class InterfaceCollectionFormatter<TR, T>
    : InterfaceEnumerableFormatter<TR, T> where TR : class, ICollection<T>, new()
  {
    public override TR? Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      if (parser.IsNullScalar())
      {
        parser.Read();
        return default;
      }

      parser.ReadWithVerify(ParseEventType.SequenceStart);

      var list = new TR();

      var elementFormatter = context.Resolver.GetFormatterWithVerify<T>();
      while (!parser.End && parser.CurrentEventType != ParseEventType.SequenceEnd)
      {
        var value = context.DeserializeWithAlias(elementFormatter, ref parser);
        list.Add(value);
      }

      parser.ReadWithVerify(ParseEventType.SequenceEnd);

      return list;
    }
  }
}