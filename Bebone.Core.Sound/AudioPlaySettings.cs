namespace Bebone.Core.Sound.Abstractions;

public readonly record struct AudioPlaySettings(bool Loop, float Volume)
{
    public static AudioPlaySettings Default => new(false, 1f);
}