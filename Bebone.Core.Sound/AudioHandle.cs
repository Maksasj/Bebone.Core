namespace Bebone.Core.Sound.Abstractions;

public readonly record struct AudioHandle(Guid Id)
{
    public static AudioHandle New() => new(Guid.NewGuid());
}