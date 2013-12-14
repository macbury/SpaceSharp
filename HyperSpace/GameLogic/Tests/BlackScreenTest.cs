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
    private PerspecitveCamera camera;

    public void onEnter() {
      this.camera            = new PerspecitveCamera(MathHelper.PiOver4, Game.shared.width, Game.shared.height);
      this.shader            = Game.assets.shader("test");
      VertexAttributes attrs = new VertexAttributes(VertexAttribute.Position(), VertexAttribute.Color());
      this.rawMesh           = new VertexBufferObject(true, 3, 7, attrs);
      this.rawIndicies       = new IndexBufferObject(true);

      float[] data = new float[] {
        -0.8f, -0.8f, 0f,  1f, 0f, 0f, 1.0f,
        0.8f, -0.8f, 0f,   0f, 1f, 0f, 1.0f,
        0f,  0.8f, 0f,     0f,  0f, 1f, 1.0f,
        0.8f,  0.8f, 0f,   1f,  0f, 1f, 1.0f
      };

      uint[] indicies = new uint[] {
        0, 1, 2,
        1, 2, 3
      };

      this.rawIndicies.setIndicies(ref indicies);
      this.rawMesh.setVerticies(ref data);

      this.mviewdata = Matrix4.Translation(0f, 0f, 0f);
      Vector3 cameraPosition = new Vector3(5f, 5f, 5f);
      this.camera.translate(ref cameraPosition);

      Vector3 target = new Vector3(0f, 0f, 0f);
      this.camera.lookAt(ref target);
    }

    public void resize() {
      float aspect_ratio = Game.shared.width / (float)Game.shared.height;
      
      this.projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 60);
      this.viewMatrix       = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -10.0f));
      this.combinedMatrix   = Matrix4.Mult(this.viewMatrix, this.projectionMatrix);

      this.camera.viewportWidth  = Game.shared.width;
      this.camera.viewportHeight = Game.shared.height;
    }

    public void update(double delta) {
      angle += 1f * (float)delta;
      Matrix4.CreateRotationY(angle, out mviewdata);
    }

    public void render() {
      this.camera.update();

      GL.Enable(EnableCap.DepthTest);

      shader.use();
      shader.uniformMatrix4(UNIFORM_MODEL_VIEW, false, ref mviewdata);
      shader.uniformMatrix4(UNIFORM_PROJECTION, false, ref camera.combined);

      this.rawMesh.bind(ref shader);
      this.rawIndicies.bind();
      //GL.DrawArrays(PrimitiveType.Triangles, 0, this.rawMesh.numVertices); //Without indicies
      GL.DrawElements(BeginMode.Triangles, this.rawIndicies.size, DrawElementsType.UnsignedInt, 0);
      
      this.rawIndicies.unbind();
      this.rawMesh.unbind(ref shader);
      GL.Flush();
    }

    public void onExit() {
      
    }

    public void dispose() {
      this.rawIndicies.dispose();
      this.rawMesh.dispose();
    }
  }
}
