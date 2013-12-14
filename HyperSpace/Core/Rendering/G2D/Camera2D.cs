using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Camera2D : Camera  {
    public int zoom = 1;

    public Camera2D() : base(Game.shared.width, Game.shared.height) {
      this.near = 0f;
    }

    public void setToOrtho(bool yDown, float viewportWidth, float viewportHeight) {
      if (yDown) {
        _up.X  = 0;
        _up.Y  = -1;
        _up.Z  = 0;

        _direction.X = 0;
        _direction.Y = 0;
        _direction.Z = 1;
      } else {
        _up.X = 0;
        _up.Y = 1;
        _up.Z = 0;

        _direction.X = 0;
        _direction.Y = 0;
        _direction.Z = -1;
      }

      _position.X = zoom * viewportWidth / 2.0f;
      _position.Y = zoom * viewportHeight / 2.0f;
      _position.Z = 0;
      _viewportWidth = viewportWidth;
      _viewportHeight = viewportHeight;
      update();
    }

    public void setToOrtho(bool yDown) {
      setToOrtho(yDown, Game.shared.width, Game.shared.height);
    }

    public override void onResize() {
      //_projection = Matrix4.CreateOrthographicOffCenter(zoom * -viewportWidth / 2, zoom * (viewportWidth / 2), zoom * -(viewportHeight / 2), zoom * viewportHeight / 2, near, far);
      _projection =  Matrix4.CreateOrthographic(viewportWidth, viewportHeight, near, far);
    }

    public override void calculateViewMatrix() {
      Matrix4.CreateTranslation(ref _position, out _view);
    }
  }
}
