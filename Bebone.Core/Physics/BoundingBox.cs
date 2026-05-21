using System.Numerics;

namespace Bebone.Core;

public readonly record struct BoundingBox
{
    public Vector3 Center { get; }
    public Vector3 Min { get; }
    public Vector3 Max { get; }

    public BoundingBox(Vector3 center, Vector3 size)
    {
        Center = center;

        Min = new Vector3(center.X - size.X, center.Y - size.Y, center.Z - size.Z);
        Max = new Vector3(center.X + size.X, center.Y + size.Y, center.Z + size.Z);
    }

    public bool Intersects(BoundingBox other)
    {
        return (Min.X <= other.Max.X && Max.X >= other.Min.X) &&
                (Min.Y <= other.Max.Y && Max.Y >= other.Min.Y) &&
                (Min.Z <= other.Max.Z && Max.Z >= other.Min.Z);
    }
}