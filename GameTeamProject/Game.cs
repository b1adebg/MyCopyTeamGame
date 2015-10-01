using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTeamProject
{
    public class Game
    {
        private Bitmap frame; // Used to store the current frame
        private Graphics frameRender; // Used to build the current frame
        private Graphics render; // Used to render the current frame
        private GameThread gameThread; // GameLoop thread
        private Player player;
        public Game(Graphics g)
        {
            frame = new Bitmap(800, 600);
            frameRender = Graphics.FromImage(frame);
            render = g;
            gameThread = new GameThread();
            player = new Player(0, 0);
        }

        public void Start()
        {
            gameThread.Start();
        }

        public void Render()
        {
            // Build frame
            // Black screen
            frameRender.FillRectangle(new SolidBrush(Color.Black), 0, 0, 800, 600);

            // Player
            player.Render(frameRender);

            // Render frame
            render.DrawImage(frame, 0, 0);
        }

        public void Update()
        {
            player.Update(Environment.TickCount);
        }

        public void Stop()
        {
            gameThread.Stop();
        }

        public void HandleKeyDown(KeyEventArgs key)
        {
            var code = key.KeyCode;

            if (code == Keys.W || code == Keys.A || code == Keys.S || code == Keys.D || code == Keys.Space)
            {
                player.HandleKeyDown(key);
            }
        }

        public void HandleKeyUp(KeyEventArgs key)
        {
            var code = key.KeyCode;

            if (code == Keys.W || code == Keys.A || code == Keys.S || code == Keys.D)
            {
                player.HandleKeyUp(key);
            }
        }
    }
}
