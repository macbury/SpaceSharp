using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.Drawing;

namespace HyperSpace.Core.Rendering {
  class Texture : Disposable {
    public  TextureTarget textureTarget = TextureTarget.Texture2D;
    private TextureMinFilter minFilter;
    private TextureMagFilter magFilter;

    private int textureHandle = 0;
    private int _width;
    private int _height;

    public int width {
      get { return _width; }
    }
    public int height {
      get { return _height; }
    }

    public Texture(int width, int height) {
      _width  = width;
      _height = height;

      init();
      Bitmap bitmap = new Bitmap(_width, _height);
      load(ref bitmap);
    }

    public Texture(ref Bitmap bmp) {
      init();
      load(ref bmp);
    }

    private void init() {
      createHandle();

      setFilters(TextureMinFilter.Linear, TextureMagFilter.Linear);
      setWrap(TextureWrapMode.Clamp, TextureWrapMode.Clamp);
      Game.glResources.Add(this);
    }

    private void createHandle() {
      if (textureHandle == 0) {
        textureHandle = GL.GenTexture();
      } else {
        throw new Exception("already have handle!!!!");
      }
    }

    public int getHandle() {
      return textureHandle;
    }

    public void load(ref Bitmap bmp) {
      GL.BindTexture(TextureTarget.Texture2D, textureHandle);

      BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpData.Width, bmpData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);
      _width = bmp.Width;
      _height = bmp.Height;
      bmp.UnlockBits(bmpData);
    }

    public void setFilters(TextureMinFilter min, TextureMagFilter mag) {
      bind();
      GL.TexParameter(textureTarget, TextureParameterName.TextureMinFilter, (int)min);
      GL.TexParameter(textureTarget, TextureParameterName.TextureMagFilter, (int)mag);
    }

    public void setWrap(TextureWrapMode u, TextureWrapMode v) {
      bind();
      GL.TexParameter(textureTarget, TextureParameterName.TextureWrapS, (int)u);
      GL.TexParameter(textureTarget, TextureParameterName.TextureWrapT, (int)v);
    }

    public void bind() {
      GL.BindTexture(textureTarget, textureHandle);
    }

    public void bind(int unit) {
      bind();
      GL.ActiveTexture(TextureUnit.Texture0 + unit);
    }

    private void delete() {
      GL.DeleteTexture(textureHandle);
      textureHandle = 0;
    }
    public void dispose() {
      delete();
      Game.glResources.Remove(this);
    }
  }
}
