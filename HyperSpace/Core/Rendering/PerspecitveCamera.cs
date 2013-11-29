using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class PerspecitveCamera : Camera {
    public float fieldOfView = MathHelper.PiOver4;

    public PerspecitveCamera() : this(Game.shared.width, Game.shared.height) { }
    public PerspecitveCamera(int width, int height) : this(MathHelper.PiOver4, width, height) {}
    public PerspecitveCamera(float fieldOfView, int width, int height)
      : base(width, height) {
        this.fieldOfView = fieldOfView;
    }

    public override void onResize() {
      float aspect = _viewportWidth / _viewportHeight;
      Matrix4.CreatePerspectiveFieldOfView(fieldOfView, aspect, near, far, out _projection);
    }
  }
}
