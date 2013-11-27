using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Scenes {
  interface Scene {
    void onEnter();
    void resize();
    void update(double delta);
    void render();
    void onExit();
    void dispose();
  }
}
