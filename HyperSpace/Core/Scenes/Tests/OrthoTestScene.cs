using HyperSpace.Core.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes.Tests {
  class OrthoTestScene : Scene {
    private Camera2D camera;
    private Matrix4 modelView;
    private Shader shader;
    private Mesh mesh;
    private Texture texture;

    public void onEnter() {
      this.shader = Game.assets.shader("texture");

      this.texture = Game.assets.texture("uvmap.png");

      this.camera = new Camera2D();
      Vector3 cameraPosition = new Vector3(0f, 0f, 1f);
      this.camera.translate(ref cameraPosition);
      this.mesh = MeshBuilder.generateTextureQuad();
      this.modelView = Matrix4.CreateScale(128f) * Matrix4.CreateTranslation(Vector3.Zero) ;

      Vector3 pos = new Vector3(0f, 0f, 0f);
      this.camera.lookAt(ref pos);
    }

    public void resize() {
      //camera.resize(800, 600);
      camera.resize();
    }

    public void update(double delta) {
      camera.update();
    }

    public void render() {
      GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.CullFace);
      GL.CullFace(CullFaceMode.Back);

      this.shader.begin();

      this.shader.projectUsingCamera(ref this.camera);
      this.shader.uniformMatrix4(Shader.MODEL_VIEW_UNIFORM, false, ref modelView);
      this.texture.bind(0);
      this.shader.uniformTexture(0);
      this.mesh.bind(ref shader);
      this.mesh.render(PrimitiveType.Triangles, BeginMode.Triangles);
      this.mesh.unbind(ref shader);
      this.shader.end();

      GL.Flush();
    }

    public void onExit() {
      
    }

    public void dispose() {
      
    }
  }
}
