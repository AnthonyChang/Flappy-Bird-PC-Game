using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyChangFinalProject
{
    class Hud : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;

        Game1 g;

        public Score scoreSystem;
        private List<Rectangle> numberTextures;
        private Rectangle gameOverTexture;
        private Rectangle getReadyTexture;
        private Rectangle instuctionTexture;
        private Rectangle endScreenTexture;
        private Rectangle continueTexture;
        private List<Rectangle> medalTextures;


        public Hud(Game game, SpriteBatch spriteBatch, Texture2D spriteSheet) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteSheet = spriteSheet;
            this.g = (Game1)game;

            scoreSystem = new Score();
            scoreSystem.ResetScore();

            numberTextures = new List<Rectangle>();
            medalTextures = new List<Rectangle>();


            //adding position of rectangles in spritesheet from https://www.spriters-resource.com/mobile/flappybird/sheet/59894/
            //some modifications have been made to the spritesheet for use with PC
            numberTextures.Add(new Rectangle(991, 119, 26, 38));    //0
            numberTextures.Add(new Rectangle(271, 909, 18, 38));    //1
            numberTextures.Add(new Rectangle(583, 319, 26, 38));    //2
            numberTextures.Add(new Rectangle(611, 319, 26, 38));    //3
            numberTextures.Add(new Rectangle(639, 319, 26, 38));    //4
            numberTextures.Add(new Rectangle(667, 319, 26, 38));    //5
            numberTextures.Add(new Rectangle(583, 367, 26, 38));    //6
            numberTextures.Add(new Rectangle(611, 367, 26, 38));    //7
            numberTextures.Add(new Rectangle(639, 367, 26, 38));    //8
            numberTextures.Add(new Rectangle(667, 367, 26, 38));    //9

            
            medalTextures.Add(new Rectangle(223, 953, 46, 46));    //bronzeMedelTexture
            medalTextures.Add(new Rectangle(223, 905, 46, 46));    //silverMedelTexture
            medalTextures.Add(new Rectangle(241, 563, 46, 46));    //goldMedelTexture
            medalTextures.Add(new Rectangle(241, 515, 46, 46));    //platniumMedelTexture

            endScreenTexture = new Rectangle(6, 518, 227, 113);     //end screen with medal placement
            gameOverTexture = new Rectangle(789, 117, 194, 44);     //game over header
            getReadyTexture = new Rectangle(589, 117, 186, 52);     //get ready texture
            instuctionTexture = new Rectangle(585, 181, 113, 101);  //instructions texture
            continueTexture = new Rectangle(712, 307, 197, 15);    //shift to continue texture
        }

        public override void Draw(GameTime gameTime)
        {
            //draw instructions
            if (!g.actionScene.doneInstructions && !g.actionScene.gameIsOver)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(spriteSheet, new Rectangle(50, 20, 186, 52),getReadyTexture,Color.White);
                spriteBatch.Draw(spriteSheet, new Rectangle(20, 195, 115, 98), instuctionTexture, Color.White);

                spriteBatch.End();
            }

            //draw end screen with medal placement
            if (g.actionScene.gameIsOver && g.actionScene.doneInstructions)
            {
                spriteBatch.Begin();

                //header
                spriteBatch.Draw(spriteSheet, new Rectangle(50, 20, 186, 52), gameOverTexture, Color.White);

                //menu
                spriteBatch.Draw(spriteSheet, new Rectangle(25, 150, 227, 113), endScreenTexture, Color.White);
                spriteBatch.Draw(spriteSheet, new Rectangle(40, 275, 197, 15), continueTexture, Color.White);
                //medal system
                if (scoreSystem.Scores == scoreSystem.HighScore)
                    spriteBatch.Draw(spriteSheet, new Rectangle(50, 193, 46, 46), medalTextures[3], Color.White);
                else if (scoreSystem.Scores * 1.5 > scoreSystem.HighScore)
                    spriteBatch.Draw(spriteSheet, new Rectangle(50, 193, 46, 46), medalTextures[2], Color.White);
                else if (scoreSystem.Scores * 2 > scoreSystem.HighScore)
                    spriteBatch.Draw(spriteSheet, new Rectangle(50, 193, 46, 46), medalTextures[1], Color.White);
                else
                    spriteBatch.Draw(spriteSheet, new Rectangle(50, 193, 46, 46), medalTextures[0], Color.White);

                spriteBatch.End();
            }

            //draw scores if user is done with instructions
            if (g.actionScene.doneInstructions)
            {
                spriteBatch.Begin();

                DrawScore(spriteBatch, scoreSystem.Scores, false);
                DrawScore(spriteBatch, scoreSystem.HighScore, true);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private void DrawScore(SpriteBatch spriteBatch, int score, bool highScore)
        {
            int y;

            //draw for both highscore and player score
            if (highScore)
            {
                if(!g.actionScene.gameIsOver)
                    y = 450;
                else
                    y = 200;
            }
            else 
            {
                if(!g.actionScene.gameIsOver)
                    y = 20;
                else
                    y = 155;
            }



            //if score is less than 10; one digit
            if (score < 10)
            {
                //center numbers
                Rectangle secondNumber = new Rectangle(130, y, 26, 38);
                //1 has a special width
                Rectangle secondNumberOne = new Rectangle(130, y, 18, 38);
                if (score == 1)
                {
                    spriteBatch.Draw(spriteSheet, secondNumberOne, numberTextures[score], Color.White);
                }
                else
                {
                    spriteBatch.Draw(spriteSheet, secondNumber, numberTextures[score], Color.White);
                }
            }

            // if score has 2 digits
            if (score > 9)
            {
                //if number is in the 10's column
                Rectangle firstNumber = new Rectangle(117, y, 26, 38);
                //1 has a special width
                Rectangle firstNumberOne = new Rectangle(125, y, 18, 38);

                //center numbers
                Rectangle secondNumber = new Rectangle(143, y, 26, 38);
                Rectangle secondNumberOne = new Rectangle(143, y, 18, 38);

                //draw second number 
                int lastnumber = score % 10;

                if(lastnumber == 1)
                {
                    spriteBatch.Draw(spriteSheet, secondNumberOne, numberTextures[lastnumber], Color.White);
                }
                else
                {
                    spriteBatch.Draw(spriteSheet, secondNumber, numberTextures[lastnumber], Color.White);
                }

                //draw first nuber
                if (score / 10 == 1)
                {
                    spriteBatch.Draw(spriteSheet, firstNumberOne, numberTextures[1], Color.White);
                }

                //draw first number if not 1
                if (score > 19 )
                {
                    spriteBatch.Draw(spriteSheet, firstNumber, numberTextures[Math.Abs(score / 10)], Color.White);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //access to score from hud
        public void AddScore()
        {
            scoreSystem.AddScore();
        }
    }
}