using Bebone.Math;
using FsCheck;
using FsCheck.Fluent;
using System.Numerics;

namespace Bebone.Core.Tests.Math;

[TestFixture]
public class TransformTests
{
    [Test]
    public void Constructor_SetsPositionRotationAndScale()
    {
        // Arrange
        var position = new Vector3(1f, 2f, 3f);
        var rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathF.PI / 4);
        var scale = new Vector3(2f, 2f, 2f);

        // Act
        var t = new Transform(position, rotation, scale);

        // Assert
        Assert.That(t.Position, Is.EqualTo(position));
        Assert.That(t.Rotation, Is.EqualTo(rotation));
        Assert.That(t.Scale, Is.EqualTo(scale));
    }

    [Test]
    public void Constructor_WithPosition_SetsDefaultRotationAndScale()
    {
        // Arrange & Act
        var t = new Transform(new Vector3(1f, 2f, 3f));

        // Assert
        Assert.That(t.Rotation, Is.EqualTo(Quaternion.Identity));
        Assert.That(t.Scale, Is.EqualTo(Vector3.One));
    }

    [Test]
    public void Constructor_WithPositionAndRotation_SetsDefaultScale()
    {
        // Arrange
        var rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, MathF.PI);

        // Act
        var t = new Transform(new Vector3(1f, 0f, 0f), rotation);

        // Assert
        Assert.That(t.Rotation, Is.EqualTo(rotation));
        Assert.That(t.Scale, Is.EqualTo(Vector3.One));
    }

    [Test]
    public void Constructor_WithPositionAndScale_SetsDefaultRotation()
    {
        // Arrange & Act
        var t = new Transform(new Vector3(1f, 2f, 3f), new Vector3(3f, 3f, 3f));

        // Assert
        Assert.That(t.Rotation, Is.EqualTo(Quaternion.Identity));
        Assert.That(t.Scale, Is.EqualTo(new Vector3(3f, 3f, 3f)));
    }

    [Test]
    public void Identity_ReturnsExpectedValues()
    {
        // Arrange & Act
        var t = Transform.Identity;

        // Assert
        Assert.That(t.Position, Is.EqualTo(Vector3.Zero));
        Assert.That(t.Rotation, Is.EqualTo(Quaternion.Identity));
        Assert.That(t.Scale, Is.EqualTo(Vector3.One));
    }

    [Test]
    public void ToMatrix_Identity_ReturnsIdentityMatrix()
    {
        // Arrange & Act
        var matrix = Transform.Identity.ToMatrix();

        // Assert
        Assert.That(matrix, Is.EqualTo(Matrix4x4.Identity));
    }

    [Test]
    public void ToMatrix_TranslationOnly_ContainsCorrectTranslation()
    {
        Prop.ForAll<(float x, float y, float z)>(data =>
        {
            if (!float.IsFinite(data.x) || !float.IsFinite(data.y) || !float.IsFinite(data.z))
                return true;

            // Arrange
            var position = new Vector3(data.x, data.y, data.z);
            var t = new Transform(position);

            // Act
            var matrix = t.ToMatrix();

            // Assert
            return matrix.M41 == data.x
                && matrix.M42 == data.y
                && matrix.M43 == data.z;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void ToMatrix_UniformScale_ContainsCorrectScale()
    {
        // Arrange
        var scale = new Vector3(3f, 3f, 3f);
        var t = new Transform(Vector3.Zero, scale);

        // Act
        var matrix = t.ToMatrix();

        // Assert
        Assert.That(matrix.M11, Is.EqualTo(3f).Within(1e-5f));
        Assert.That(matrix.M22, Is.EqualTo(3f).Within(1e-5f));
        Assert.That(matrix.M33, Is.EqualTo(3f).Within(1e-5f));
    }

    [Test]
    public void Equals_WorksCorrectly()
    {
        // Arrange
        var a = new Transform(new Vector3(1f, 2f, 3f), Quaternion.Identity, Vector3.One);
        var b = new Transform(new Vector3(1f, 2f, 3f), Quaternion.Identity, Vector3.One);
        var c = new Transform(new Vector3(9f, 9f, 9f), Quaternion.Identity, Vector3.One);

        // Act & Assert
        Assert.That(a, Is.EqualTo(b));
        Assert.That(a, Is.Not.EqualTo(c));
    }

    [Test]
    public void GetHashCode_SameValues_ReturnsSameHash()
    {
        // Arrange
        var a = new Transform(new Vector3(1f, 2f, 3f), Quaternion.Identity, Vector3.One);
        var b = new Transform(new Vector3(1f, 2f, 3f), Quaternion.Identity, Vector3.One);

        // Act & Assert
        Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
    }
}
