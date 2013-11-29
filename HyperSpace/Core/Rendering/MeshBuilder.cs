using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class MeshBuilder {

    public static float[] quadTextureVerts = new float[] {
      -1f, -1f,  0f,   0f,  1f,// bottom left
      1f, -1f,  0f,   1f,  1f,// bottom right
      1f,  1f,  0f,   1f,  0f,// top right
      -1f,  1f,  0f,   0f,  0f// top left
    };

    public static uint[] quadTextureIndencies = new uint[] {
      0, 1, 2, 
      0, 2, 3
    };

    public static Mesh generateTextureQuad() {
      VertexAttributes attrs = new VertexAttributes(VertexAttribute.Position(), VertexAttribute.TexCoords(0));
      Mesh mesh = new Mesh(true, 4, 5, attrs);
      mesh.setVerticies(ref quadTextureVerts);
      mesh.setIndicies(ref quadTextureIndencies);

      return mesh;
    }

  }
}
