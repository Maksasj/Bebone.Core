using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Camera;
using Bebone.Graphics.RenderGraph;
using System.Drawing;

namespace Bebone.Graphics.Renderer
{
    public class Renderer
    {
        private readonly List<IDrawTask<int>> MainPassTasks;
        private readonly List<IDrawTask<int>> UIPassTasks;

        public PerspectiveCamera PerspectiveCamera { get; init; }
        public OrthographicCamera OrthographicCamera { get; init; }

        private readonly FrameGraph frameGraph;

        public Renderer(IGLContext context)
        {
            MainPassTasks = [];
            UIPassTasks = [];

            PerspectiveCamera = new PerspectiveCamera();
            OrthographicCamera = new OrthographicCamera(0, 1920, 1080, 0, -1.0f, 1.0f);

            frameGraph = CreateFrameGraph(context);
        }

        private FrameGraph CreateFrameGraph(IGLContext context)
        {
            var graph = new FrameGraph();

            graph.AddPass(new RenderTask<int>(
                (resources) => 0,
                (shader) =>
                {
                    context.ClearColor(Color.FromArgb(255, 135, 206, 235));
                    context.ClearBuffers();
                }));

            graph.AddPass(new RenderTask<int>(
                (resources) => 0,
                (shader) =>
                {
                    context.EnableDepthTest();
                }));

            graph.AddPass(CreateRenderQueuePass(context, null, PerspectiveCamera, MainPassTasks));

            graph.AddPass(new RenderTask<int>(
                (resources) => 0,
                (shader) =>
                {
                    context.DisableDepthTest();
                }));

            graph.AddPass(CreateRenderQueuePass(context, null, OrthographicCamera, UIPassTasks));

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

        private static IPass CreateRenderQueuePass(IGLContext context, IShaderProgram? shader, ICamera camera, List<IDrawTask<int>> renderQueue)
        {
            return new RenderTask<int>(
                (resources) => 0,
                (data) =>
                {
                    context.SetViewport(0, 0, 1920, 1080);

                    shader!.Activate();
                    shader!.SetUniform("cam", camera.GetViewMatrix() * camera.GetProjectionMatrix((float)1920 / (float)1080));

                    foreach (var task in renderQueue)
                        task.Execute(data);
                });
        }
    }
}
