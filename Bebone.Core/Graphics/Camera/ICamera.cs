using System.Numerics;

namespace Bebone.Core.Graphics.Camera;

public interface ICamera
{
    public Matrix4x4 GetViewMatrix();
    public Matrix4x4 GetProjectionMatrix(float aspectRatio);
}
