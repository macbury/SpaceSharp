using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.IO;

namespace HyperSpace.Core {
  class OpenTKWindow : GameWindow {
    public static String TAG = "OpenTKWindow";
    

    public OpenTKWindow(Game game)
      : base(game.width, game.height, new GraphicsMode(32, 24, 0, 4)) {
    }

    protected override void OnUnload(EventArgs e) {
      base.OnUnload(e);
      Game.shared.dispose();
    }

    protected override void OnLoad(EventArgs e) {
      base.OnLoad(e);
      Game.shared.initialize();
      WindowBorder = WindowBorder.Fixed;
    }

    protected override void OnUpdateFrame(FrameEventArgs e) {
      base.OnUpdateFrame(e);
      Game.shared.update(e.Time);
    }

    protected override void OnResize(EventArgs e) {
      base.OnResize(e);
      Game.shared.resize(Width, Height);
    }

    protected override void OnRenderFrame(FrameEventArgs e) {
      base.OnRenderFrame(e);
      Game.shared.render();
      SwapBuffers();
    }
  }
}
