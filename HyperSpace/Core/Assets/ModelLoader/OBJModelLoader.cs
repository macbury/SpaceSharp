using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HyperSpace.Core.Utils.WavefrontObjLoader;

namespace HyperSpace.Core.Assets.ModelLoader {
  class OBJModelLoader : IModelLoader {

    public void loadModel(string modelPath) {
      WavefrontObjLoader rawModel = new WavefrontObjLoader("Assets/Models/spaceship.obj", delegate(string resource_name) {
        String ext = Path.GetExtension(resource_name);
        if (ext == ".obj")
          return System.IO.File.Open(resource_name, System.IO.FileMode.Open);
        if (ext == ".mtl")
          return System.IO.File.Open("Assets/Materials/" + resource_name, System.IO.FileMode.Open);
        return null;
      });

      foreach(var mat in rawModel.materials) {
        foreach (var face in mat.faces) {

        }
      }
    }
  }
}
