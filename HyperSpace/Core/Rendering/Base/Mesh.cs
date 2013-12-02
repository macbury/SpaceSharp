
using HyperSpace.Core.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Mesh : Disposable {
    private VertexBufferObject verticies;
    private IndexBufferObject  indicies;

    public Mesh(bool isStatic, int numVertices, int vertexSize, VertexAttributes attrs) {
      verticies = new VertexBufferObject(isStatic, numVertices, vertexSize, attrs);
      indicies  = new IndexBufferObject(isStatic);
      Game.glResources.Add(this);
    }

    public void setIndicies(ref uint[] buffer) {
      this.indicies.setIndicies(ref buffer);
    }
    public void setIndices(uint[] buffer) {
      indicies.setIndicies(ref buffer);
    }

    public void setVerticies(ref float[] vert) {
      verticies.setVerticies(ref vert);
    }
    public void setVertices(float[] vert) {
      verticies.setVerticies(ref vert);
    }

    public void dispose() {
      this.verticies.dispose();
      if (this.indicies != null) {
        this.indicies.dispose();
      }
      Game.glResources.Remove(this);
    }

    #region Rendering
    public void bind(ref Shader shader) {
      this.verticies.bind(ref shader);
      if (!this.indicies.empty())
        this.indicies.bind();
    }
    public void render(PrimitiveType primitiveArray, BeginMode primitiveElement, int count, int  offset) {
      if (indicies.empty()) {
        GL.DrawArrays(primitiveArray, offset, count);
      } else {
        //GL.DrawElements(BeginMode.Triangles, this.cubeIndexObject.size, DrawElementsType.UnsignedInt, 0);
        GL.DrawElements(primitiveElement, count, DrawElementsType.UnsignedInt, offset);
      }
    }
    public void render(PrimitiveType primitiveArray, BeginMode primitiveElement) {
      render(primitiveArray, primitiveElement, indicies.empty() ? verticies.numVertices : indicies.size, 0);
    }
    public void unbind(ref Shader shader) {
      this.verticies.unbind(ref shader);
      if (!this.indicies.empty())
        this.indicies.unbind();
    }
    #endregion

  }
}
