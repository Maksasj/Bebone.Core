using Bebone.Math;
using FsCheck;
using FsCheck.Fluent;

namespace Bebone.Core.Tests.Math;

[TestFixture]
public class Vector3IntTests
{
    [Test]
    public void Constructor_SetsXYZ()
    {
        Prop.ForAll<int, int, int>((x, y, z) =>
        {
            // Arrange & Act
            var v = new Vector3Int(x, y, z);

            // Assert
            return v.X == x &&
                   v.Y == y &&
                   v.Z == z;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Length_ReturnsMagnitude()
    {
        Prop.ForAll<int, int, int>((x, y, z) =>
        {
            // Arrange
            var v = new Vector3Int(x, y, z);

            // Act
            var expected = MathF.Sqrt(x * x + y * y + z * z);

            // Assert
            return v.Length == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void StaticVectors_ReturnExpectedValues()
    {
        // Arrange & Act & Assert
        Assert.That(Vector3Int.Zero, Is.EqualTo(new Vector3Int(0, 0, 0)));
        Assert.That(Vector3Int.One, Is.EqualTo(new Vector3Int(1, 1, 1)));

        Assert.That(Vector3Int.Up, Is.EqualTo(new Vector3Int(0, 1, 0)));
        Assert.That(Vector3Int.Right, Is.EqualTo(new Vector3Int(1, 0, 0)));
        Assert.That(Vector3Int.Down, Is.EqualTo(new Vector3Int(0, -1, 0)));
        Assert.That(Vector3Int.Left, Is.EqualTo(new Vector3Int(-1, 0, 0)));

        Assert.That(Vector3Int.Forward, Is.EqualTo(new Vector3Int(0, 0, 1)));
        Assert.That(Vector3Int.Back, Is.EqualTo(new Vector3Int(0, 0, -1)));
    }

    [Test]
    public void Add_ReturnsSum()
    {
        Prop.ForAll<(int x1, int y1, int z1, int x2, int y2, int z2)>(data =>
        {
            // Arrange
            var a = new Vector3Int(data.x1, data.y1, data.z1);
            var b = new Vector3Int(data.x2, data.y2, data.z2);

            // Act
            var result = a + b;

            // Assert
            var expected = new Vector3Int(
                data.x1 + data.x2,
                data.y1 + data.y2,
                data.z1 + data.z2);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Subtract_ReturnsDifference()
    {
        Prop.ForAll<(int x1, int y1, int z1, int x2, int y2, int z2)>(data =>
        {
            // Arrange
            var a = new Vector3Int(data.x1, data.y1, data.z1);
            var b = new Vector3Int(data.x2, data.y2, data.z2);

            // Act
            var result = a - b;

            // Assert
            var expected = new Vector3Int(
                data.x1 - data.x2,
                data.y1 - data.y2,
                data.z1 - data.z2);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Multiply_ReturnsScaledVector()
    {
        // Arrange & Act
        var result = new Vector3Int(1, -2, 3) * 3;

        // Assert
        Assert.That(result, Is.EqualTo(new Vector3Int(3, -6, 9)));

        Prop.ForAll<(int x1, int y1, int z1, int scalar)>(data =>
        {
            // Arrange
            var v = new Vector3Int(data.x1, data.y1, data.z1);

            // Act
            var result = v * data.scalar;

            // Assert
            var expected = new Vector3Int(
                data.x1 * data.scalar,
                data.y1 * data.scalar,
                data.z1 * data.scalar);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Divide_ReturnsDividedVector()
    {
        Prop.ForAll<(int x1, int y1, int z1, int scalar)>(data =>
        {
            if (data.scalar == 0)
                return true;

            // Arrange
            var v = new Vector3Int(data.x1, data.y1, data.z1);

            // Act
            var result = v / data.scalar;

            // Assert
            var expected = new Vector3Int(
                data.x1 / data.scalar,
                data.y1 / data.scalar,
                data.z1 / data.scalar);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        var v = new Vector3Int(1, 1, 1);

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() =>
        {
            _ = v / 0;
        });
    }

    [Test]
    public void EqualityOperators_WorkCorrectly()
    {
        // Arrange
        var a = new Vector3Int(1, 2, 3);
        var b = new Vector3Int(1, 2, 3);
        var c = new Vector3Int(1, 2, 4);

        // Act & Assert
        Assert.That(a == b, Is.True);
        Assert.That(a != c, Is.True);
    }

    [Test]
    public void Equals_WorksCorrectly()
    {
        // Arrange
        var a = new Vector3Int(1, 2, 3);
        var b = new Vector3Int(1, 2, 3);
        var c = new Vector3Int(1, 2, 4);

        // Act & Assert
        Assert.That(a.Equals(b), Is.True);
        Assert.That(a.Equals(c), Is.False);
    }

    [Test]
    public void GetHashCode_SameXYZ_ReturnsSameHash()
    {
        // Arrange
        var a = new Vector3Int(1, 2, 3);
        var b = new Vector3Int(1, 2, 3);

        // Act & Assert
        Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
    }

    [Test]
    public void ToString_ReturnsFormattedVector()
    {
        Prop.ForAll<int, int, int>((x, y, z) =>
        {
            var v = new Vector3Int(x, y, z);

            return v.ToString() == $"({x}, {y}, {z})";
        }).QuickCheckThrowOnFailure();
    }
}