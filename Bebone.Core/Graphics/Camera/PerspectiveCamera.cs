using System.Numerics;

namespace Bebone.Core.Graphics.Camera
{
    public class PerspectiveCamera : ICamera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public Vector3 Up { get; set; }
        public float Fov { get; set; }

        public PerspectiveCamera()
        {
            Position = new Vector3(0.0f, 5.0f, 5.0f);
            Up = Vector3.UnitY;
            Target = new Vector3(0.0f, 0.0f, 0.0f);
            Fov = 45f;
        }

        public Vector3 GetDirection() => Vector3.Normalize(Position - Target);

        public Matrix4x4 GetViewMatrix() => Matrix4x4.CreateLookAt(Position, Target, Up);
        public Matrix4x4 GetProjectionMatrix(float aspectRatio) => Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 180f * Fov, aspectRatio, 0.1f, 10000.0f);
    }
}
