using Bebone.Graphics.Abstractions;
using Bebone.Graphics.Abstractions.Mesh;
using Bebone.Graphics.MeshGeneration;
using Bebone.Graphics.OpenGL;
using Bebone.Math;
using System.Drawing;
using System.Numerics;

namespace Bebone.Graphics.Renderer;

public class RendererDispatcher(Renderer renderer, IGraphicsFactory factory)
{
    private readonly Renderer _renderer = renderer;

    // Pregenerate often used resources
    private readonly IMesh<Vertex> _cubeMesh = new CubeMeshGenerator().GenerateMesh(factory.CreateMeshBuilder<Vertex>());
    private readonly IMesh<Vertex> _quadMesh = new QuadMeshGenerator().GenerateMesh(factory.CreateMeshBuilder<Vertex>());
    private readonly IMesh<Vertex> _icosahedron = new IcosahedronMeshGenerator().GenerateMesh(factory.CreateMeshBuilder<Vertex>());
    private readonly IMesh<Vertex> _sphereMesh = new IcosphereMeshGenerator(1.0f, 4).GenerateMesh(factory.CreateMeshBuilder<Vertex>());
    private readonly ITexture2D _whiteTexture = factory.CreateFlatColorTexture(Color.White, new TextureConfiguration(Width: 16, Height: 16));

    public void DrawRectangle(int posX, int posY, int width, int height, ITexture2D? texture = null, Color? tint = null)
    {
        var size = new Vector3(width, height, 1.0f);
        var position = new Vector3(posX + width * 0.5f, posY + height * 0.5f, 0.0f);

        _renderer.SubmitUi(new MeshDrawTask()
        {
            Mesh = _quadMesh,
            Transform = new Transform(position, Quaternion.Identity, size),
            Texture = texture ?? _whiteTexture,
            Tint = tint ?? Color.White
        });
    }

    public void DrawQuad(Vector3 position, Quaternion rotation, Vector3 scale, ITexture2D? texture = null, Color? tint = null)
    {
        SubmitMainGeometry(_quadMesh, position, rotation, scale, texture, tint);
    }

    public void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale, ITexture2D? texture = null, Color? tint = null)
    {
        SubmitMainGeometry(_cubeMesh, position, rotation, scale, texture, tint);
    }

    public void DrawIcosahedron(Vector3 position, Quaternion rotation, Vector3 scale, ITexture2D? texture = null, Color? tint = null)
    {
        SubmitMainGeometry(_icosahedron, position, rotation, scale, texture, tint);
    }

    public void DrawSphere(Vector3 position, Quaternion rotation, Vector3 scale, ITexture2D? texture = null, Color? tint = null)
    {
        SubmitMainGeometry(_sphereMesh, position, rotation, scale, texture, tint);
    }

    private void SubmitMainGeometry(IMesh<Vertex> mesh, Vector3 position, Quaternion rotation, Vector3 scale, ITexture2D? texture = null, Color? tint = null)
    {
        _renderer.SubmitMain(new MeshDrawTask()
        {
            Mesh = mesh,
            Transform = new Transform(position, rotation, scale),
            Texture = texture ?? _whiteTexture,
            Tint = tint ?? Color.White
        });
    }
}