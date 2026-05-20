namespace Bebone.Core.Math;

public readonly struct Vector2Int(int x, int y) : IEquatable<Vector2Int>
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public float Length { get; } = MathF.Sqrt(x * x + y * y);

    public static Vector2Int Zero => new(0, 0);
    public static Vector2Int One => new(1, 1);
    public static Vector2Int Up => new(0, 1);
    public static Vector2Int Right => new(1, 0);
    public static Vector2Int Down => new(0, -1);
    public static Vector2Int Left => new(-1, 0);

    public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2Int operator *(Vector2Int v, int scalar) => new(v.X * scalar, v.Y * scalar);
    public static Vector2Int operator /(Vector2Int v, int scalar) => new(v.X / scalar, v.Y / scalar);

    public static bool operator ==(Vector2Int a, Vector2Int b) => a.Equals(b);
    public static bool operator !=(Vector2Int a, Vector2Int b) => !a.Equals(b);

    public override string ToString() => $"({X}, {Y})";

    public override bool Equals(object? obj) => obj is Vector2Int other && Equals(other);
    public bool Equals(Vector2Int other) => X == other.X && Y == other.Y;
    public override int GetHashCode() => HashCode.Combine(X, Y);
}
