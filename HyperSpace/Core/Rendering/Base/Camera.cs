using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  abstract class Camera {
    protected bool dirty = true;
    /** the position of the camera **/
    protected Vector3 _position;
    /** the unit length direction vector of the camera **/
    protected Vector3 _direction;
    /** the unit length up vector of the camera **/
    protected Vector3 _up;

    protected Vector3 _temp;

    protected Matrix4 _projection;
    protected Matrix4 _view;
    public    Matrix4 combined;
    public Vector3 direction {
      get { return _direction; }
      //set { _direction = value; }
    }
    protected Matrix4 _invProjectionView;

    /** the near clipping plane distance, has to be positive **/
    public float near = 0.1f;
    /** the far clipping plane distance, has to be positive **/
    public float far = 100f;

    /** the viewport width **/
    protected float _viewportWidth = 0;
    /** the viewport height **/
    protected float _viewportHeight = 0;

    public float viewportWidth {
      get { return _viewportWidth; }
      set {
        _viewportWidth = value;
        dirty = true;
      }
    }
    public float viewportHeight {
      get { return _viewportHeight; }
      set {
        _viewportHeight = value;
        dirty = true;
      }
    }
    public Camera(int width, int height) {
      this._viewportHeight = height;
      this._viewportWidth  = width;
      this._position       = Vector3.Zero;
      this._direction      = new Vector3(0, 0, 1);
      this._up             = new Vector3(0, 1, 0);
      this._temp           = Vector3.Zero;
      this._projection     = new Matrix4();
      this._view           = Matrix4.Identity;
    }
    public void update() {
      if (dirty) {
        normalizeUp();
        onResize();
        dirty = false;
      }

      calculateViewMatrix();
      Matrix4.Mult(ref _view, ref _projection, out combined);
      Matrix4.Invert(ref combined, out _invProjectionView);
    }
    public abstract void calculateViewMatrix();
    public abstract void onResize();
    public void lookAt(ref Vector3 target) {
      Vector3.Subtract(ref target, ref _position, out _direction);
      _direction.Normalize();
    }
    public void normalizeUp() {
      Vector3.Cross(ref _direction, ref _up, out _temp);
      _temp.Normalize();
      Vector3.Cross(ref _temp, ref _direction, out _up);
      _up.Normalize();
    }
    public void translate(ref Vector3 _target) {
      _position += _target;
      normalizeUp();
    }
    public void transform(ref Vector3 _target) {
      _position = _target;
      normalizeUp();
    }
    public void resize(int w, int h) {
      this.viewportWidth = w;
      this.viewportHeight = h;
    }
    public void resize() {
      resize(Game.shared.width, Game.shared.height);
    }
    public override string ToString() {
      return "Projection:\n" + _projection.ToString() + "\nView:\n" + _view.ToString() + "\nCombined:\n" + combined.ToString();
    }
    public void unproject(ref Vector3 vec, float viewportX, float viewportY, float viewportWidth, float viewportHeight) {
      float x = vec.X, y = vec.Y;
      x = x - viewportX;
      y = Game.shared.height - y - 1;
      y = y - viewportY;
      vec.X = (2 * x) / viewportWidth - 1;
      vec.Y = (2 * y) / viewportHeight - 1;
      vec.Z = 2 * vec.Z - 1;
      Vector3.TransformPerspective(ref vec, ref _invProjectionView, out vec);
    }
    public void unproject(ref Vector3 vec) {
      unproject(ref vec, 0, 0, (float)Game.shared.width, (float)Game.shared.height);
    }
  }
}
