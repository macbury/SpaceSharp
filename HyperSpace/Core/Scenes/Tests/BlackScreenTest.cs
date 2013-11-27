using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace HyperSpace.Core.Scenes.Tests {
  class BlackScreenTest : Scene {
    private Rendering.Shader shader;
    private Matrix4 projectionMatrix;
    private Matrix4 modelView;
    private string UNIFORM_MODEL_VIEW = "u_model_view";
    private string UNIFORM_PROJECTION = "u_projection";

    public void onEnter() {
      this.shader = Game.assets.shader("test");
    }

    public void resize() {
      
    }

    public void update(double delta) {
      
    }

    public void render() {
      shader.use();
      shader.uniformMatrix4(UNIFORM_MODEL_VIEW, false, ref modelView);
      shader.uniformMatrix4(UNIFORM_PROJECTION, false, ref projectionMatrix);
    }

    public void onExit() {
      
    }

    public void dispose() {
      
    }
  }
}
