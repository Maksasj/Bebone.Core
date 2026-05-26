namespace Bebone.Graphics.RenderGraph;

public readonly record struct FrameData
{
    public uint Width { get; init; }
    public uint Height { get; init; }
    public float AspectRatio { get; }

    public float Time { get; init; }

    public FrameData(uint width, uint height, float time)
    {
        Width = width;
        Height = height;
        Time = time;
        AspectRatio = (float)width / height;
    }
}