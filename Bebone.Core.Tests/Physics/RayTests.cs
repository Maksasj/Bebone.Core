using System.Numerics;

namespace Bebone.Core.Tests.Physics;

[TestFixture]
public class RayTests
{
    [Test]
    public void Ray_SetsOrigin()
    {
        // Arrange
        var origin = new Vector3(1, 2, 3);
        var direction = new Vector3(10, 0, 0);

        // Act
        var ray = new Ray(origin, direction);

        // Assert
        Assert.That(ray.Origin, Is.EqualTo(origin));
    }

    [Test]
    public void Ray_NormalizesDirection()
    {
        // Arrange
        var direction = new Vector3(10, 0, 0);

        // Act
        var ray = new Ray(Vector3.Zero, direction);

        // Assert
        Assert.That(ray.Direction, Is.EqualTo(Vector3.UnitX));
    }

    [Test]
    public void Ray_DirectionIsZero_ThrowsArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new Ray(Vector3.Zero, Vector3.Zero);
        });
    }

    [Test]
    public void GetPoint_ReturnsCorrectPoint()
    {
        // Arrange
        var ray = new Ray(new Vector3(1, 2, 3), Vector3.UnitZ);

        // Act
        var point = ray.GetPoint(5f);

        // Assert
        Assert.That(point, Is.EqualTo(new Vector3(1, 2, 8)));
    }

    [Test]
    public void GetPoint_UsesNormalizedDirection()
    {
        // Arrange
        var ray = new Ray(Vector3.Zero, new Vector3(0, 0, 10));

        // Act
        var point = ray.GetPoint(2f);

        // Assert
        Assert.That(point, Is.EqualTo(new Vector3(0, 0, 2)));
    }

    [Test]
    public void GetPoint_WithNegativeDistance_ReturnsPointBehindOrigin()
    {
        // Arrange
        var ray = new Ray(Vector3.Zero, Vector3.UnitX);

        // Act
        var point = ray.GetPoint(-3f);

        // Assert
        Assert.That(point, Is.EqualTo(new Vector3(-3, 0, 0)));
    }
}