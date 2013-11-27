using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using HyperSpace.Core.Rendering;
using OpenTK.Graphics.OpenGL4;

namespace HyperSpace.Core.Scenes.Tests {
  class BlackScreenTest : Scene {
    private Rendering.Shader shader;
    
    private string UNIFORM_MODEL_VIEW = "u_model_view";
    private string UNIFORM_PROJECTION = "u_projection_view";
    

    private Matrix4 mviewdata;
    private Matrix4 projectionMatrix;
    private Matrix4 viewMatrix;
    private Matrix4 combinedMatrix;

    float angle = 0.0f;
    private Mesh mesh;
    private VertexBufferObject rawMesh;
    private IndexBufferObject rawIndicies;

    public void onEnter() {
      this.shader            = Game.assets.shader("test");
      VertexAttributes attrs = new VertexAttributes(VertexAttribute.Position(), VertexAttribute.Color());
      this.rawMesh           = new VertexBufferObject(true, 3, 7, attrs);
      this.rawIndicies       = new IndexBufferObject(true, 3);
      float[] data = new float[] {
        -0.8f, -0.8f, 0f,  1f, 0f, 0f, 1.0f,
        0.8f, -0.8f, 0f,   0f, 1f, 0f, 1.0f,
        0f,  0.8f, 0f,     0f,  0f, 1f, 1.0f,
        0.8f,  0.8f, 0f,     1f,  0f, 1f, 1.0f
      };

      uint[] indicies = new uint[] {
        0, 1, 2,
        1, 2, 3
      };

      this.rawIndicies.setIndicies(ref indicies);
      this.rawMesh.setVerticies(ref data);

      /*this.positionVBO = new VBO();
      this.colorVBO    = new VBO();

      this.mesh = new Mesh(OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw, OpenTK.Graphics.OpenGL.PrimitiveType.Triangles);

      Vector3[] vertdata = new Vector3[] { new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f),
                new Vector3( 0f,  0.8f, 0f)};
      PositionAttribute vertexs = new PositionAttribute(vertdata.Length);
      vertexs.set(ref vertdata);
      //this.mesh.addAttribute(ref vertexs);

      Vector4[] coldata = new Vector4[] { new Vector4(1f, 0f, 0f, 1.0f),
                new Vector4( 0f, 1f, 0f, 1.0f),
                new Vector4( 0f,  0f, 1f, 1.0f)};

      this.colors = new ColorAttribute(coldata.Length);
      this.colors.set(ref coldata);
      //this.mesh.addAttribute(ref vertexs);

      mviewdata = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);*/
    }

    public void resize() {
      float aspect_ratio = Game.shared.width / (float)Game.shared.height;
      this.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 60);
      this.viewMatrix = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -4.0f));
      this.combinedMatrix = Matrix4.Mult(this.viewMatrix, this.projectionMatrix);
    }

    public void update(double delta) {
      //angle += 1f * (float)delta;
      Matrix4.CreateRotationY(angle, out mviewdata);
    }

    public void render() {
      GL.Viewport(0, 0, Game.shared.width, Game.shared.height);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      GL.ClearColor(0f, 0f, 0.0f, 1.0f);
      GL.Enable(EnableCap.DepthTest);

      shader.use();
      shader.uniformMatrix4(UNIFORM_MODEL_VIEW, false, ref mviewdata);
      shader.uniformMatrix4(UNIFORM_PROJECTION, false, ref combinedMatrix);

      this.rawMesh.bind(ref shader);
      this.rawIndicies.bind();
      //GL.DrawArrays(PrimitiveType.Triangles, 0, this.rawMesh.numVertices); //Without indicies
      GL.DrawElements(BeginMode.Triangles, this.rawIndicies.size, DrawElementsType.UnsignedInt, 0);
      
      this.rawIndicies.unbind();
      this.rawMesh.unbind(ref shader);
      GL.Flush();
      /*
      //int position = shader.attribute(vertexs.attrName);
      int color    = shader.attribute(colors.attrName);

      positionVBO.bind();
      
      GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, vertexs.size(), vertexs.get(), BufferUsageHint.StaticDraw);
      //GL.VertexAttribPointer(position, vertexs.attrSize, vertexs.pointerType, false, 0, 0);
      GL.EnableVertexAttribArray(position);

      colorVBO.bind();
      GL.BufferData<Vector4>(BufferTarget.ArrayBuffer, colors.size(), colors.get(), BufferUsageHint.StaticDraw);
      //GL.VertexAttribPointer(color, colors.attrSize, vertexs.pointerType, true, 0, 0);
      GL.EnableVertexAttribArray(color);

      

      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

      GL.DisableVertexAttribArray(position);
      GL.DisableVertexAttribArray(color);

      GL.Flush();*/
    }

    public void onExit() {
      
    }

    public void dispose() {
    }
  }
}
