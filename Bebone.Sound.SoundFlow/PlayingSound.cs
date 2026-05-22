using SoundFlow.Components;
using SoundFlow.Providers;

namespace Bebone.Sound.SoundFlow;

internal sealed record PlayingSound(SoundPlayer Player, StreamDataProvider DataProvider) : IDisposable
{
    public void Dispose()
    {
        Player.Stop();
        Player.Dispose();
        DataProvider.Dispose();
    }
}
