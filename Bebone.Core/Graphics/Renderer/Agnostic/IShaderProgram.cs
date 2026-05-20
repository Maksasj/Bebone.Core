using System.Drawing;
using System.Numerics;

namespace Bebone.Core.Graphics.Renderer.Agnostic
{
    public interface IShaderProgram
    {
        void Activate();

        void SetUniform(string name, int value);
        void SetUniform(string name, float value);
        void SetUniform(string name, Vector3 value);
        void SetUniform(string name, Vector2 vector);
        void SetUniform(string name, Color color);
        void SetUniform(string name, Matrix4x4 value);
    }
}
