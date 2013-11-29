using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Camera2D : Camera  {
    public int zoom = 1;
    public Camera2D() : base(Game.shared.width, Game.shared.height) { }

    public override void onResize() {
      _projection = Matrix4.CreateOrthographicOffCenter(zoom * -viewportWidth / 2, zoom * (viewportWidth / 2), zoom * -(viewportHeight / 2), zoom * viewportHeight / 2, near, far);
    }
  }
}
