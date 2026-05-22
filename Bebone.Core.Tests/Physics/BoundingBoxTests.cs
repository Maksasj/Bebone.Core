using Bebone.Physics;
using FsCheck;
using FsCheck.Fluent;
using System.Numerics;

namespace Bebone.Core.Tests.Physics;

[TestFixture]
public class BoundingBoxTests
{
    [Test]
    public void Constructor_CalculatesMinAndMax()
    {
        Prop.ForAll<(int x, int y, int z, int sizeX, int sizeY, int sizeZ)>(data =>
        {
            // Arrange & Act
            var box = new BoundingBox(
                new Vector3(data.x, data.y, data.z),
                new Vector3(data.sizeX, data.sizeY, data.sizeZ));

            // Assert
            var sameAsCenter = box.Center.X == data.x && box.Center.Y == data.y && box.Center.Z == data.z;
            var minIsCorrect = (box.Min.X == data.x - data.sizeX) && (box.Min.Y == data.y - data.sizeY) && (box.Min.Z == data.z - data.sizeZ);
            var maxIsCorrect = (box.Max.X == data.x + data.sizeX) && (box.Max.Y == data.y + data.sizeY) && (box.Max.Z == data.z + data.sizeZ);
            return sameAsCenter && minIsCorrect && maxIsCorrect;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Intersects_WhenBoxesOverlap_ReturnsTrue()
    {
        // Arrange
        var a = new BoundingBox(Vector3.Zero, new Vector3(5, 5, 5));
        var b = new BoundingBox(new Vector3(3, 3, 3), new Vector3(5, 5, 5));

        // Act & Assert
        Assert.That(a.Intersects(b), Is.True);
        Assert.That(b.Intersects(a), Is.True);
    }

    [Test]
    public void Intersects_WhenBoxesDoNotOverlap_ReturnsFalse()
    {
        // Arrange
        var a = new BoundingBox(Vector3.Zero, new Vector3(1, 1, 1));
        var b = new BoundingBox(new Vector3(5, 0, 0), new Vector3(1, 1, 1));

        // Act & Assert
        Assert.That(a.Intersects(b), Is.False);
        Assert.That(b.Intersects(a), Is.False);
    }

    [Test]
    public void Intersects_WhenBoxesTouchAtFace_ReturnsTrue()
    {
        // Arrange
        var a = new BoundingBox(Vector3.Zero, new Vector3(1, 1, 1));
        var b = new BoundingBox(new Vector3(2, 0, 0), new Vector3(1, 1, 1));

        // Act & Assert
        Assert.That(a.Intersects(b), Is.True);
    }

    [Test]
    public void Intersects_WhenOneBoxContainsAnother_ReturnsTrue()
    {
        // Arrange
        var outer = new BoundingBox(Vector3.Zero, new Vector3(10, 10, 10));
        var inner = new BoundingBox(Vector3.Zero, new Vector3(1, 1, 1));

        // Act & Assert
        Assert.That(outer.Intersects(inner), Is.True);
        Assert.That(inner.Intersects(outer), Is.True);
    }

    [Test]
    public void Intersects_WhenSeparatedOnYAxis_ReturnsFalse()
    {
        // Arrange
        var a = new BoundingBox(Vector3.Zero, new Vector3(1, 1, 1));
        var b = new BoundingBox(new Vector3(0, 3, 0), new Vector3(1, 1, 1));

        // Act & Assert
        Assert.That(a.Intersects(b), Is.False);
    }

    [Test]
    public void Intersects_WhenSeparatedOnZAxis_ReturnsFalse()
    {
        // Arrange
        var a = new BoundingBox(Vector3.Zero, new Vector3(1, 1, 1));
        var b = new BoundingBox(new Vector3(0, 0, 3), new Vector3(1, 1, 1));

        // Act & Assert
        Assert.That(a.Intersects(b), Is.False);
    }
}
