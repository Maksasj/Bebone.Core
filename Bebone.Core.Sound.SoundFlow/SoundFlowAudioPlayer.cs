using Bebone.Core.Sound.Abstractions;
using SoundFlow.Abstracts;
using SoundFlow.Abstracts.Devices;
using SoundFlow.Components;
using SoundFlow.Providers;
using SoundFlow.Structs;

namespace Bebone.Core.Sound.SoundFlow;

public sealed class SoundFlowAudioPlayer : IAudioPlayer, IDisposable
{
    private readonly AudioEngine _engine;
    private readonly AudioPlaybackDevice _playbackDevice;
    private readonly Dictionary<AudioHandle, PlayingSound> _playingSounds = [];
    private readonly AudioFormat _audioFormat;

    public SoundFlowAudioPlayer(AudioEngine engine)
    {
        _engine = engine;
        _audioFormat = AudioFormat.DvdHq;

        _playbackDevice = GetDefaultPlaybackDevice();
        _playbackDevice.Start();
    }

    private AudioPlaybackDevice GetDefaultPlaybackDevice()
    {
        _engine.UpdateAudioDevicesInfo();
        var defaultDevice = _engine.PlaybackDevices.FirstOrDefault(x => x.IsDefault);
        return _engine.InitializePlaybackDevice(defaultDevice, _audioFormat);
    }

    public AudioHandle Play(Stream audioStream) => Play(audioStream, AudioPlaySettings.Default);

    // TODO: handle exceptions
    public AudioHandle Play(Stream audioStream, AudioPlaySettings settings)
    {
        if (audioStream.CanSeek)
            audioStream.Position = 0;

        var dataProvider = new StreamDataProvider(_engine, audioStream);
        var player = new SoundPlayer(_engine, _audioFormat, dataProvider)
        {
            Volume = settings.Volume,
            IsLooping = settings.Loop
        };

        _playbackDevice.MasterMixer.AddComponent(player);

        var handle = AudioHandle.New();
        var playingSound = new PlayingSound(player, dataProvider);

        _playingSounds.Add(handle, playingSound);

        player.Play();

        return handle;
    }

    public void Stop(AudioHandle handle)
    {
        if (!_playingSounds.Remove(handle, out var sound))
            return;

        _playbackDevice.MasterMixer.RemoveComponent(sound.Player);
        sound.Dispose();
    }

    public void Dispose()
    {
        _playbackDevice.Stop();

        _engine.Dispose();
        _playbackDevice.Dispose();

        foreach (var playingSound in _playingSounds.Values)
        {
            playingSound.Dispose();
        }

        _playingSounds.Clear();
    }
}