namespace Bebone.Sample
{
    public class Application
    {
        private const string VertexCode = @"
#version 450

layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;

layout(binding = 0) uniform TransformBuffer
{
    mat4 Transform;
};

layout(location = 0) out vec4 fsin_Color;

void main()
{
    vec4 position = Transform * vec4(Position, 0, 1);
    gl_Position = position;
    fsin_Color = Color;
}";

        private const string FragmentCode = @"
#version 450

layout(location = 0) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = fsin_Color;
}";
        public Application()
        {

        }

        public void PreInit()
        {

        }

        public void Init()
        {

        }

        public void Load()
        {

        }

        public void Run()
        {

        }

        public void Unload()
        {

        }
    }
}
