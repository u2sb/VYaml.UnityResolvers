using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Templates
{
  public class CreateCode : Editor
  {
    private static readonly string VectorFormatterTemplatesFile =
      Path.Combine(Application.dataPath, "Templates", "VectorFormatter.txt");

    private static readonly string MatrixFormatterTemplatesFile =
      Path.Combine(Application.dataPath, "Templates", "MatrixFormatter.txt");

    private static readonly string MathematicsPath = Path.Combine(Application.dataPath,
      "Scripts\\VYaml\\Serialization\\Unity\\Formatters\\Mathematics");

    private static readonly string[] IndexNames = { "x", "y", "z", "w" };
    private static readonly string[] MatrixIndexNames = { "c0", "c1", "c2", "c3" };

    private static readonly string[] Names = { "Bool", "Double", "Float", "Int", "UInt" };

    [MenuItem("VYaml/Unity/CreateVector")]
    public static void CreateVector()
    {
      var d0 = new StringBuilder();

      foreach (var name in Names)
        for (var i = 2; i <= 4; i++)
        {
          d0.AppendLine($"{{ typeof({name.ToLower()}{i}), {name}{i}Formatter.Instance }},");
          d0.AppendLine(
            $"{{ typeof({name.ToLower()}{i}?), new StaticNullableFormatter<{name.ToLower()}{i}>({name}{i}Formatter.Instance) }},");

          var fileName = $"{name}{i}Formatter.cs";
          var s = CreateVector(name, i);
          File.WriteAllText(Path.Combine(MathematicsPath, fileName), s);
        }

      Debug.Log(d0.ToString());
    }

    [MenuItem("VYaml/Unity/CreateMatrix")]
    public static void CreateMatrix()
    {
      var d0 = new StringBuilder();

      foreach (var name in Names)
        for (var i = 2; i <= 4; i++)
        for (var j = 2; j <= 4; j++)
        {
          d0.AppendLine($"{{ typeof({name.ToLower()}{i}x{j}), {name}{i}x{j}Formatter.Instance }},");
          d0.AppendLine(
            $"{{ typeof({name.ToLower()}{i}x{j}?), new StaticNullableFormatter<{name.ToLower()}{i}x{j}>({name}{i}x{j}Formatter.Instance) }},");

          var fileName = $"{name}{i}x{j}Formatter.cs";
          var s = CreateMatrix(name, i, j);
          File.WriteAllText(Path.Combine(MathematicsPath, fileName), s);
        }

      Debug.Log(d0.ToString());
    }


    private static string CreateVector(string name, int l)
    {
      var s = File.ReadAllText(VectorFormatterTemplatesFile);
      var b0 = new StringBuilder();
      var b1 = new StringBuilder();
      for (var i = 0; i < l; i++)
      {
        b0.Append($"      Buf[{i}] = value.{IndexNames[i]};");
        b1.Append($"Buf[{i}]");

        if (i != l - 1)
        {
          b0.Append("\n");
          b1.Append(", ");
        }
      }

      s = s.Replace("{NAME}", name);
      s = s.Replace("{NAME_LOW}", name.ToLower());
      s = s.Replace("{L}", l.ToString());
      s = s.Replace("{BLOCK0}", b0.ToString());
      s = s.Replace("{BLOCK1}", b1.ToString());

      return s;
    }

    private static string CreateMatrix(string name, int rc, int cc)
    {
      var s = File.ReadAllText(MatrixFormatterTemplatesFile);
      var b0 = new StringBuilder();
      var b1 = new StringBuilder();
      for (var i = 0; i < cc; i++)
      {
        b0.Append($"      Buf[{i}] = value.{MatrixIndexNames[i]};");
        b1.Append($"Buf[{i}]");

        if (i != cc - 1)
        {
          b0.Append("\n");
          b1.Append(", ");
        }
      }

      s = s.Replace("{NAME}", name);
      s = s.Replace("{NAME_LOW}", name.ToLower());
      s = s.Replace("{RC}", rc.ToString());
      s = s.Replace("{CC}", cc.ToString());
      s = s.Replace("{BLOCK0}", b0.ToString());
      s = s.Replace("{BLOCK1}", b1.ToString());

      return s;
    }
  }
}