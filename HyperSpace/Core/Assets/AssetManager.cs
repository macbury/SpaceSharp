using HyperSpace.Core.Rendering;
using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Assets {
  class AssetManager : Disposable {
    private String directory;
    private Dictionary<String, Disposable> resources;
    public AssetManager(String directory) {
      this.directory = directory;
      this.resources = new Dictionary<String,Disposable>();
    }
    public String path(String pathToFile) {
      return directory + pathToFile;
    }
    public String readString(String pathToFile) {
      pathToFile = path(pathToFile);
      using (StreamReader sr = new StreamReader(pathToFile)) {
        return sr.ReadToEnd();
      }
    }

    public Shader shader(String name) {
      String key = "Shader"+name;
      if (!resources.ContainsKey(key)) {
        resources.Add(key, new Shader(readString("Shaders/" + name + ".vert"), readString("Shaders/" + name + ".frag")));
      }

      return (Shader)resources[key];
    }

    public Texture texture(String name) {
      String key = "Texture" + name;
      if (!resources.ContainsKey(key)) {
        Bitmap bitmap = new Bitmap(path("Textures/"+name));
        resources.Add(key, new Texture(ref bitmap));
      }

      return (Texture)resources[key];
    }

    public void dispose() {
      resources = null;
    }
  }
}
