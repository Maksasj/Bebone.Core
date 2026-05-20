namespace Bebone.Core;

public readonly struct Vector3Int(int x, int y, int z) : IEquatable<Vector3Int>
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public int Z { get; } = z;
    public float Length { get; } = MathF.Sqrt(x * x + y * y + z * z);

    public static Vector3Int Zero => new(0, 0, 0);
    public static Vector3Int One => new(1, 1, 1);
    public static Vector3Int Up => new(0, 1, 0);
    public static Vector3Int Right => new(1, 0, 0);
    public static Vector3Int Down => new(0, -1, 0);
    public static Vector3Int Left => new(-1, 0, 0);
    public static Vector3Int Forward => new(0, 0, 1);
    public static Vector3Int Back => new(0, 0, -1);

    public static Vector3Int operator +(Vector3Int a, Vector3Int b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3Int operator -(Vector3Int a, Vector3Int b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vector3Int operator *(Vector3Int v, int scalar) => new(v.X * scalar, v.Y * scalar, v.Z * scalar);
    public static Vector3Int operator /(Vector3Int v, int scalar) => new(v.X / scalar, v.Y / scalar, v.Z / scalar);

    public static bool operator ==(Vector3Int a, Vector3Int b) => a.Equals(b);
    public static bool operator !=(Vector3Int a, Vector3Int b) => !a.Equals(b);

    public override string ToString() => $"({X}, {Y}, {Z})";

    public override bool Equals(object? obj) => obj is Vector3Int other && Equals(other);
    public bool Equals(Vector3Int other) => X == other.X && Y == other.Y && Z == other.Z;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
