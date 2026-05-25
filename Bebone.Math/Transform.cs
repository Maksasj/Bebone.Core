using System.Numerics;

namespace Bebone.Math;

public record struct Transform(Vector3 Position, Quaternion Rotation, Vector3 Scale)
{
    public Transform(Vector3 position) : this(position, Quaternion.Identity, Vector3.One) { }
    public Transform(Vector3 position, Quaternion rotation) : this(position, rotation, Vector3.One) { }
    public Transform(Vector3 position, Vector3 scale) : this(position, Quaternion.Identity, scale) { }

    public static Transform Identity => new(Vector3.Zero, Quaternion.Identity, Vector3.One);

    public readonly Matrix4x4 ToMatrix()
    {
        var scaleMatrix = Matrix4x4.CreateScale(Scale);
        var rotationMatrix = Matrix4x4.CreateFromQuaternion(Rotation);
        var translationMatrix = Matrix4x4.CreateTranslation(Position);

        return scaleMatrix * rotationMatrix * translationMatrix;
    }
}
