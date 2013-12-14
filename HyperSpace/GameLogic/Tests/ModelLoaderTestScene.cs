using HyperSpace.Core.Assets.ModelLoader;
using HyperSpace.Core.Scenes;
using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.GameLogic.Tests {
  class ModelLoaderTestScene : Scene {
    public void onEnter() {
      OBJModelLoader modelLoader = new OBJModelLoader();
      modelLoader.loadModel("Assets/Models/spaceship.obj");
      

      return;
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
