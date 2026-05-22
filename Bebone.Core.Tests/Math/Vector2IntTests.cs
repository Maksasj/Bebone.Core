using Bebone.Math;
using FsCheck;
using FsCheck.Fluent;

namespace Bebone.Core.Tests.Math;

[TestFixture]
public class Vector2IntTests
{
    [Test]
    public void Constructor_SetsXAndY()
    {
        Prop.ForAll<int, int>((x, y) =>
        {
            // Arrange & Act
            var v = new Vector2Int(x, y);

            // Assert
            return v.X == x
                && v.Y == y;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Length_ReturnsMagnitude()
    {
        Prop.ForAll<(int x, int y)>(data =>
        {
            // Arrange & Act
            var v = new Vector2Int(data.x, data.y);

            // Assert
            var expected = MathF.Sqrt(
                data.x * data.x +
                data.y * data.y);

            return v.Length == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void StaticVectors_ReturnExpectedValues()
    {
        Assert.That(Vector2Int.Zero, Is.EqualTo(new Vector2Int(0, 0)));
        Assert.That(Vector2Int.One, Is.EqualTo(new Vector2Int(1, 1)));
        Assert.That(Vector2Int.Up, Is.EqualTo(new Vector2Int(0, 1)));
        Assert.That(Vector2Int.Right, Is.EqualTo(new Vector2Int(1, 0)));
        Assert.That(Vector2Int.Down, Is.EqualTo(new Vector2Int(0, -1)));
        Assert.That(Vector2Int.Left, Is.EqualTo(new Vector2Int(-1, 0)));
    }

    [Test]
    public void Add_ReturnsComponentWiseSum()
    {
        Prop.ForAll<(int x1, int y1, int x2, int y2)>(data =>
        {
            // Arrange
            var a = new Vector2Int(data.x1, data.y1);
            var b = new Vector2Int(data.x2, data.y2);

            // Act
            var result = a + b;

            // Assert
            var expected = new Vector2Int(
                data.x1 + data.x2,
                data.y1 + data.y2);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Subtract_ReturnsComponentWiseDifference()
    {
        Prop.ForAll<(int x1, int y1, int x2, int y2)>(data =>
        {
            // Arrange
            var a = new Vector2Int(data.x1, data.y1);
            var b = new Vector2Int(data.x2, data.y2);

            // Act
            var result = a - b;

            // Assert
            var expected = new Vector2Int(
                data.x1 - data.x2,
                data.y1 - data.y2);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Multiply_ReturnsScaledVector()
    {
        Prop.ForAll<(int x, int y, int scalar)>(data =>
        {
            // Arrange
            var v = new Vector2Int(data.x, data.y);

            // Act
            var result = v * data.scalar;

            // Assert
            var expected = new Vector2Int(
                data.x * data.scalar,
                data.y * data.scalar);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Divide_ReturnsDividedVector()
    {
        Prop.ForAll<(int x, int y, int scalar)>(data =>
        {
            if (data.scalar == 0)
                return true;

            // Arrange
            var v = new Vector2Int(data.x, data.y);

            // Act
            var result = v / data.scalar;

            // Assert
            var expected = new Vector2Int(
                data.x / data.scalar,
                data.y / data.scalar);

            return result == expected;
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Divide_ByZero_Throws()
    {
        // Arrange
        var v = new Vector2Int(1, 1);

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
        var a = new Vector2Int(5, -7);
        var b = new Vector2Int(5, -7);

        // Act & Assert
        Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
    }

    [Test]
    public void Equals_WorksCorrectly()
    {
        // Arrange
        var a = new Vector2Int(1, 2);
        var b = new Vector2Int(1, 2);
        var c = new Vector2Int(2, 1);

        // Act & Assert
        Assert.That(a.Equals(b), Is.True);
        Assert.That(a.Equals(c), Is.False);
        Assert.That(a.Equals((object)b), Is.True);
        Assert.That(a.Equals("not a vector"), Is.False);
    }

    [Test]
    public void GetHashCode_SameValues_ReturnsSameHash()
    {
        // Arrange
        var a = new Vector2Int(5, -7);
        var b = new Vector2Int(5, -7);

        // Act & Assert
        Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
    }

    [Test]
    public void ToString_ReturnsFormattedVector()
    {
        Prop.ForAll<int, int>((x, y) =>
        {
            var v = new Vector2Int(x, y);

            return v.ToString() == $"({x}, {y})";
        }).QuickCheckThrowOnFailure();
    }
}