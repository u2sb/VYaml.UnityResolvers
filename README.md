This package is a UnityResolvers extension for VYaml

## Installation

> Please install [VYaml](https://github.com/hadashiA/VYaml.git) first

Install via git url

```
https://github.com/u2sb/VYaml.UnityResolvers.git?path=Assets/Scripts
```

## Types

### Geometry

#### Bounds

```yaml
center: [x, y, z]
size: [x, y, z]
```

#### BoundsInt

```yaml
position: [x, y, z]
size: [x, y, z]
```

#### Plane

```yaml
normal: [x, y, z]
distance: value
```

#### Rect RectInt

```yaml
position: [x, y, z]
size: [x, y, z]
```

#### RectOffset

```yaml
[left, right, top, bottom]
```

### Math

| Type                   | Yaml Example   |
| :--------------------- | :------------- |
| `Color` `Color32`      | `[r, g, b, a]` |
| `Quaternion`           | `[x, y, z, w]` |
| `Vector2` `Vector2Int` | `[x, y]`       |
| `Vector3` `Vector3Int` | `[x, y, z]`    |
| `Vector4`              | `[x, y, z, w]` |

#### Matrix4x4

```yaml
- [m00, m10, m20, m30]
- [m01, m11, m21, m31]
- [m02, m12, m22, m32]
- [m03, m13, m23, m33]
```

Raw Matrix:

$$
\begin{bmatrix}
 m00 & m01 & m02 & m03 \\
 m10 & m11 & m12 & m13 \\
 m20 & m21 & m22 & m23 \\
 m30 & m31 & m32 & m33 \\
\end{bmatrix}
$$

### NativeArray

| Type        | Yaml Example |
| :---------- | :----------- |
| NativeArray | []           |

## Manual Registration

If you want to register manually, define `VYAML_UNITYRESOLVER_NOTAUTOINIT`
