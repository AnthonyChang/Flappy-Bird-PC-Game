using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.XNA;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace AnthonyChangFinalProject
{
    class Bird : DrawableGameComponent
    {
        Game1 g;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;

        const int DEFAULTFRAMEWIDTH = 35;
        const int DEFAULTFRAMEHEIGHT = 25;

        const int DEFAULTFRAME = 0;
        const int FLYFRAME1 = 1;
        const int FLYFRAME2 = 2;

        List<Rectangle> flyFrames;

        const int FRAMEDELAYMAXCOUNT = 2;
        int currentFrameDelayCount = 0;

        bool flapping = false;

        const float GRAVITY = 0.01f;

        public int currentFlyPower = 0;
        const int FLYPOWER = -1;
        const float FLYSTEP = 1.001f;

        private int currentFrame = DEFAULTFRAME;

        public Vector2 velocity;

        public Rectangle bird;
        public SoundEffect flap;

        public Bird(Game game, SpriteBatch spriteBatch, Texture2D spriteSheet) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteSheet = spriteSheet;
            g = (Game1)game;

            bird = new Rectangle(60, 195, DEFAULTFRAMEWIDTH, DEFAULTFRAMEHEIGHT);

            velocity = new Vector2(0);

            flyFrames = new List<Rectangle>();

            // Default frame
            flyFrames.Add(new Rectangle(229, 761, 35, 25));

            // Flap animation
            flyFrames.Add(new Rectangle(229, 813, 35, 25));
            flyFrames.Add(new Rectangle(229, 865, 35, 25));

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spriteSheet,
                bird,
                flyFrames.ElementAt<Rectangle>(currentFrame),
                Color.White,
                0f,
                new Vector2(0), 
                SpriteEffects.None,
                0f
                );
            //spriteBatch.DrawRectangle(bird, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //during playable time; after instrctions, before the game is over
            if (g.actionScene.doneInstructions)
            {

                if (!g.actionScene.gameIsOver)
                {
                    float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    velocity.X = 0;
                    velocity.Y += GRAVITY * deltaTime;

                    KeyboardState keyState = Keyboard.GetState();
                    if (keyState.IsKeyDown(Keys.Space))
                    {
                        if (!flapping)  // ready to jump
                        {
                            flapping = true;
                            currentFlyPower = FLYPOWER;  // this is maximum "thrust" at the very beginning of jump
                        }
                    }

                    if (flapping)
                    {
                        if (currentFlyPower < 0)  // we still have upward thrust 
                        {
                            currentFrameDelayCount++;
                            if (currentFrameDelayCount > FRAMEDELAYMAXCOUNT)
                            {
                                currentFrameDelayCount = 0;
                                currentFrame++;             //animate flapping
                                flap.Play(0.4f, 0, 0);      //play flap sound
                            }

                            velocity.Y -= FLYSTEP;
                            currentFlyPower++;
                        }
                        else
                        {
                            flapping = false;   //now we are falling
                            currentFrame = DEFAULTFRAME;
                        }
                    }

                    if (bird.Y > -100 && bird.Y < 400)
                        bird.Y = bird.Y + (int)velocity.Y;
                }
            }
            base.Update(gameTime);
        }

        public void Reset()
        {
            currentFlyPower = 0;
            velocity = new Vector2(0,0);
            bird.Y = 195;                   //Changes start position
        }

        //check intersection with birds
        public bool Intersect(Rectangle rectangle)
        {
            return bird.Intersects(rectangle);
        }
    }
}
