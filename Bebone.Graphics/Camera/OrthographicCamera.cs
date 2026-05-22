using System.Numerics;

namespace Bebone.Graphics.Camera;

public class OrthographicCamera(int left, int right, int bottom, int top, float zNearPlane, float zFarPlane) : ICamera
{
    public float Left = left;
    public float Right = right;

    public float Bottom = bottom;
    public float Top = top;

    public float ZNearPlane = zNearPlane;
    public float ZFarPlane = zFarPlane;

    public Matrix4x4 GetViewMatrix()
        => Matrix4x4.Identity;

    public Matrix4x4 GetProjectionMatrix(float aspectRatio)
        => Matrix4x4.CreateOrthographicOffCenter(Left, Right, Bottom, Top, ZNearPlane, ZFarPlane);
}