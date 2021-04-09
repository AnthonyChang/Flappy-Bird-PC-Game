using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyChangFinalProject
{
    public class ActionScene : GameScene
    {
        SpriteBatch spriteBatch;

        Game1 g;

        private Bird bird1;
        private Pipe pipe;
        private Background ground;
        private Background farBackground;
        private Hud hud;
        private Vector2 scorePosition;

        public bool doneInstructions = false;
        public bool gameIsOver;

        private SoundEffect flap;
        private SoundEffect coin;
        private SoundEffect hit;

        // set speed
        private Vector2 scoreSpeed = new Vector2(2.4f, 0);  //make sure to set in ResetAction aswell
        public Vector2 pipeSpeed = new Vector2(2.4f, 0);
        public Vector2 groundSpeed = new Vector2(2.4f, 0);
        public Vector2 backgroundSpeed = new Vector2(0.9f, 0);
        public Vector2 stopSpeed = new Vector2(0, 0);

        public ActionScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.gameIsOver = false;

        // TODO: use this.Content to load your game content here
        Texture2D spriteSheet = g.Content.Load<Texture2D>("images/FlappySpriteSheet");  

            Vector2 stage = new Vector2(286, 510);

            // load sound
            flap = g.Content.Load<SoundEffect>("sounds/BirdFlap");   //flap sound from open game art https://opengameart.org/content/flap-splat-poo-yo-frankie
            coin = g.Content.Load<SoundEffect>("sounds/Coin");       //coin sound from open game art https://opengameart.org/content/completion-sound
            hit = g.Content.Load<SoundEffect>("sounds/hit");         //hit sound from open game art https://opengameart.org/content/punch-slap-n-kick

            // background scroll
            Rectangle backgroundFrame;

            // if after 6PM background turns to night
            if (DateTime.Now.Hour < 18)
            {
                backgroundFrame = new Rectangle(0, 0, 286, 510);
            }
            else
            {
                backgroundFrame = new Rectangle(293, 0, 286, 510);
            }

            // postion background 
            Vector2 backgroundPosition = new Vector2(0, stage.Y - backgroundFrame.Height);

            // instantiate new class background
            farBackground = new Background(g, spriteBatch, spriteSheet, backgroundPosition, backgroundFrame, backgroundSpeed);

            // ground scroll
            Rectangle groundFrame = new Rectangle(585, 0, 286, 110);
            Vector2 groundPosition = new Vector2(0, stage.Y - groundFrame.Height);
            ground = new Background(g, spriteBatch, spriteSheet, groundPosition, groundFrame, groundSpeed);

            // pipe scroll

            Rectangle higherPipeFrame = new Rectangle(112, 646, 52, 320);
            Rectangle lowerPipeFrame = new Rectangle(168, 646, 52, 320);
            pipe = new Pipe(g, spriteBatch, spriteSheet, higherPipeFrame, lowerPipeFrame, pipeSpeed);

            // bird 
            bird1 = new Bird(g, spriteBatch, spriteSheet);
            bird1.flap = this.flap;

            // hud
            hud = new Hud(g, spriteBatch, spriteSheet);

            // add to screen, order matters
            this.Components.Add(farBackground);
            this.Components.Add(ground);
            this.Components.Add(bird1);
            this.Components.Add(pipe);
            this.Components.Add(hud);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // if user done with instructions
            if (ks.IsKeyDown(Keys.Space))
            {

                doneInstructions = true;
            }

            // if user done with instructions
            if (doneInstructions)
            {
                // start scorePosition update
                scorePosition -= scoreSpeed;
            }

            // add a point 
            if (scorePosition.X < -286 - 50)
            {
                coin.Play();
                hud.AddScore();
                scorePosition.X = 0;
            }

            //during playable time 
            if(!gameIsOver && doneInstructions)
            {
                //check if bird intersects with a pipe the ground or the sky
                if (pipe.Intersect(bird1.bird) || ground.Intersect(bird1.bird))
                {
                    scorePosition.X = 0;
                    pipe.speed = stopSpeed;
                    ground.speed = stopSpeed;
                    scoreSpeed = stopSpeed;
                    //play bird hit sound
                    hit.Play(0.4f, 0, 0);
                    gameIsOver = true;
                }
            }

            //if the player ha been defeated
            if (gameIsOver)
            {
                if (ks.IsKeyDown(Keys.LeftShift) || ks.IsKeyDown(Keys.RightShift))
                {
                    BackToMenu();
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void ResetAction()
        {
            bird1.Reset();
            pipe.Reset();
            hud.scoreSystem.ResetScore();
            scorePosition.X = 0;
            gameIsOver = false;
            doneInstructions = false;
            ground.speed = groundSpeed;
            scoreSpeed = new Vector2(2.4f, 0);
        }
        private void BackToMenu()
        {
            gameIsOver = false;
            g.hideAllScenes();
            g.startScene.show();
            g.MenuControls();
        }
    }
}
