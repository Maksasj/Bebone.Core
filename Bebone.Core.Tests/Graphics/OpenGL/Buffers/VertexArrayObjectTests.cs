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
        var buffer = new VertexArrayObject(mockGL.Object);

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

        var buffer = new VertexArrayObject(mockGL.Object);

        // Act
        buffer.Bind();

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

        var buffer = new VertexArrayObject(mockGL.Object);

        // Act
        buffer.Unbind();

        // Assert
        mockGL.Verify(s => s.BindVertexArray(0), Times.Once);
    }
}