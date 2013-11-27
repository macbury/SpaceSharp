using HyperSpace.Core.Scenes;
using HyperSpace.Core.Scenes.Tests;
using HyperSpace.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core {
  class Game {
    public static string TAG = "Game";
    #region Shared logic
    private static Logger _logger;
    private static Game _game;
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
    #endregion
    #region Configuration
    public static double FPS = 60;
    public int height   = 768;
    public int width    = 1366;
    #endregion
    #region Scene managment
    private Scene _currentScene;
    public Scene CurrentScene {
      get { return _currentScene; }
      set {
        if (_currentScene != null) {
          logger.info(TAG, "Exiting scene: " + _currentScene.GetType().ToString());
          _currentScene.onExit();
        }
        _currentScene = value;
        _currentScene.onEnter();
        logger.info(TAG, "Entering scene: " + _currentScene.GetType().ToString());
      }
    }
    #endregion
    
    public Game() {
      logger.info(TAG, "Creating");

      if (_game != null)
        throw new Exception("Already initialized!!!!");
      _game = this;
    }

    #region Game Life Cycle
    public void initialize() {
      logger.info(TAG, "Initialize");
      CurrentScene = new BlackScreenTest();
    }
    public void resize(int Width, int Height) {
      width = Width;
      height = Height;
      CurrentScene.resize();
    }
    public void update(double time) {
      logger.fps(time);
      CurrentScene.update(time);
    }
    public void render() {
      CurrentScene.render();
    }
    public void dispose() {
      logger.info(TAG, "Dispose");
      CurrentScene.dispose();
    }
    #endregion
  }
}
