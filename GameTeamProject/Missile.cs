using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeamProject
{
    class Missile
    {
        private int x; // Current x position
        private int y; // Current y position
        private int dx; // X direction (-1 = left, 1 = right)
        private int dy; // Y direction (-1 = up, 1 = down)
        private int velocity; // Missile speed (Pixels per move)

        private bool destroyed;

        // Sprites
        private Bitmap bitmap;	// the animation sequence
        private Rectangle sourceRect;	// the rectangle to be drawn from the animation bitmap

        private int spriteWidth;    // the width of the sprite to calculate the cut out rectangle
        private int spriteHeight;	// the height of the sprite
        protected long frameTicker;	// the time of the last frame update
        protected int framePeriod;	// milliseconds between each frame (1000/fps)

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public Missile(int x, int y, int tankFrame, int tankWidth, int tankHeight)
        {
            this.x = x;
            this.y = y;
            SetDirection(tankFrame, tankWidth, tankHeight);
            velocity = 6;
            destroyed = false;

            // Sprites
            bitmap = GameTeamProject.Properties.Resources.sprites;

            spriteWidth = (bitmap.Width / 25) / 2;
            spriteHeight = (bitmap.Height / 16) / 2;

            int currentSprite = GetCurrentSprite();

            sourceRect = new Rectangle((spriteWidth * 2) * 20 + currentSprite, // X position of bullet on the sprite map
                (spriteHeight * 2) * 6 + (spriteHeight / 2), // Y position of bullet on the sprite map
                spriteWidth, spriteHeight);

            framePeriod = 20;
            frameTicker = 0;
        }

        public void Update(long gameTime)
        {
            if (destroyed)
            {
                // Play destroying animation
                if (gameTime - frameTicker > framePeriod)
                {
                    frameTicker = gameTime;

                    this.sourceRect.X += spriteWidth;
                }

                return;
            }

            // Move missile
            x += velocity * dx;
            y += velocity * dy;
        }

        public void Render(Graphics g)
        {
            g.DrawImage(bitmap, new Rectangle(x, y, spriteWidth, spriteHeight),
                sourceRect, GraphicsUnit.Pixel);
        }

        private int GetCurrentSprite()
        {
            int currentSprite = 0;

            if (this.dx == -1)
            {
                currentSprite += this.spriteWidth;
            }
            else if (this.dx == 1)
            {
                currentSprite += this.spriteWidth * 3;
            }
            else if (this.dy == 1)
            {
                currentSprite += this.spriteWidth * 2;
            }

            return currentSprite;
        }

        private void SetDirection(int tankFrame, int tankWidth, int tankHeight)
        {
            switch (tankFrame)
            {
                case 0:
                case 1:
                    this.dx = 0;
                    this.dy = -1;
                    x += tankWidth / 5;
                    y -= tankHeight / 2;
                    break;
                case 2:
                case 3:
                    this.dx = -1;
                    this.dy = 0;
                    x -= tankWidth / 2;
                    y += tankHeight / 5;
                    break;
                case 4:
                case 5:
                    this.dx = 0;
                    this.dy = 1;
                    x += tankWidth / 5;
                    y += tankHeight;
                    break;
                case 6:
                case 7:
                    this.dx = 1;
                    this.dy = 0;
                    x += tankWidth;
                    y += tankHeight / 5;
                    break;
            }
        }

        public bool ShouldBeDestroyed()
        {
            if (destroyed)
            {
                // Already destroyed
                return false;
            }

            if (x < 0 || x > 800 - spriteWidth * 2 || y < 0 || y > 600 - spriteHeight * 2)
            {
                // Out of screen
                return true;
            }

            return false;
        }

        public bool IsDestroyed()
        {
            if (destroyed && sourceRect.X > spriteWidth * 18)
            {
                return true;
            }

            return false;
        }

        public void Destroy()
        {
            spriteWidth = bitmap.Width / 25;
            spriteHeight = bitmap.Height / 16;

            sourceRect = new Rectangle(spriteWidth * 15, // X position of bullet on the sprite map
                spriteHeight * 8, // Y position of bullet on the sprite map
                spriteWidth, spriteHeight);

            destroyed = true;
        }
    }
}
