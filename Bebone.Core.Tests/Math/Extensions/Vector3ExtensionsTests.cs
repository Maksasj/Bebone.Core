using System.Numerics;
using Bebone.Math.Extensions;
using FsCheck;
using FsCheck.Fluent;

namespace Bebone.Core.Tests.Math.Extensions;

[TestFixture]
public class Vector3ExtensionsTests
{
    [Test]
    public void WithX_ReplacesXComponent()
    {
        Prop.ForAll<(float x, float y, float z, float newX)>(data =>
        {
            // Arrange
            var v = new Vector3(data.x, data.y, data.z);

            // Act
            var result = v.WithX(data.newX);

            // Assert
            return result.X.Equals(data.newX)
                && result.Y.Equals(data.y)
                && result.Z.Equals(data.z);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithY_ReplacesYComponent()
    {
        Prop.ForAll<(float x, float y, float z, float newY)>(data =>
        {
            // Arrange
            var v = new Vector3(data.x, data.y, data.z);

            // Act
            var result = v.WithY(data.newY);

            // Assert
            return result.X.Equals(data.x)
                && result.Y.Equals(data.newY)
                && result.Z.Equals(data.z);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithZ_ReplacesZComponent()
    {
        Prop.ForAll<(float x, float y, float z, float newZ)>(data =>
        {
            // Arrange
            var v = new Vector3(data.x, data.y, data.z);

            // Act
            var result = v.WithZ(data.newZ);

            // Assert
            return result.X.Equals(data.x)
                && result.Y.Equals(data.y)
                && result.Z.Equals(data.newZ);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithX_DoesNotMutateOriginal()
    {
        Prop.ForAll<(float x, float y, float z, float newX)>(data =>
        {
            // Arrange
            var v = new Vector3(data.x, data.y, data.z);

            // Act
            _ = v.WithX(data.newX);

            // Assert
            return v.X.Equals(data.x)
                && v.Y.Equals(data.y)
                && v.Z.Equals(data.z);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithY_DoesNotMutateOriginal()
    {
        Prop.ForAll<(float x, float y, float z, float newY)>(data =>
        {
            // Arrange
            var v = new Vector3(data.x, data.y, data.z);

            // Act
            _ = v.WithY(data.newY);

            // Assert
            return v.X.Equals(data.x)
                && v.Y.Equals(data.y)
                && v.Z.Equals(data.z);
        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void WithZ_DoesNotMutateOriginal()
    {
        Prop.ForAll<(float x, float y, float z, float newZ)>(data =>
        {
            // Arrange
            var v = new Vector3(data.x, data.y, data.z);

            // Act
            _ = v.WithZ(data.newZ);

            // Assert
            return v.X.Equals(data.x)
                && v.Y.Equals(data.y)
                && v.Z.Equals(data.z);
        }).QuickCheckThrowOnFailure();
    }
}
