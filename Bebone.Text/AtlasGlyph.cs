namespace Bebone.Text;

public readonly record struct AtlasGlyph
{
    public float U0 { get; init; }
    public float V0 { get; init; }
    public float U1 { get; init; }
    public float V1 { get; init; }

    public int Width { get; init; }
    public int Height { get; init; }

    public int BearingX { get; init; }
    public int BearingY { get; init; }

    public int Advance { get; init; }
}