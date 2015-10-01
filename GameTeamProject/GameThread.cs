using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameTeamProject
{
    class GameThread
    {    
        const int MAX_FPS = 60; // Desired fps
        const int MAX_FRAME_SKIPS = 5; // maximum number of frames to be skipped
        const int FRAME_PERIOD = 1000 / MAX_FPS; // the frame period

        Thread gameThread;

        static readonly object locker = new object();
        private volatile bool running;

        public GameThread()
        {
            this.gameThread = new Thread(GameLoop);
            running = false;
        }

        public void Start()
        {
            gameThread.Start();
        }

        private void GameLoop()
        {
            long beginTime;		// the time when the cycle begun
            long timeDiff;		// the time it took for the cycle to execute
            int sleepTime;		// ms to sleep (<0 if we're behind)
            int framesSkipped;	// number of frames being skipped 

            this.running = true;

            while (running)
            {
                lock (locker)
                {
                    beginTime = Environment.TickCount;
                    framesSkipped = 0;	// resetting the frames skipped
                    // update game state
                    GameWindow.game.Update();
                    // render state to the screen
                    GameWindow.game.Render();
                    // calculate how long did the cycle take
                    timeDiff = Environment.TickCount - beginTime;
                    // calculate sleep time
                    sleepTime = (int)(FRAME_PERIOD - timeDiff);

                    if (sleepTime > 0)
                    {
                        // if sleepTime > 0 we're OK
                        try
                        {
                            // send the thread to sleep for a short period
                            // very useful for battery saving
                            Thread.Sleep(sleepTime);
                        }
                        catch (ThreadInterruptedException e) { }
                    }

                    while (sleepTime < 0 && framesSkipped < MAX_FRAME_SKIPS)
                    {
                        // we need to catch up
                        // update without rendering
                        GameWindow.game.Update();
                        // add frame period to check if in next frame
                        sleepTime += FRAME_PERIOD;
                        framesSkipped++;
                    }
                }
            }
        }
        public void Stop()
        {
            this.running = false;
        }
    }
}
