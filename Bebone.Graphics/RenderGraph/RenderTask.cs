namespace Bebone.Graphics.RenderGraph;

public class RenderTask<PassData>(Func<IReadOnlyDictionary<string, object>, PassData> compile, Action<PassData> execute) : IPass
{
    private PassData? _data = default;
    private readonly Func<IReadOnlyDictionary<string, object>, PassData> _compile = compile;
    private readonly Action<PassData> _execute = execute;

    private bool _compiled = false;

    public void Compile(IReadOnlyDictionary<string, object> resources)
    {
        _data = _compile(resources);
        _compiled = true;
    }

    public void Execute()
    {
        if (!_compiled)
        {
            throw new InvalidOperationException("RenderTask must be compiled before execution.");
        }

        _execute(_data!);
    }
}