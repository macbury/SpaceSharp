using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering.Base {
  class FrameBuffer : Disposable {
    private int frameBufferHandle;
    private int depthBufferHandle;
    private int _width, _height;
    private Texture colorTexture;
    private bool hasDepth;
    private bool isDirty;

    public FrameBuffer(int w, int h, bool hasDepth) {
      this.hasDepth = hasDepth;
      this._width   = w;
      this._height  = h;
      build();
      Game.glResources.Add(this);
    }

    private void build() {
      colorTexture = new Texture();
    }

    public void dispose() {
      Game.glResources.Remove(this);
    }
  }
}
