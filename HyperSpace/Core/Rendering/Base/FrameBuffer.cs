using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace HyperSpace.Core.Rendering.Base {
  class FrameBuffer : Disposable {
    private static int defaultFramebufferHandle;

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
      defaultFramebufferHandle = 0;

      colorTexture = new Texture(_width, _height);
      colorTexture.setFilters(TextureMinFilter.Linear, TextureMagFilter.Linear);
      colorTexture.setWrap(TextureWrapMode.ClampToEdge, TextureWrapMode.ClampToEdge);

      frameBufferHandle = GL.GenFramebuffer();
      if (hasDepth)
        depthBufferHandle = GL.GenRenderbuffer();
      

      GL.BindTexture(TextureTarget.Texture2D, colorTexture.getHandle());

      if (hasDepth) {
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, depthBufferHandle);
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent16, colorTexture.width, colorTexture.height);
      }

      GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferHandle);
      GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, colorTexture.getHandle(), 0);

      if (hasDepth)
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, depthBufferHandle);

      FramebufferErrorCode result = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

      GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
      GL.BindTexture(TextureTarget.Texture2D, 0);
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, defaultFramebufferHandle);

      if (result != FramebufferErrorCode.FramebufferComplete) {
        colorTexture.dispose();
        if (hasDepth) {
          GL.DeleteRenderbuffer(depthBufferHandle);
        }

        colorTexture.dispose();
        GL.DeleteRenderbuffer(frameBufferHandle);

        if (result == FramebufferErrorCode.FramebufferIncompleteAttachment)
          throw new Exception("frame buffer couldn't be constructed: incomplete attachment");
        if (result == FramebufferErrorCode.FramebufferIncompleteDimensionsExt)
          throw new Exception("frame buffer couldn't be constructed: incomplete dimensions");
        if (result == FramebufferErrorCode.FramebufferIncompleteMissingAttachment)
          throw new Exception("frame buffer couldn't be constructed: missing attachment");
        if (result == FramebufferErrorCode.FramebufferUnsupported)
          throw new Exception("frame buffer couldn't be constructed: unsupported combination of formats");
        throw new Exception("frame buffer couldn't be constructed: unknown error " + result);
      }
    }

    public void begin() {
      GL.Viewport(0,0, _width, _height);
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, frameBufferHandle);
    }

    public void end() {
      GL.Viewport(0, 0, Game.shared.width, Game.shared.height);
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, defaultFramebufferHandle);
    }

    public void dispose() {
      if (hasDepth) {
        GL.DeleteRenderbuffer(depthBufferHandle);
      }
      GL.DeleteRenderbuffer(frameBufferHandle);
      colorTexture.dispose();
      Game.glResources.Remove(this);
    }
  }
}
