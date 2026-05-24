namespace Bebone.Graphics.Renderer;

public interface IDrawTask<T>
{
    public void Execute(T data);
}