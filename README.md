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

| Type               | Yaml Example |
| :----------------- | :----------- |
| Color              | [r, g, b, a] |
| Color32            | [r, g, b, a] |
| Quaternion         | [x, y, z, w] |
| Vector2 Vector2Int | [x, y]       |
| Vector3 Vector3Int | [x, y, z]    |
| Vector4            | [x, y, z, w] |

### NativeArray

| Type        | Yaml Example |
| :---------- | :----------- |
| NativeArray | []           |

## Manual Registration

If you want to register manually, define `VYAML_UNITYRESOLVER_NOTAUTOINIT`
