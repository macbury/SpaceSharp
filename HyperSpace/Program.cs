using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using HyperSpace.Core;


namespace HyperSpace {
  class Program {
    static void Main(string[] args) {
      OpenTKWindow gw = new OpenTKWindow(GraphicsContextFlags.Default);
      gw.Run(60);
    }
  }
}
