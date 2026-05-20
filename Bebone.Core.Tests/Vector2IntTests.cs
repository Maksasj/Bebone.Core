namespace Bebone.Core.Tests;

[TestFixture]
public class Vector2IntTests
{
    [Test]
    public void Constructor_SetsXAndY()
    {
        // Arrange & Act
        var v = new Vector2Int(2, -3);
        
        // Assert
        Assert.That(v.X, Is.EqualTo(2));
        Assert.That(v.Y, Is.EqualTo(-3));
    }

    [Test]
    public void Length_ReturnsMagnitude()
    {
        // Arrange & Act
        var v = new Vector2Int(3, 4);

        // Assert
        Assert.That(v.Length, Is.EqualTo(5f));
    }

    [Test]
    public void StaticVectors_ReturnExpectedValues()
    {
        // Arrange & Act & Assert
        Assert.That(Vector2Int.Zero, Is.EqualTo(new Vector2Int(0, 0)));
        Assert.That(Vector2Int.One, Is.EqualTo(new Vector2Int(1, 1)));
        Assert.That(Vector2Int.Up, Is.EqualTo(new Vector2Int(0, 1)));
        Assert.That(Vector2Int.Right, Is.EqualTo(new Vector2Int(1, 0)));
        Assert.That(Vector2Int.Down, Is.EqualTo(new Vector2Int(0, -1)));
        Assert.That(Vector2Int.Left, Is.EqualTo(new Vector2Int(-1, 0)));
    }

    [Test]
    public void Add_ReturnsSum()
    {
        // Arrange & Act & Assert
        Assert.That(new Vector2Int(2, 3) + new Vector2Int(4, -1),
            Is.EqualTo(new Vector2Int(6, 2)));
    }

    [Test]
    public void Subtract_ReturnsDifference()
    {
        // Arrange & Act & Assert
        Assert.That(new Vector2Int(2, 3) - new Vector2Int(4, -1),
            Is.EqualTo(new Vector2Int(-2, 4)));
    }

    [Test]
    public void Multiply_ReturnsScaledVector()
    {
        // Arrange & Act & Assert
        Assert.That(new Vector2Int(2, -3) * 4,
            Is.EqualTo(new Vector2Int(8, -12)));
    }

    [Test]
    public void Divide_ReturnsDividedVector()
    {
        // Arrange & Act & Assert
        Assert.That(new Vector2Int(8, -12) / 4,
            Is.EqualTo(new Vector2Int(2, -3)));
    }

    [Test]
    public void EqualityOperators_WorkCorrectly()
    {
        // Arrange
        var a = new Vector2Int(1, 2);
        var b = new Vector2Int(1, 2);
        var c = new Vector2Int(2, 1);

        // Act & Assert
        Assert.That(a == b, Is.True);
        Assert.That(a != c, Is.True);
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
        // Arrange
        var v = new Vector2Int(2, -3);

        // Act & Assert
        Assert.That(v.ToString(), Is.EqualTo("(2, -3)"));
    }
}