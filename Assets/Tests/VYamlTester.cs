using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
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
              refreshRate = (int)(Random.value * 100)
            },
            new Resolution
            {
              width = (int)(Random.value * 100),
              height = (int)(Random.value * 100),
              refreshRate = (int)(Random.value * 100)
            }
          }
        }
      };
      Test(data0);
    }

    [Test]
    public void VYamlMatrix4x4Tester()
    {
      var data0 = new Dictionary<string, Matrix4x4>
      {
        {
          nameof(Matrix4x4),

            new Matrix4x4(
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100,
              Random.insideUnitSphere * 100
            )
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
          nameof(Matrix4x4),
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
          nameof(Matrix4x4),
          new Color(Random.value, Random.value, Random.value, Random.value)
        }
      };
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