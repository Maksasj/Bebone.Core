namespace Bebone.Core.Graphics
{
    public interface IPass
    {
        void Compile(Dictionary<string, object> resources);

        void Execute();
    }
}
