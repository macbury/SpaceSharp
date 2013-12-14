using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Assets.ModelLoader {
  interface IModelLoader {
    void loadModel(String modelPath);
  }
}
