using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes.Tests {
  class BlackScreenTest : Scene {
    private Rendering.Shader shader;

    public void onEnter() {
      this.shader = Game.assets.shader("test");
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
      
    }
  }
}
