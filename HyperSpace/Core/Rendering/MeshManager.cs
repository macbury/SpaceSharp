using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyperSpace.Core.Rendering {
  class MeshManager : List<Mesh>, Disposable {
    public static String TAG = "MeshManager";
    public void dispose() {
      Game.logger.info(TAG, "Elements to dispose: {0}", Count);
      for (int i = 0; i < Count; i++) {
        Mesh mesh = this.ElementAt(i);
        mesh.dispose();
      }

      Game.logger.info(TAG, "Elements left: {0}", Count);
    }
  }
}
