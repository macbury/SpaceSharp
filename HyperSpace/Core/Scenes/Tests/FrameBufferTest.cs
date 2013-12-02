using HyperSpace.Core.Rendering;
using HyperSpace.Core.Rendering.Base;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes.Tests {
  class FrameBufferTest : Scene{
    private FrameBuffer fbo;
    private Mesh screenQuad;
    private Camera2D camera2D;
    private Shader fboShader;
    private Shader textureShader;
    private PerspecitveCamera camera;
    private Vector3 cameraPosition;
    private Vector3 quadPosition = new Vector3(0f, 0, 0f);
    private Matrix4 modelView;
    private Mesh mesh;
    private Texture texture;
    private float angle;

    public void onEnter() {
      this.cameraPosition = new Vector3(0f, 0f, 10f);
      //Game.assets.shader("blur");
      this.textureShader = Game.assets.shader("texture");
      this.fboShader = Game.assets.shader("invert");

      this.fbo           = new FrameBuffer(600, 480, true);
      this.camera2D      = new Camera2D();
      //this.camera2D.translate(ref cameraPosition);
      camera2D.update();

      Camera tmpCam   = this.camera2D;
      this.screenQuad = MeshBuilder.screenQuad(ref tmpCam, 0);

      this.camera = new PerspecitveCamera();
      this.camera.translate(ref cameraPosition);

      Vector3 target = new Vector3(0f, 0f, 0f);
      this.camera.lookAt(ref target);
      this.mesh      = MeshBuilder.generateTextureQuad();
      this.modelView = Matrix4.CreateTranslation(quadPosition);

      this.texture = Game.assets.texture("uvmap.png");

      camera.update();
    }

    public void resize() {
      
    }

    public void update(double delta) {
      angle += 80f * (float)delta;
      camera.orbit(Vector3.Zero, 10, 90, angle);
      camera.update();

      camera2D.update();
    }

    public void render() {
      fbo.begin();
      {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        textureShader.begin();
        {
          textureShader.projectUsingCamera(ref this.camera);
          textureShader.uniformMatrix4(Shader.MODEL_VIEW_UNIFORM, false, ref modelView);

          this.texture.bind(0);
          textureShader.uniformTexture(0);

          this.mesh.bind(ref textureShader);
          {
            this.mesh.render(PrimitiveType.Triangles, BeginMode.Triangles);
          }
          this.mesh.unbind(ref textureShader);
        }
        textureShader.end();
      }
      fbo.end();

      fboShader.begin();
      {
        fbo.texture.bind(0);
        fboShader.uniformTexture(0);
        fboShader.projectUsingCamera(ref camera2D);
        fboShader.uniformMatrix4(Shader.MODEL_VIEW_UNIFORM, false, ref modelView);
        screenQuad.bind(ref fboShader);
        {
          screenQuad.render(PrimitiveType.Triangles, BeginMode.Triangles);
        }
        screenQuad.unbind(ref fboShader);
      }
      fboShader.end();
    }

    public void onExit() {
      
    }

    public void dispose() {
      //this.textureShader.dispose();
      //this.fboShader.dispose();
      //this.screenQuad.dispose();
      //this.fbo.dispose();
    }
  }
}
