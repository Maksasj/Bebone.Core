using Bebone.Graphics.Abstractions;
using Bebone.Graphics.OpenGL.Buffers;
using Moq;
using Silk.NET.OpenGL;

namespace Bebone.Core.Tests.Graphics.OpenGL.Buffers;

[TestFixture]
public class VertexBufferObjectTests
{
    [Test]
    public void Constructor_ExecuteGenBufferOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();
        mockGL
            .Setup(s => s.GenBuffer())
            .Returns(123);

        // Act
        var buffer = new VertexBufferObject(mockGL.Object);

        // Assert
        mockGL.Verify(s => s.GenBuffer(), Times.Once);
    }

    [Test]
    public void Bind_ExecuteBindBufferOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenBuffer())
            .Returns(123);

        mockGL
            .Setup(s => s.BindBuffer(BufferTargetARB.ArrayBuffer, 123));

        var buffer = new VertexBufferObject(mockGL.Object);

        // Act
        buffer.Bind();

        // Assert
        mockGL.Verify(s => s.BindBuffer(It.IsAny<BufferTargetARB>(), 123), Times.Once);
    }

    [Test]
    public void Unbind_ExecuteUnbindBufferOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenBuffer())
            .Returns(123);

        mockGL
            .Setup(s => s.BindBuffer(BufferTargetARB.ArrayBuffer, 0));

        var buffer = new VertexBufferObject(mockGL.Object);

        // Act
        buffer.Unbind();

        // Assert
        mockGL.Verify(s => s.BindBuffer(It.IsAny<BufferTargetARB>(), 0), Times.Once);
    }

    [Test]
    public void Dispose_ExecuteDisposeOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenBuffer())
            .Returns(123);

        mockGL
            .Setup(s => s.DeleteBuffer(123));

        var buffer = new VertexBufferObject(mockGL.Object);

        // Act
        buffer.Dispose();

        // Assert
        mockGL.Verify(s => s.DeleteBuffer(123), Times.Once);
    }

    [Test]
    public void MultipleDispose_ExecuteDisposeOnce()
    {
        // Arrange
        var mockGL = new Mock<IGLContext>();

        mockGL
            .Setup(s => s.GenBuffer())
            .Returns(123);

        mockGL
            .Setup(s => s.DeleteBuffer(123));

        var buffer = new VertexBufferObject(mockGL.Object);

        // Act
        buffer.Dispose();
        buffer.Dispose();
        buffer.Dispose();

        // Assert
        mockGL.Verify(s => s.DeleteBuffer(123), Times.Once);
    }
}
