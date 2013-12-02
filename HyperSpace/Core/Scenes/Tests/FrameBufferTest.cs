using HyperSpace.Core.Rendering;
using HyperSpace.Core.Rendering.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes.Tests {
  class FrameBufferTest : Scene{
    private FrameBuffer fbo;

    public void onEnter() {
      this.fbo = new FrameBuffer(800, 600, true);
      Camera camera = new Camera2D();
      camera.update();
      Mesh screenQuad = MeshBuilder.screenQuad(ref camera, 0);
    }

    public void resize() {
      
    }

    public void update(double delta) {
      
    }

    public void render() {
      
    }

    public void onExit() {
      
    }

    public void dispose() {
      this.fbo.dispose();
    }
  }
}
