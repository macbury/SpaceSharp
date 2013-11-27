using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class VertexAttributes {
    public static class Usage {
      public static int Position = 1;
      public static int Color = 2;
      public static int ColorPacked = 4;
      public static int Normal = 8;
      public static int TextureCoordinates = 16;
      public static int Generic = 32;
      public static int BoneWeight = 64;
      public static int Tangent = 128;
      public static int BiNormal = 256;
    }
    private VertexAttribute[] attributes;
    public int vertexSize { get; set; }

    public VertexAttributes(params VertexAttribute[] attrs) {
      this.attributes = attrs;
      vertexSize = calculateOffsets();
    }

    private int calculateOffsets() {
      int count = 0;
      for (int i = 0; i < this.attributes.Length; i++) {
        VertexAttribute attr = this.attributes[i];
        attr.offset          = count;
        count                += 4 * attr.numComponents;
      }
      return count;
    }

    public VertexAttribute get(int index) {
      return attributes[index];
    }

    public int size() {
      return attributes.Length;
    }
  }
}
