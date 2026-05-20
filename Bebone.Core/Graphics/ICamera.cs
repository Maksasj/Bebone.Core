using System.Numerics;

namespace tpi.Graphics
{
    public interface ICamera
    {
        public Matrix4x4 GetViewMatrix();
        public Matrix4x4 GetProjectionMatrix(float aspectRatio);
    }
}