using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTeamProject
{
    class Player : Tank
    {
        private volatile bool moving; // Check if player is moving

        public Player(int x, int y)
            : base(x, y, Tanks.GetType(TankType.Yellow), 0) // Calls the constructor of Tank class
        {
            moving = false;
        }

        public void Update(long gameTime)
        {
            // Move player (Pixels per move * direction)
            x += velocity * dx;
            y += velocity * dy;

            if (moving == true)
            {
                // Play moving animation
                if (gameTime - frameTicker > framePeriod)
                {
                    frameTicker = gameTime;

                    if (currentFrame % 2 == 0)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame--;
                    }
                }
            }

            // Update missiles
            try
            {
                foreach (var m in missiles)
                {
                    m.Update(Environment.TickCount);

                    if (m.ShouldBeDestroyed())
                    {
                        m.Destroy();
                    }

                    if (m.IsDestroyed())
                    {
                        missiles.Remove(m);
                    }
                }
            }
            catch (InvalidOperationException e)
            { 
            
            }
        }

        public void Render(Graphics g)
        {
            // Player
            g.DrawImage(bitmap, new Rectangle(x, y, spriteWidth, spriteHeight),
                sourceRect, GraphicsUnit.Pixel);

            // Missiles
            foreach (var m in missiles)
            {
                m.Render(g);
            }
        }

        public void HandleKeyDown(KeyEventArgs key)
        {
            var code = key.KeyCode;

            switch (code)
            {
                case Keys.W:
                    dy = -1;
                    dx = 0;
                    ChangeFrame((int)TankFrames.Up);
                    break;
                case Keys.A:
                    dx = -1;
                    dy = 0;
                    ChangeFrame((int)TankFrames.Left);
                    break;
                case Keys.S:
                    dy = 1;
                    dx = 0;
                    ChangeFrame((int)TankFrames.Down);
                    break;
                case Keys.D:
                    dx = 1;
                    dy = 0;
                    ChangeFrame((int)TankFrames.Right);
                    break;
                case Keys.Space:
                    Shoot();
                    break;
            }

            this.moving = true;
        }

        public void HandleKeyUp(KeyEventArgs key)
        {
            var code = key.KeyCode;

            switch (code)
            {
                case Keys.W:
                    dy = 0;
                    break;
                case Keys.A:
                    dx = 0;
                    break;
                case Keys.S:
                    dy = 0;
                    break;
                case Keys.D:
                    dx = 0;
                    break;
            }

            if (dx == 0 && dy == 0)
            {
                moving = false;
            }
        }

        private void ChangeFrame(int frame)
        {
            if (currentFrame != firstFrame + frame || currentFrame != firstFrame + frame + 1)
            {
                currentFrame = firstFrame + frame;
            }

            // Set the frame
            sourceRect.X = currentFrame * spriteWidth;
        }

        public void Shoot()
        {
            if (this.missiles.Count == 0)
            {
                missiles.Add(new Missile(x, y, currentFrame, spriteWidth, spriteHeight));
            }
        }
    }
}
