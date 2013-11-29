using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyperSpace.Core.Rendering {
  class GLResourcesManager : List<Disposable>, Disposable {
    public static String TAG = "GLResourcesManager";
    public void dispose() {
      Game.logger.info(TAG, "Elements to dispose: {0}", Count);

      while (Count > 0) {
        Game.logger.info(TAG, "Found element to dispose");
        Disposable resource = this.ElementAt(0);
        resource.dispose();
      }

      Game.logger.info(TAG, "Elements left: {0}", Count);
    }
  }
}
