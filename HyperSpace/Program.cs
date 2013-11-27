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
      Game game       = new Game();
      OpenTKWindow gw = new OpenTKWindow(game);
      gw.Run(Game.FPS);
    }
  }
}
