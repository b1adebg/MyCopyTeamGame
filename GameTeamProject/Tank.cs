using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTeamProject
{
    abstract class Tank
    {
        protected int x; // Current x position
        protected int y; // Current y position
        protected int dx; // X direction (-1 = left, 1 = right)
        protected int dy; // Y direction (-1 = up, 1 = down)
        protected int velocity; // Player speed (Pixels per move)
        protected int rank; // Bigger the rank, better the tank
        protected List<Missile> missiles;

        // Sprites
        protected Bitmap bitmap;	// the animation sequence
        protected Rectangle sourceRect;	// the rectangle to be drawn from the animation bitmap
        protected int firstFrame; // the first frame
        protected int currentFrame;   // the current frame
        protected long frameTicker;	// the time of the last frame update
        protected int framePeriod;	// milliseconds between each frame (1000/fps)

        protected int spriteWidth;    // the width of the sprite to calculate the cut out rectangle
        protected int spriteHeight;	// the height of the sprite

        public Tank(int x, int y, int[] tankType, int rank)
        {
            this.x = x;
            this.y = y;
            dx = 0;
            dy = 0;
            velocity = 3;
            this.rank = rank;
            missiles = new List<Missile>();

            // Sprites
            bitmap = GameTeamProject.Properties.Resources.sprites;
            firstFrame = tankType[0];
            currentFrame = tankType[0];
            spriteWidth = bitmap.Width / 25;
            spriteHeight = bitmap.Height / 16;
            sourceRect = new Rectangle(spriteWidth * tankType[0], spriteHeight * (tankType[1] + rank), spriteWidth, spriteHeight);
            framePeriod = 20;
            frameTicker = 0;
        }
    }
}
