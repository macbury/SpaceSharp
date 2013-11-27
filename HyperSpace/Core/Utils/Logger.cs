using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Utils {
  class Logger {
    private int frames;
    private Timer timer;

    public Logger() {
      this.frames = 0;
      this.timer = new Timer(1.0);
      timer.onTimerExecute = this.logFPS;
      timer.start();
    }

    private void logFPS(Timer timer) {
      if (frames < 25) {
        info("FPS", frames.ToString() + " LOW!!!!!!");
      } else {
        info("FPS", frames.ToString());
      }
      frames = 0;
    }

    public void info(String tag, String message) {
      info(tag, message, null);
    }

    public void info(String tag, String message, params object[] args) {
      if (args == null) {
        Debug.WriteLine("[" + tag + "] " + message);
      } else {
        Debug.WriteLine("[" + tag + "] " + message, args);
      }
      
    }

    public void fps(double delta) {
      frames++;
      timer.update(delta);
    }
  }
}
