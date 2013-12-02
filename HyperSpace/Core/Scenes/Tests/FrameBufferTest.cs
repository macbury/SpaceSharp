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
      this.fbo = new FrameBuffer();
    }

    public void resize() {
      throw new NotImplementedException();
    }

    public void update(double delta) {
      throw new NotImplementedException();
    }

    public void render() {
      throw new NotImplementedException();
    }

    public void onExit() {
      throw new NotImplementedException();
    }

    public void dispose() {
      this.fbo.dispose();
    }
  }
}
