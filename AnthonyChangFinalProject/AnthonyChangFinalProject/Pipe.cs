using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.XNA;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;

namespace AnthonyChangFinalProject
{
    class Pipe : DrawableGameComponent
    {
        Game1 g;

        SpriteBatch spriteBatch;
        Texture2D spriteSheet;

        const int SPACEBETWEENWIDTH =  286 + TEXTUREWIDTH;
        const int TEXTUREWIDTH = 51;
        const int TEXTUREHEIGHT = 320;

        private Vector2 scoreSpeed = new Vector2(1, 0);

        Rectangle higherFrame;
        Rectangle lowerFrame;
        Vector2 position1;
        Vector2 position2;
        Vector2 position3;
        public Vector2 speed;

        Random random;

        int spaceBetweenHeight;
        int heightOfPipes;

        //list of pipes to look for intersections
        private Rectangle[] pipeList;

        public Pipe(Game game, SpriteBatch spriteBatch, Texture2D spriteSheet, Rectangle higherFrame, Rectangle lowerFrame, Vector2 speed) : base(game)
        {
            this.g = (Game1)game;
            this.spriteBatch = spriteBatch;
            this.spriteSheet = spriteSheet;

            //higherpipe
            this.higherFrame = higherFrame;
            //lowerpipe
            this.lowerFrame = lowerFrame;
            //default pipe starting X and Y
            this.position1 = new Vector2(0 , -300);
            this.position2 = new Vector2(0  + SPACEBETWEENWIDTH, -300);
            this.position3 = new Vector2(0  + SPACEBETWEENWIDTH + SPACEBETWEENWIDTH, -300);
            this.speed = speed;

            random = new Random();

            this.spaceBetweenHeight = random.Next(0, 100);
            this.heightOfPipes = random.Next(0, 220);

            pipeList = new Rectangle[6];

        }

        public override void Draw(GameTime gameTime)
        {
            //after player has read instructions draw all pipes
            if (g.actionScene.doneInstructions == true)
            {
                //instantiate pipes and add them to pipeList 
                spriteBatch.Begin();
                spriteBatch.Draw(spriteSheet, pipeList[0] = new Rectangle((int)position1.X, (int)position1.Y + heightOfPipes, TEXTUREWIDTH, TEXTUREHEIGHT), higherFrame, Color.White);
                spriteBatch.Draw(spriteSheet, pipeList[1] = new Rectangle((int)position1.X, (int)position1.Y + 490 + heightOfPipes - spaceBetweenHeight, TEXTUREWIDTH, TEXTUREHEIGHT), lowerFrame, Color.White);
                spriteBatch.Draw(spriteSheet, pipeList[2] = new Rectangle((int)position2.X, (int)position2.Y + heightOfPipes, TEXTUREWIDTH, TEXTUREHEIGHT), higherFrame, Color.White);
                spriteBatch.Draw(spriteSheet, pipeList[3] = new Rectangle((int)position2.X, (int)position2.Y + 490 + heightOfPipes - spaceBetweenHeight, TEXTUREWIDTH, TEXTUREHEIGHT), lowerFrame, Color.White);
                spriteBatch.Draw(spriteSheet, pipeList[4] = new Rectangle((int)position3.X, (int)position3.Y + heightOfPipes, TEXTUREWIDTH, TEXTUREHEIGHT), higherFrame, Color.White);
                spriteBatch.Draw(spriteSheet, pipeList[5] = new Rectangle((int)position3.X, (int)position3.Y + 490 + heightOfPipes - spaceBetweenHeight, TEXTUREWIDTH, TEXTUREHEIGHT), lowerFrame, Color.White);
                //foreach (Rectangle r in pipeList)
                //    spriteBatch.DrawRectangle(r, Color.Red);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // speed will not start until playable time in action scene
            if (g.actionScene.doneInstructions && !g.actionScene.gameIsOver)
            {
                //scrolling
                position1 -= speed;
                position2 -= speed;
                position3 -= speed;
                if (position1.X + TEXTUREWIDTH < -SPACEBETWEENWIDTH)
                {
                    position1.X = position2.X + SPACEBETWEENWIDTH;
                    //random space between pipes and the height of the gap relative to the screen
                    this.spaceBetweenHeight = random.Next(0, 100);
                    this.heightOfPipes = random.Next(0, 220);
                }
                if (position2.X + TEXTUREWIDTH < -SPACEBETWEENWIDTH)
                {
                    position2.X = position3.X + SPACEBETWEENWIDTH;
                    this.spaceBetweenHeight = random.Next(0, 100);
                    this.heightOfPipes = random.Next(0, 220);
                }
                if (position3.X + TEXTUREWIDTH < -SPACEBETWEENWIDTH)
                {
                    position3.X = position1.X + SPACEBETWEENWIDTH;
                    this.spaceBetweenHeight = random.Next(0, 100);
                    this.heightOfPipes = random.Next(0, 220);
                }
            }
            base.Update(gameTime);
        }

        public void Reset()
        {
            //reset to default settings
            position1 = new Vector2(0, -300);
            position2 = new Vector2(0 + SPACEBETWEENWIDTH, -300);
            position3 = new Vector2(0 + SPACEBETWEENWIDTH + SPACEBETWEENWIDTH, -300);
            //fetches speed from master controls in action scene
            speed = g.actionScene.pipeSpeed;
        }

        //check intersections with pipes
        public bool Intersect(Rectangle rectangle)
        {
            for (int i = 0; i < pipeList.Length; i++)
            {
                if (pipeList[i].Intersects(rectangle))
                {
                    return true;
                }
            }
            return false;
        }
    }
}


