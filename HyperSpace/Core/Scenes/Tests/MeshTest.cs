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
    private VertexBufferObject cubeVertexObject;
    private IndexBufferObject cubeIndexObject;
    private Matrix4 mviewdata;
    float angle = 0.0f;
    private Mesh mesh;

    public void onEnter() {
      this.shader         = Game.assets.shader("test");
      this.camera         = new PerspecitveCamera();
      this.cameraPosition = new Vector3(5f, 5f, -10f);
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
      this.cubeVertexObject  = new VertexBufferObject(true, 3, 7, attrs);
      this.cubeIndexObject   = new IndexBufferObject(true);

      this.mesh              = new Mesh(true, 7, 8, attrs);

      this.cubeVertexObject.setVerticies(ref vertData);
      this.cubeIndexObject.setIndicies(ref indicedata);

      this.mviewdata = Matrix4.CreateTranslation(Vector3.Zero);
    }

    public void resize() {
      this.camera.resize(Game.shared.width, Game.shared.height);
    }

    public void update(double delta) {
      this.camera.update();
      angle += 1f * (float)delta;
      Matrix4.CreateRotationY(angle, out mviewdata);
    }

    public void render() {
      GL.Enable(EnableCap.DepthTest);

      this.shader.begin();
        this.shader.projectUsingCamera(ref this.camera);
        this.shader.uniformMatrix4(Shader.MODEL_VIEW_UNIFORM, false, ref mviewdata);

        this.cubeVertexObject.bind(ref shader);
          this.cubeIndexObject.bind();
            GL.DrawElements(BeginMode.Triangles, this.cubeIndexObject.size, DrawElementsType.UnsignedInt, 0);
          this.cubeIndexObject.unbind();
        this.cubeVertexObject.unbind(ref shader);
      this.shader.end();

      GL.Flush();
    }

    public void onExit() {
      
    }

    public void dispose() {
      this.cubeVertexObject.dispose();
      this.cubeIndexObject.dispose();

      this.mesh.dispose();
    }
  }
}
