using HyperSpace.Core.Utils;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class IndexBufferObject : Disposable {
    private int bufferHandle = -1;
    private uint[] buffer;
    private bool dirty;
    private BufferUsageHint hint;

    public int id {
      get { return bufferHandle; }
    }
    public IndexBufferObject(bool isStatic) {
      this.dirty = true;
      this.hint = isStatic ? BufferUsageHint.StaticDraw : BufferUsageHint.DynamicDraw;
    }
    public void setIndicies(ref uint[] buffer) {
      this.buffer = buffer;
      dirty = true;
    }
    public void bind() {
      if (bufferHandle == -1)
        GL.GenBuffers(1, out bufferHandle);

      GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferHandle);

      if (dirty) {
        bufferChange();
        dirty = false;
      }
    }
    public void unbind() {
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }
    public void bufferChange() {
      dirty = false;
      GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(size * sizeof(uint)), buffer, hint);
    }
    public void dispose() {
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.DeleteBuffer(bufferHandle);
    }
    public int size { get { return buffer == null ? 0 : buffer.Length; } }

    public bool empty() {
      return this.buffer == null || this.buffer.Length == 0;
    }
  }
}
