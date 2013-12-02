using OpenTK;
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

    public static Mesh screenQuad(ref Camera camera, int textureUnit) {
      VertexAttributes attrs = new VertexAttributes(VertexAttribute.Position(), VertexAttribute.TexCoords(textureUnit));
      Mesh mesh = new Mesh(true, 4, 5, attrs);

      Vector3 vec0 = Vector3.Zero;
      camera.unproject(ref vec0);

      Vector3 vec1 = new Vector3(Game.shared.width, Game.shared.height, 0);
      camera.unproject(ref vec1);

      mesh.setVertices(new float[]{
                        vec0.X, vec0.Y, 0, 0, 1,
                        vec1.X, vec0.Y, 0, 1, 1,
                        vec1.X, vec1.Y, 0, 1, 0,
                        vec0.X, vec1.Y, 0, 0, 0
                      });
      mesh.setIndices(new uint[] { 0, 1, 2, 2, 3, 0 });

      return mesh;
    }

  }
}
