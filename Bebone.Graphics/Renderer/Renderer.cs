using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;
using System.Drawing;

namespace Bebone.Graphics.Renderer
{
    public class Renderer
    {
        private const string DefaultVertexShader = """
            #version 330 core
            layout(location = 0) in vec3 aPosition;
            layout(location = 1) in vec3 aNormal;
            layout(location = 2) in vec2 aTexCoords;

            uniform mat4 cam;

            out vec2 texCoords;
            out vec3 normal;

            void main()
            {
                gl_Position = cam * vec4(aPosition, 1.0);
                texCoords = aTexCoords;
                normal = aNormal;
            }
            """;

        private const string DefaultFragmentShader = """
            #version 330 core
            in vec2 texCoords;
            in vec3 normal;

            out vec4 fragColor;

            void main()
            {
                fragColor = vec4(normal * 0.5 + 0.5, 1.0);
            }
            """;

        private readonly List<IDrawTask<int>> MainPassTasks;
        private readonly List<IDrawTask<int>> UIPassTasks;

        public PerspectiveCamera PerspectiveCamera { get; init; }
        public OrthographicCamera OrthographicCamera { get; init; }

        private readonly FrameGraph frameGraph;
        private readonly IShaderProgram shaderProgram;

        public Renderer(IGLContext context, IGraphicsFactory factory)
        {
            MainPassTasks = [];
            UIPassTasks = [];

            PerspectiveCamera = new PerspectiveCamera();
            OrthographicCamera = new OrthographicCamera(0, 1920, 1080, 0, -1.0f, 1.0f);

            shaderProgram = factory.CreateShader(DefaultVertexShader, DefaultFragmentShader);

            frameGraph = CreateFrameGraph(context);
        }

        private FrameGraph CreateFrameGraph(IGLContext context)
        {
            var graph = new FrameGraph();

            // Clear & Clear buffers
            graph.AddPass(new RenderTask<int>(
                (resources) => 0,
                (shader) =>
                {
                    context.ClearColor(Color.FromArgb(255, 135, 206, 235));
                    context.ClearBuffers();
                }));

            graph.AddPass(new RenderQueuePass(context, PerspectiveCamera, shaderProgram, MainPassTasks, true));
            graph.AddPass(new RenderQueuePass(context, OrthographicCamera, shaderProgram, UIPassTasks, false));

            // Clear queues
            graph.AddPass(new RenderTask<int>(
                (resources) => 0,
                (shader) =>
                {
                    MainPassTasks.Clear();
                    UIPassTasks.Clear();
                }));

            graph.Compile();

            return graph;
        }

        public void Execute() => frameGraph.Execute();
    }
}
