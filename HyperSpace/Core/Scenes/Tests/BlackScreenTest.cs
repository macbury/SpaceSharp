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
    private VBO positionVBO;
    private VBO colorVBO;

    private Vector3[] vertdata;
    private Vector3[] coldata;

    private Matrix4 mviewdata;
    private Matrix4 projectionMatrix;
    private Matrix4 viewMatrix;
    private Matrix4 combinedMatrix;

    float angle = 0.0f;

    public void onEnter() {
      this.shader      = Game.assets.shader("test");
      this.positionVBO = new VBO();
      this.colorVBO    = new VBO();
      vertdata = new Vector3[] { new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f),
                new Vector3( 0f,  0.8f, 0f)};


      coldata = new Vector3[] { new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f,  1f, 0f)};


      mviewdata = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
    }

    public void resize() {
      float aspect_ratio = Game.shared.width / (float)Game.shared.height;
      this.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 60);
      this.viewMatrix = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -4.0f));
      this.combinedMatrix = Matrix4.Mult(this.viewMatrix, this.projectionMatrix);
    }

    public void update(double delta) {
      angle += 1f * (float)delta;
      Matrix4.CreateRotationY(angle, out mviewdata);
    }

    public void render() {
      GL.Viewport(0, 0, Game.shared.width, Game.shared.height);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      GL.ClearColor(0f, 0f, 0.0f, 1.0f);
      GL.Enable(EnableCap.DepthTest);

      shader.use();
      int position = shader.attribute("a_position");
      int color = shader.attribute("a_color");

      positionVBO.bind();
      GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
      GL.VertexAttribPointer(position, 3, VertexAttribPointerType.Float, false, 0, 0);

      colorVBO.bind();
      GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
      GL.VertexAttribPointer(color, 3, VertexAttribPointerType.Float, true, 0, 0);

      shader.uniformMatrix4(UNIFORM_MODEL_VIEW, false, ref mviewdata);
      shader.uniformMatrix4(UNIFORM_PROJECTION, false, ref combinedMatrix);
      
      GL.EnableVertexAttribArray(position);
      GL.EnableVertexAttribArray(color);

      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

      GL.DisableVertexAttribArray(position);
      GL.DisableVertexAttribArray(color);

      GL.Flush();
    }

    public void onExit() {
      
    }

    public void dispose() {
      this.positionVBO.dispose();
      this.colorVBO.dispose();
    }
  }
}
