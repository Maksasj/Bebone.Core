using Bebone.Sound.Abstractions;

namespace Bebone.Core.Tests.Sound.Abstractions;

[TestFixture]
public class AudioPlaySettingsTests
{
    [Test]
    public void AudioPlaySettings_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var settings = new AudioPlaySettings(loop: true, volume: 0.5f);

        // Assert
        Assert.That(settings.Loop, Is.True);
        Assert.That(settings.Volume, Is.EqualTo(0.5f));
    }

    [Test]
    public void AudioPlaySettings_Throws_WhenVolumeIsNegative()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new AudioPlaySettings(volume: -1f));
    }
}