using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using VYaml.Serialization;
using Random = UnityEngine.Random;

namespace Tests
{
  public class VYamlTester
  {
    [Test]
    public void VYamlBoundsTester()
    {
      var data0 = new Dictionary<string, Bounds[]>
      {
        {
          nameof(Bounds),
          new[]
          {
            new Bounds(Random.insideUnitSphere * 100, Random.onUnitSphere * 100),
            new Bounds(Random.insideUnitSphere * 100, Random.onUnitSphere * 100)
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlBoundsIntTester()
    {
      var data0 = new Dictionary<string, BoundsInt[]>
      {
        {
          nameof(BoundsInt),
          new[]
          {
            new BoundsInt(Vector3Int.back, Vector3Int.one),
            new BoundsInt(Vector3Int.down, Vector3Int.zero)
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlPlaneTester()
    {
      var data0 = new Dictionary<string, Plane[]>
      {
        {
          nameof(Plane),
          new[]
          {
            new Plane(Random.insideUnitSphere * 100, Random.onUnitSphere * 100),
            new Plane(Random.insideUnitSphere * 100, Random.onUnitSphere * 100)
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlRectTester()
    {
      var data0 = new Dictionary<string, Rect[]>
      {
        {
          nameof(Rect),
          new[]
          {
            new Rect(Random.insideUnitCircle * 100, Random.insideUnitCircle * 100),
            new Rect(Random.insideUnitCircle * 100, Random.insideUnitCircle * 100)
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlRectIntIntTester()
    {
      var data0 = new Dictionary<string, RectInt[]>
      {
        {
          nameof(RectInt),
          new[]
          {
            new RectInt(Vector2Int.up, Vector2Int.one),
            new RectInt(Vector2Int.down, Vector2Int.zero)
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlRectOffsetTester()
    {
      var data0 = new Dictionary<string, RectOffset[]>
      {
        {
          nameof(RectOffset),
          new[]
          {
            new RectOffset((int)(Random.value * 100), (int)(Random.value * 100), (int)(Random.value * 100),
              (int)(Random.value * 100)),
            new RectOffset((int)(Random.value * 100), (int)(Random.value * 100), (int)(Random.value * 100),
              (int)(Random.value * 100))
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlResolutionTester()
    {
      var data0 = new Dictionary<string, Resolution[]>
      {
        {
          nameof(Resolution),
          new[]
          {
            new Resolution
            {
              width = (int)(Random.value * 100),
              height = (int)(Random.value * 100),
#pragma warning disable CS0618 // Type or member is obsolete
              refreshRate = (int)(Random.value * 100)
#pragma warning restore CS0618 // Type or member is obsolete
            },
            new Resolution
            {
              width = (int)(Random.value * 100),
              height = (int)(Random.value * 100),
#pragma warning disable CS0618 // Type or member is obsolete
              refreshRate = (int)(Random.value * 100)
#pragma warning restore CS0618 // Type or member is obsolete
            }
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlMatrix4x4Tester()
    {
      var data0 = new Dictionary<string, Matrix4x4[]>
      {
        {
          nameof(Matrix4x4),
          new[]
          {
            new Matrix4x4
            (
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100
            ),
            new Matrix4x4
            (
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100
            )
          }
        }
      };

      Test(data0);
    }

    [Test]
    public void VYamlColorTester()
    {
      var data0 = new Dictionary<string, Color>
      {
        {
          nameof(Color),
          new Color(Random.value, Random.value, Random.value, Random.value)
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlColor32Tester()
    {
      var data0 = new Dictionary<string, Color32>
      {
        {
          nameof(Color32),
          new Color(Random.value, Random.value, Random.value, Random.value)
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlVector4Tester()
    {
      var data0 = new Dictionary<string, Vector4>
      {
        {
          nameof(Vector4),
          new Vector4(Random.value, Random.value, Random.value, Random.value)
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlVector3Tester()
    {
      var data0 = new Dictionary<string, Vector3>
      {
        {
          nameof(Vector3),
          new Vector3(Random.value, Random.value, Random.value)
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlVector3IntTester()
    {
      var data0 = new Dictionary<string, Vector3Int>
      {
        {
          nameof(Vector3Int),
          new Vector3Int((int)(Random.value * 100), (int)(Random.value * 100), (int)(Random.value * 100))
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlVector2Tester()
    {
      var data0 = new Dictionary<string, Vector2>
      {
        {
          nameof(Vector2),
          new Vector2(Random.value, Random.value)
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlVector2IntTester()
    {
      var data0 = new Dictionary<string, Vector2Int>
      {
        {
          nameof(Vector2Int),
          new Vector2Int((int)(Random.value * 100), (int)(Random.value * 100))
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlUlidTester()
    {
      var data0 = new Dictionary<string, Ulid>
      {
        {
          nameof(Ulid),
          Ulid.NewUlid()
        }
      };
      Test(data0);

      var s = Ulid.NewUlid();
      var data1 = YamlSerializer.Deserialize<Ulid>(Encoding.UTF8.GetBytes(s.ToGuid().ToString()));
      Debug.Log(s);
      Debug.Log(data1);
    }

    [Test]
    public void VYamlFloat234Tester()
    {
      var data0 = new Tuple<float2, float3, float4>(
        new float2(Random.value, Random.value),
        new float3(Random.value, Random.value, Random.value),
        new float4(Random.value, Random.value, Random.value, Random.value)
      );
      Test(data0);
    }

    private void Test<T>(T data0)
    {
      var b0 = YamlSerializer.SerializeToString(data0);
      Debug.Log(b0);
      var data1 = YamlSerializer.Deserialize<T>(Encoding.UTF8.GetBytes(b0));
      var b1 = YamlSerializer.SerializeToString(data1);
      if (b0 != b1) throw new Exception();
    }
  }
}