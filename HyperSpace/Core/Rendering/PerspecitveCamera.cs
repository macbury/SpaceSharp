using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class PerspecitveCamera : Camera {
    public float fieldOfView = MathHelper.PiOver4;
    private Quaternion _rotation;
    public PerspecitveCamera() : this(Game.shared.width, Game.shared.height) { }
    public PerspecitveCamera(int width, int height) : this(MathHelper.PiOver4, width, height) {}
    public PerspecitveCamera(float fieldOfView, int width, int height)
      : base(width, height) {
        this.fieldOfView = fieldOfView;
        this._rotation = new Quaternion();
    }

    // Angle is in degrees!
    public void rotate(Vector3 axis, float angle) {
      _rotation = Quaternion.FromAxisAngle(axis, MathHelper.DegreesToRadians(angle));
      _temp     = _direction;
      Vector3.Transform(ref _temp, ref _rotation, out _direction);
      normalizeUp();
    }

    public void orbit(Vector3 point, float distance, float yaw, float roll) {
      yaw     = MathHelper.DegreesToRadians(yaw);
      roll    = MathHelper.DegreesToRadians(roll);
      _temp.X = (float)(Math.Sin(roll) * Math.Sin(yaw) * distance);
      _temp.Y = (float)(Math.Cos(yaw) * distance);
      _temp.Z = (float)(Math.Cos(roll) * Math.Sin(yaw) * distance);

      _position = point + _temp;
      lookAt(ref point);
    }

    public override void onResize() {
      float aspect = _viewportWidth / _viewportHeight;
      Matrix4.CreatePerspectiveFieldOfView(fieldOfView, aspect, near, far, out _projection);
    }
  }
}
