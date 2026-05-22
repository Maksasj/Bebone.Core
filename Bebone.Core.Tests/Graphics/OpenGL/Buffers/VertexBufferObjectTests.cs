using Bebone.Graphics.OpenGL.Buffers;
using Moq;
using Silk.NET.OpenGL;

namespace Bebone.Core.Tests.Graphics.OpenGL.Buffers;

[TestFixture]
public class VertexBufferObjectTests
{
    [Test]
    public void Constructor_CallGenBufferOnce()
    {
        // Arrange
        var mockGL = new Mock<GL>();
        mockGL
            .Setup(s => s.GenBuffer())
            .Returns(123);

        // Act
        var buffer = new VertexBufferObject(mockGL.Object);

        // Assert
        mockGL.Verify(s => s.GenBuffer(), Times.Once);
    }
}
