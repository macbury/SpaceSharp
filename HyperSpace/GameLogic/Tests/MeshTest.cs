using HyperSpace.Core.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes.Tests {
  class MeshTest : Scene {
    private PerspecitveCamera camera;
    private Vector3 cameraPosition;
    private Shader shader;
    private Matrix4 mviewdata;
    float angle = 0.0f;
    private Mesh mesh;
    private Vector3 posVector;
    private Matrix4 rotationMatrix;
    private Matrix4 finalMatrix;

    public void onEnter() {
      this.shader         = Game.assets.shader("test");
      this.camera         = new PerspecitveCamera();
      this.cameraPosition = new Vector3(0f, 5f, -20f);
      this.camera.translate(ref cameraPosition);

      Vector3 target = new Vector3(0f, 0f, 0f);
      this.camera.lookAt(ref target);

      float[] vertData = new float[] {
        -1f, -1f,  -1f,       0.0f, 1.0f, 1.0f, 1.0f,
        1.0f, -1.0f,  -1.0f,       1.0f, 0.0f, 1.0f, 1.0f,
        1.0f, 1.0f,  -1.0f,       1.0f, 1.0f, 0.0f, 1.0f,
        -1.0f, 1.0f,  -1.0f,       0.0f, 0.0f, 1.0f, 1.0f,
        -1.0f, -1.0f,  1.0f,       0.0f, 1.0f, 0.0f, 1.0f,
        1.0f, -1.0f,  1.0f,       0.0f, 0.0f, 1.0f, 1.0f,
        1.0f, 1.0f,  1.0f,       1.0f, 0.0f, 1.0f, 1.0f,
        -1.0f, 1.0f,  1.0f,       0.0f, 0.0f, 1.0f, 1.0f
      };

      uint[] indicedata = new uint[]{
          //front
          0, 7, 3,
          0, 4, 7,
          //back
          1, 2, 6,
          6, 5, 1,
          //left
          0, 2, 1,
          0, 3, 2,
          //right
          4, 5, 6,
          6, 7, 4,
          //top
          2, 3, 6,
          6, 3, 7,
          //bottom
          0, 1, 5,
          0, 5, 4
      };

      VertexAttributes attrs = new VertexAttributes(VertexAttribute.Position(), VertexAttribute.Color());
      this.mesh              = new Mesh(true, 8, 7, attrs);

      this.mesh.setVerticies(ref vertData);
      this.mesh.setIndicies(ref indicedata);

      this.posVector = Vector3.Zero;
      this.mviewdata = Matrix4.CreateTranslation(Vector3.Zero);
      this.rotationMatrix = Matrix4.CreateRotationX(0f);
      this.finalMatrix = new Matrix4();
    }

    public void resize() {
      this.camera.resize(Game.shared.width, Game.shared.height);
    }

    public void update(double delta) {
      this.camera.update();
      angle += 1f * (float)delta;
      Matrix4.CreateRotationY(angle, out rotationMatrix);
    }

    public void render() {
      GL.Enable(EnableCap.DepthTest);
      //GL.Enable(EnableCap.CullFace);
      //GL.CullFace(CullFaceMode.Back);

      this.shader.begin();
        this.shader.projectUsingCamera(ref this.camera);
        this.mesh.bind(ref shader);
          for (int i = -3; i < 3; i++) {
            posVector.X = i * 3.5f;
            Matrix4.CreateTranslation(ref posVector, out mviewdata);
            finalMatrix = rotationMatrix * mviewdata;
            this.shader.uniformMatrix4(Shader.MODEL_VIEW_UNIFORM, false, ref finalMatrix);
            this.mesh.render(PrimitiveType.Triangles, BeginMode.Triangles);
          }
        this.mesh.unbind(ref shader);
      this.shader.end();

      GL.Flush();
    }

    public void onExit() {
      
    }

    public void dispose() {
      this.mesh.dispose();
    }
  }
}
