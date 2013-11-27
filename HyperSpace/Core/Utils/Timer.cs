using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperSpace.Core.Utils {
  class Timer {
    private double accumulatedDelta;
    private bool running = false;
    private double executeAfter;

    public delegate void OnTimerExecute(Timer timer);
    public OnTimerExecute onTimerExecute;

    public Timer(double time) {
      this.executeAfter = time;
    }

    public void update(double delta) {
      if (running) {
        accumulatedDelta += delta;
        if (accumulatedDelta >= executeAfter) {
          restart();
          if (onTimerExecute != null) {
            onTimerExecute(this);
          }
        }
      }
    }

    public void start() {
      restart();
    }

    public void restart() {
      running = true;
      accumulatedDelta = 0.0;
    }

    public void stop() {
      running = false;
    }
  }
}
