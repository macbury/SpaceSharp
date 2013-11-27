using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes.Tests {
  class ColoredTriangleRenderScene : Scene {
    int pgmID;
    int vsID;
    int fsID;

    int attribute_vcol;
    int attribute_vpos;
    int uniform_mview;
    int uniform_projection;
    int vbo_position;
    int vbo_color;
    int vbo_mview;

    float angle = 0.0f;

    Vector3[] vertdata;
    Vector3[] coldata;
    Matrix4 mviewdata;

    private Matrix4 projectionMatrix;
    private Matrix4 viewMatrix;
    private Matrix4 combinedMatrix;
    public static string TAG = "ColoredTriangleRenderScene";

    public void onEnter() {
      pgmID = GL.CreateProgram();
      loadShader("Res/test.vert", ShaderType.VertexShader, pgmID, out vsID);
      loadShader("Res/test.frag", ShaderType.FragmentShader, pgmID, out fsID);

      GL.LinkProgram(pgmID);
      Game.logger.info(TAG, "Compiled program {0}", pgmID);
      Game.logger.info(TAG, GL.GetProgramInfoLog(pgmID));

      attribute_vpos = GL.GetAttribLocation(pgmID, "a_position");
      attribute_vcol = GL.GetAttribLocation(pgmID, "a_color");
      uniform_projection = GL.GetUniformLocation(pgmID, "u_projection_view");
      uniform_mview = GL.GetUniformLocation(pgmID, "u_model_view");

      if (attribute_vpos == -1 || attribute_vcol == -1 || uniform_mview == -1 || uniform_projection == -1) {
        Game.logger.info(TAG, "Error binding attributes");
      }

      GL.GenBuffers(1, out vbo_position);
      GL.GenBuffers(1, out vbo_color);
      GL.GenBuffers(1, out vbo_mview);

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

      GL.UseProgram(pgmID);

      GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
      GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
      GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

      GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
      GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
      GL.VertexAttribPointer(attribute_vcol, 3, VertexAttribPointerType.Float, true, 0, 0);

      GL.UniformMatrix4(uniform_mview, false, ref mviewdata);
      GL.UniformMatrix4(uniform_projection, false, ref combinedMatrix);

      GL.EnableVertexAttribArray(attribute_vpos);
      GL.EnableVertexAttribArray(attribute_vcol);

      GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

      GL.DisableVertexAttribArray(attribute_vpos);
      GL.DisableVertexAttribArray(attribute_vcol);

      GL.Flush();
    }

    public void onExit() {
    }

    public void dispose() {
      Game.logger.info(TAG, "Remvoing shit");
      GL.DeleteBuffer(vbo_position);
      GL.DeleteBuffer(vbo_color);
      GL.DeleteBuffer(vbo_mview);
      GL.DeleteShader(pgmID);
    }

    void loadShader(String filename, ShaderType type, int program, out int address) {
      address = GL.CreateShader(type);
      using (StreamReader sr = new StreamReader(filename)) {
        GL.ShaderSource(address, sr.ReadToEnd());
      }
      GL.CompileShader(address);
      GL.AttachShader(program, address);
      Debug.WriteLine(GL.GetShaderInfoLog(address));

      int compileResult;
      GL.GetShader(address, ShaderParameter.CompileStatus, out compileResult);
      if (compileResult != 1) {
        Game.logger.info(TAG, "Compile Error!");
      }
      Game.logger.info(TAG, "Compile result: " + compileResult);
    }
  }
}
