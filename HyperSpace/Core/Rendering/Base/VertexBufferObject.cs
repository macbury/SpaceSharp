using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class VertexBufferObject {
    private int bufferHandle;
    private float[] vertices;
    private VertexAttributes attributes;
    private bool dirty;
    private BufferUsageHint hint;
    public int numVertices;
    public int id {
      get { return bufferHandle; }
    }
    public VertexBufferObject(bool isStatic, int numVertices, int vertexSize, VertexAttributes attrs) {
      this.hint       = isStatic ? BufferUsageHint.StaticDraw : BufferUsageHint.DynamicDraw;
      this.attributes = attrs;
      
      vertices        = new float[numVertices * vertexSize];
      dirty           = true;
      this.numVertices = numVertices;
      GL.GenBuffers(1, out bufferHandle);
    }
    public void setVerticies(ref float[] vert) {
      vertices = vert;
      dirty = true;
    }
    public void bind(ref Shader shader) {
      GL.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
      
      if (dirty) {
        bufferChange();
        dirty = false;
      }
      
      for (int i = 0; i < attributes.size(); i++) {
        VertexAttribute attr = attributes.get(i);
        int location         = shader.attribute(attr.alias);
        GL.EnableVertexAttribArray(location);
        GL.VertexAttribPointer(location, attr.numComponents, VertexAttribPointerType.Float, false, attributes.vertexSize, attr.offset);
      }
    }
    public void unbind(ref Shader shader) {
      for (int i = 0; i < attributes.size(); i++) {
        VertexAttribute attr = attributes.get(i);
        int location = shader.attribute(attr.alias);

        GL.DisableVertexAttribArray(location);
      }

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
    public void bufferChange() {
      dirty = false;
      GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeof(float)), vertices, hint);
    }
    public void dispose() {
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.DeleteBuffer(bufferHandle);
    }
  }
}
