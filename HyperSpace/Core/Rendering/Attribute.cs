using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Rendering {
  class Attribute<T> {
    private String name;
    private T[] data;
    private int attrSize;
    private int sizeInBytes;

    public Attribute(String name, int elementCount, int sizeInBytes, int attrSize) {
      this.name                 = name;
      data                      = new T[elementCount];
      this.sizeInBytes          = sizeInBytes;
      this.attrSize             = attrSize;
    }
    public IntPtr size() {
      return (IntPtr)(data.Length * sizeInBytes);
    }
  }
}
