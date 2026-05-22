namespace Bebone.Sound.Abstractions;

public readonly struct AudioPlaySettings
{
    public bool Loop { get; }
    public float Volume { get; }

    public AudioPlaySettings(bool loop = false, float volume = 1f)
    {
        if (volume < 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(volume), "Volume must be a positive value.");
        }

        Volume = volume;
        Loop = loop;
    }
}