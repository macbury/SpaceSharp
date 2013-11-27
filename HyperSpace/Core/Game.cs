﻿using HyperSpace.Core.Assets;
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
    private static Game _game;
    private Logger _logger;
    private AssetManager _assets;
    public static AssetManager assets {
      get {
        return shared._assets;
      }
    }
    public static Logger logger {
      get {
        if (shared._logger == null)
          shared._logger = new Logger();
        return shared._logger;
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
      if (_game != null)
        throw new Exception("Already initialized!!!!");
      _game = this;

      _assets = new AssetManager("Res/");
      logger.info(TAG, "Creating");
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
      assets.dispose();
    }
    #endregion
  }
}