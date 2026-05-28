using System.Numerics;

namespace Bebone.Math.Extensions;

public static class Vector2Extensions
{
    public static Vector2 WithX(this Vector2 vector, float X) => new(X, vector.Y);
    public static Vector2 WithY(this Vector2 vector, float Y) => new(vector.X, Y);
}
