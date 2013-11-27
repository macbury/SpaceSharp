using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class VertexAttribute {
    public int usage;
    public int numComponents;
    public int offset;
    public String alias;
    public int unit;
    private int usageIndex;

    public VertexAttribute(int usage, int numComponents, String alias, int unit) {
      this.usage         = usage;
      this.numComponents = numComponents;
      this.alias         = alias;
      this.unit          = unit;
    }
    public VertexAttribute(int usage, int numComponents, String alias) : this(usage, numComponents, alias, 1) {}

    #region Quick Create
    public static VertexAttribute Position() {
      return new VertexAttribute(VertexAttributes.Usage.Position, 3, Shader.POSITION_ATTRIBUTE);
    }
    public static VertexAttribute TexCoords(int unit) {
      return new VertexAttribute(VertexAttributes.Usage.TextureCoordinates, 2, Shader.TEXCOORD_ATTRIBUTE + unit, unit);
    }
    public static VertexAttribute Normal() {
      return new VertexAttribute(VertexAttributes.Usage.Normal, 3, Shader.NORMAL_ATTRIBUTE);
    }
    public static VertexAttribute Color() {
      return new VertexAttribute(VertexAttributes.Usage.ColorPacked, 4, Shader.COLOR_ATTRIBUTE);
    }
    public static VertexAttribute ColorUnpacked() {
      return new VertexAttribute(VertexAttributes.Usage.Color, 4, Shader.COLOR_ATTRIBUTE);
    }
    public static VertexAttribute Tangent() {
      return new VertexAttribute(VertexAttributes.Usage.Tangent, 3, Shader.TANGENT_ATTRIBUTE);
    }
    public static VertexAttribute Binormal() {
      return new VertexAttribute(VertexAttributes.Usage.BiNormal, 3, Shader.BINORMAL_ATTRIBUTE);
    }
    #endregion

    public int getKey() {
      return (usageIndex << 8) + (unit & 0xFF);
    }
  }
}
