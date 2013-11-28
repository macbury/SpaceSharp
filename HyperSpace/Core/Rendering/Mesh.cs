
using HyperSpace.Core.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Mesh : Disposable {
    private VertexBufferObject verticies;
    private IndexBufferObject  indicies;

    public Mesh(bool isStatic, int attributesPerVertex, int vertexCount, VertexAttributes attrs) {
      verticies = new VertexBufferObject(isStatic, vertexCount, attributesPerVertex, attrs);
      indicies  = new IndexBufferObject(isStatic);
      Game.meshes.Add(this);
    }

    public void dispose() {
      this.verticies.dispose();
      if (this.indicies != null) {
        this.indicies.dispose();
      }
      if (Game.meshes.Contains(this)) {
        Game.meshes.Remove(this);
      }
    }

    #region Rendering
    public void bind(ref Shader shader) {
      this.verticies.bind(ref shader);
      if (!this.indicies.empty())
        this.indicies.bind();
    }
    public void render(PrimitiveType primitive, int offset, int count) {
      if (indicies.empty()) {
        GL.DrawArrays(primitive, offset, count);
      } else {
        GL.DrawElements(primitive, count, DrawElementsType.UnsignedShort, offset);
      }
    }
    public void render(PrimitiveType primitive) {
      render(primitive, 0, indicies.empty() ? verticies.numVertices : indicies.size);
    }
    public void unbind(ref Shader shader) {
      this.verticies.unbind(ref shader);
      if (!this.indicies.empty())
        this.indicies.unbind();
    }
    #endregion
  }
}
