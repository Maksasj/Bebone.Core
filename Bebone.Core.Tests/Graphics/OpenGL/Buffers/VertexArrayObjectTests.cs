using Bebone.Graphics.Abstractions;
using Bebone.Graphics.OpenGL.Buffers;
using Moq;

namespace Bebone.Core.Tests.Graphics.OpenGL.Buffers;

[TestFixture]
public class VertexArrayObjectTests
{
    [Test]
    public void Constructor_ExecuteGenVertexArrayOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();
        mockGL
            .Setup(s => s.GenVertexArray())
            .Returns(123);

        // Act
        var array = new VertexArrayObject(mockGL.Object);

        // Assert
        mockGL.Verify(s => s.GenVertexArray(), Times.Once);
    }

    [Test]
    public void Bind_ExecuteBindVertexArrayOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenVertexArray())
            .Returns(123);

        mockGL
            .Setup(s => s.BindVertexArray(123));

        var array = new VertexArrayObject(mockGL.Object);

        // Act
        array.Bind();

        // Assert
        mockGL.Verify(s => s.BindVertexArray(123), Times.Once);
    }

    [Test]
    public void Unbind_ExecuteUnbindBufferOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenVertexArray())
            .Returns(123);

        mockGL
            .Setup(s => s.BindVertexArray(0));

        var array = new VertexArrayObject(mockGL.Object);

        // Act
        array.Unbind();

        // Assert
        mockGL.Verify(s => s.BindVertexArray(0), Times.Once);
    }

    [Test]
    public void Dispose_ExecuteDeleteVertexArrayOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenVertexArray())
            .Returns(123);

        mockGL
            .Setup(s => s.DeleteVertexArray(123));

        var array = new VertexArrayObject(mockGL.Object);

        // Act
        array.Dispose();

        // Assert
        mockGL.Verify(s => s.DeleteVertexArray(123), Times.Once);
    }

    [Test]
    public void MultipleDispose_ExecuteDeleteVertexArrayOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenVertexArray())
            .Returns(123);

        mockGL
            .Setup(s => s.DeleteVertexArray(123));

        var array = new VertexArrayObject(mockGL.Object);

        // Act
        array.Dispose();
        array.Dispose();
        array.Dispose();

        // Assert
        mockGL.Verify(s => s.DeleteVertexArray(123), Times.Once);
    }
}