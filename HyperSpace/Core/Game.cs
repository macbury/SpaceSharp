using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core {
  class Game {
    private static Logger _logger;
    private static Game _game;
    public static string TAG = "Game";
    public static double FPS = 60;
    public static Logger logger {
      get {
        if (_logger == null)
          _logger = new Logger();
        return _logger;
      }
    }
    public static Game shared {
      get {
        return _game;
      }
    }
    public int height   = 768;
    public int width    = 1366;

    public Game() {
      logger.info(TAG, "Creating");

      if (_game != null)
        throw new Exception("Already initialized!!!!");
      _game = this;
    }

    #region Game Life Cycle
    public void initialize() {
      logger.info(TAG, "Initialize");
    }
    public void resize(int Width, int Height) {
      width = Width;
      height = Height;
    }
    public void update(double time) {
      logger.fps(time);
    }
    public void render() {

    }
    public void dispose() {
      logger.info(TAG, "Dispose");
    }
    #endregion
  }
}
