using HyperSpace.Core.Utils;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class VBO : Disposable {
    private int vbo_id;

    public int id {
      get { return vbo_id; }
    }

    public VBO() {
      GL.GenBuffers(1, out vbo_id);
    }

    public void bind() {
      GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_id);
    }

    public void dispose() {
      GL.DeleteBuffer(vbo_id);
    }
  }
}
