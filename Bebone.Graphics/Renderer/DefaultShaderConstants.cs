namespace Bebone.Graphics.Renderer;

public static class DefaultShaderConstants
{
    public const string DefaultVertexShader = """
        #version 330 core
        layout(location = 0) in vec3 aPosition;
        layout(location = 1) in vec3 aNormal;
        layout(location = 2) in vec2 aTexCoords;

        uniform mat4 cam;
        uniform mat4 model;

        out vec2 texCoords;
        out vec3 normal;

        void main()
        {
            gl_Position = cam * model * vec4(aPosition, 1.0);
            texCoords = aTexCoords;
            normal = mat3(transpose(inverse(model))) * aNormal;
        }
        """;

    public const string DefaultFragmentShader = """
        #version 330 core
        in vec2 texCoords;
        in vec3 normal;

        out vec4 fragColor;

        void main()
        {
            fragColor = vec4(normal * 0.5 + 0.5, 1.0);
        }
        """;
}