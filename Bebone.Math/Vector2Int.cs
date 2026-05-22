namespace Bebone.Math;

public readonly record struct Vector2Int(int X, int Y)
{
    public float Length { get; } = MathF.Sqrt(X * X + Y * Y);

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

    public override string ToString() => $"({X}, {Y})";
}
