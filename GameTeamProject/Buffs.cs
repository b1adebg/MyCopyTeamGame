using System.Collections.Generic;
using System.Drawing;

namespace GameTeamProject
{
    public class Buffs
    {
        protected List<string> buffNames = new List<string> {  "shield", "freeze", "boose", "rankUp", "shazam", "lifeUp", "powerOverwhelming" };
        protected List<int> durations = new List<int> { 10, 7, 5, 0, 0, 0, 10 };

        protected int posX; 
        protected int posY; 
        protected int lifeTime = 10;

        protected int spriteWidth;    
        protected int spriteHeight;
        protected Bitmap bitmap;
        protected Rectangle sourceRect;

        protected bool buffExists;
        protected bool playerIsBuffed;


        public void CreateBuff(string name, int x, int y)
        {
            int buffType = buffNames.IndexOf(name);
            this.posX = x;
            this.posY = y;
            bitmap = GameTeamProject.Properties.Resources.sprites;
            spriteWidth = bitmap.Width / 25;
            spriteHeight = bitmap.Height / 16;
            sourceRect = new Rectangle(spriteWidth * 16 + buffType * spriteWidth, spriteHeight * 7 + buffType * spriteHeight, spriteWidth, spriteHeight);

        }

    }
}
