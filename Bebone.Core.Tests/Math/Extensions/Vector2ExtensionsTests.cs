using System.Numerics;
using Bebone.Math.Extensions;
using FsCheck;
using FsCheck.Fluent;

namespace Bebone.Core.Tests.Math.Extensions;

[TestFixture]
public class Vector2ExtensionsTests
{
    [Test]
    public void WithX_ReplacesXComponent()
    {
        Prop.ForAll<float, float, float>((x, y, newX) =>
        {
            // Arrange
            var v = new Vector2(x, y);

            // Act
            var result = v.WithX(newX);

            // Assert
            return result.X.Equals(newX)
                && result.Y.Equals(y);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithY_ReplacesYComponent()
    {
        Prop.ForAll<float, float, float>((x, y, newY) =>
        {
            // Arrange
            var v = new Vector2(x, y);

            // Act
            var result = v.WithY(newY);

            // Assert
            return result.X.Equals(x)
                && result.Y.Equals(newY);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithX_DoesNotMutateOriginal()
    {
        Prop.ForAll<float, float, float>((x, y, newX) =>
        {
            // Arrange
            var v = new Vector2(x, y);

            // Act
            _ = v.WithX(newX);

            // Assert
            return v.X.Equals(x)
                && v.Y.Equals(y);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithY_DoesNotMutateOriginal()
    {
        Prop.ForAll<float, float, float>((x, y, newY) =>
        {
            // Arrange
            var v = new Vector2(x, y);

            // Act
            _ = v.WithY(newY);

            // Assert
            return v.X.Equals(x)
                && v.Y.Equals(y);
        }).QuickCheckThrowOnFailure();
    }
}
