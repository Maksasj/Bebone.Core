namespace Bebone.Core.Tests;

[TestFixture]
public class Vector3IntTests
{
    [Test]
    public void Constructor_SetsXYZ()
    {
        // Arrange & Act
        var v = new Vector3Int(2, -3, 5);

        // Assert
        Assert.That(v.X, Is.EqualTo(2));
        Assert.That(v.Y, Is.EqualTo(-3));
        Assert.That(v.Z, Is.EqualTo(5));
    }

    [Test]
    public void Length_ReturnsMagnitude()
    {
        // Arrange & Act
        var v = new Vector3Int(2, 3, 6);

        // Assert
        Assert.That(v.Length, Is.EqualTo(7f));
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
        // Arrange & Act
        var result = new Vector3Int(1, 2, 3) + new Vector3Int(4, -1, 2);

        // Assert
        Assert.That(result, Is.EqualTo(new Vector3Int(5, 1, 5)));
    }

    [Test]
    public void Subtract_ReturnsDifference()
    {
        // Arrange & Act
        var result = new Vector3Int(5, 4, 3) - new Vector3Int(1, 2, 1);

        // Assert
        Assert.That(result, Is.EqualTo(new Vector3Int(4, 2, 2)));
    }

    [Test]
    public void Multiply_ReturnsScaledVector()
    {
        // Arrange & Act
        var result = new Vector3Int(1, -2, 3) * 3;

        // Assert
        Assert.That(result, Is.EqualTo(new Vector3Int(3, -6, 9)));
    }

    [Test]
    public void Divide_ReturnsDividedVector()
    {
        // Arrange & Act
        var result = new Vector3Int(8, -12, 4) / 4;

        // Assert
        Assert.That(result, Is.EqualTo(new Vector3Int(2, -3, 1)));
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
        // Arrange
        var v = new Vector3Int(2, -3, 5);

        // Act & Assert
        Assert.That(v.ToString(), Is.EqualTo("(2, -3, 5)"));
    }
}