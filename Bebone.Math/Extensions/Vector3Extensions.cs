using System.Numerics;

namespace Bebone.Math.Extensions;

public static class VectorExtensions
{
    public static Vector3 WithX(this Vector3 vector, float X) => new(X, vector.Y, vector.Z);
    public static Vector3 WithY(this Vector3 vector, float Y) => new(vector.X, Y, vector.Z);
    public static Vector3 WithZ(this Vector3 vector, float Z) => new(vector.X, vector.Y, Z);
}