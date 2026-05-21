namespace Bebone.Core;

public readonly record struct Vector3Int(int X, int Y, int Z)
{
    public float Length { get; } = MathF.Sqrt(X * X + Y * Y + Z * Z);

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
    public override string ToString() => $"({X}, {Y}, {Z})";
}
