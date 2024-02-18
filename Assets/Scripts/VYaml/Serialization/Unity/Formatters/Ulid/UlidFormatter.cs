#if ENABLE_ULID
using VYaml.Emitter;
using VYaml.Parser;

namespace VYaml.Serialization.Unity.Formatters.Ulid
{
  public class UlidFormatter : IYamlFormatter<System.Ulid>
  {
    public static readonly UlidFormatter Instance = new();

    private readonly byte[] _buffer = new byte[26];

    public void Serialize(ref Utf8YamlEmitter emitter, System.Ulid value, YamlSerializationContext context)
    {
      if (value.TryWriteStringify(_buffer))
      {
        emitter.WriteScalar(_buffer);
        return;
      }

      throw new YamlSerializerException("");
    }

    public System.Ulid Deserialize(ref YamlParser parser, YamlDeserializationContext context)
    {
      var buffer = parser.GetScalarAsUtf8();
      parser.Read();
      return System.Ulid.TryParse(buffer, out var ulid) ? ulid : default;
    }
  }
}
#endif