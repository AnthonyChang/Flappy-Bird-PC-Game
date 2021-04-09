using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.XNA;

namespace AnthonyChangFinalProject
{
    class Background : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;

        const int SCREENHEIGHT = 510;
        const int SCREENWIDTH = 286;
        private Rectangle frame;
        private Vector2 position1;
        private Vector2 position2;
        public Vector2 speed;
        private Rectangle sky = new Rectangle(0, 0 , 286,0);
        private Rectangle ground = new Rectangle(0, 510 - 110, 286, 0);

        public Background(Game game, SpriteBatch spriteBatch, Texture2D spriteSheet, Vector2 position, Rectangle frame, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteSheet = spriteSheet;
            this.frame = frame;
            this.position1 = position;
            this.position2 = new Vector2(position.X + SCREENWIDTH, position.Y);
            this.speed = speed;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //draw background
            spriteBatch.Draw(spriteSheet, position1, frame, Color.White);
            spriteBatch.Draw(spriteSheet, position2, frame, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //scrolling
            position1 -= speed;
            position2 -= speed;
            if (position1.X < -SCREENWIDTH)
            {
                position1.X = position2.X + SCREENWIDTH;
            }
            if (position2.X < -SCREENWIDTH)
            {
                position2.X = position1.X + SCREENWIDTH;
            }

            base.Update(gameTime);
        }

        //check intersection top and bottom playable area
        public bool Intersect(Rectangle rectangle)
        {
            if (ground.Intersects(rectangle) || sky.Intersects(rectangle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

