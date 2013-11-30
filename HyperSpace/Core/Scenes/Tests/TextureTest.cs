using HyperSpace.Core.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes.Tests {
  class TextureTest : Scene {
    private PerspecitveCamera camera;
    private Vector3           cameraPosition;
    private Matrix4           modelView;
    private Shader            shader;
    private Mesh mesh;
    private Texture texture;
    float angle = 0.0f;
    public void onEnter() {
      this.shader         = Game.assets.shader("texture");

      this.texture        = Game.assets.texture("uvmap.png");

      this.camera         = new PerspecitveCamera();
      this.cameraPosition = new Vector3(0f, 0f, 5f);
      this.camera.translate(ref cameraPosition);

      Vector3 target = new Vector3(0f, 0f, 0f);
      this.camera.lookAt(ref target);
      this.mesh      = MeshBuilder.generateTextureQuad();
      this.modelView = Matrix4.CreateTranslation(new Vector3(0f, 0f, 0f));

      camera.update();
      Game.logger.info("Camera matrix", camera.ToString());
    }

    public void resize() {
      camera.resize();
    }

    public void update(double delta) {
      angle += 1f * (float)delta;
      //this.camera.rotateY(angle);
      camera.update();
      //Matrix4.CreateRotationY(angle, out modelView);
      //Matrix4.CreateRotationZ(angle, out modelView);
    }

    public void render() {
      GL.Enable(EnableCap.DepthTest);
      //GL.Enable(EnableCap.CullFace);
      //GL.CullFace(CullFaceMode.Back);

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
