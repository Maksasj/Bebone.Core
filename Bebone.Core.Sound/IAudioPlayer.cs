namespace Bebone.Core.Sound.Abstractions;

public interface IAudioPlayer
{
    AudioHandle Play(Stream audioStream);
    AudioHandle Play(Stream audioStream, AudioPlaySettings settings);
    void Stop(AudioHandle handle);
}
