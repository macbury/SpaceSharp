using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class PerspecitveCamera : Camera {
    public float fieldOfView = MathHelper.PiOver4;

    public PerspecitveCamera(float fieldOfView, int width, int height)
      : base(width, height) {
      this.fieldOfView = fieldOfView;
      update();
    }
    
    public override void onUpdate() {
      Vector3.Add(ref _position, ref _direction, out _temp);
      _view = Matrix4.LookAt(_position, _temp, _up);

      Matrix4.Mult(ref _view, ref _projection, out combined);
    }

    public override void onResize() {
      float aspect = _viewportWidth / _viewportHeight;
      Matrix4.CreatePerspectiveFieldOfView(fieldOfView, aspect, near, far, out _projection);
    }
  }
}
