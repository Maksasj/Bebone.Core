using Bebone.Graphics.Abstractions;
using Bebone.Graphics.OpenGL.Buffers;
using Moq;
using Silk.NET.OpenGL;

namespace Bebone.Core.Tests.Graphics.OpenGL.Buffers;

[TestFixture]
public class ElementBufferObjectTests
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
        var buffer = new ElementBufferObject(mockGL.Object);

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
            .Setup(s => s.BindBuffer(BufferTargetARB.ElementArrayBuffer, 123));

        var buffer = new ElementBufferObject(mockGL.Object);

        // Act
        buffer.Bind();

        // Assert
        mockGL.Verify(s => s.BindBuffer(BufferTargetARB.ElementArrayBuffer, 123), Times.Once);
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
            .Setup(s => s.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0));

        var buffer = new ElementBufferObject(mockGL.Object);

        // Act
        buffer.Unbind();

        // Assert
        mockGL.Verify(s => s.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0), Times.Once);
    }
}
