using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Mesh {
    private BufferUsageHint hint;
    private List<Attribute> attributes;

    public Mesh(BufferUsageHint hint) {
      this.hint       = hint;
      this.attributes = new List<Attribute>();
    }
  }
}
