using HyperSpace.Core.Rendering;
using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
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
        resources.Add(key, new Shader(readString("Shader/" + name + ".vert"), readString("Shader/" + name + ".frag")));
      }

      return (Shader)resources[key];
    }
    public void dispose() {
      foreach (KeyValuePair<String, Disposable> item in resources) {
        item.Value.dispose();
      }
    }
  }
}
