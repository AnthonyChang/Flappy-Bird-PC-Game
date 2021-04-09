using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnthonyChangFinalProject
{
    public class HelpScene : GameScene
    {
        Game1 g;
        SpriteBatch spriteBatch;
        public HelpScene(Game game) : base(game)
        {
            this.g = (Game1)game;

            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D spriteSheet = g.Content.Load<Texture2D>("images/FlappySpriteSheet");

            // background
            Rectangle backgroundFrame;

            if (DateTime.Now.Hour < 18)
            {
                backgroundFrame = new Rectangle(0, 0, 286, 510);
            }
            else
            {
                backgroundFrame = new Rectangle(294, 0, 286, 510);
            }

            Vector2 backgroundPosition = new Vector2(0, Shared.stage.Y - backgroundFrame.Height);
            Background farBackground = new Background(g, spriteBatch, spriteSheet, backgroundPosition, backgroundFrame, new Vector2(0.9f, 0));

            // ground
            Rectangle groundFrame = new Rectangle(585, 0, 286, 110);
            Vector2 groundPosition = new Vector2(0, Shared.stage.Y - groundFrame.Height);
            Background ground = new Background(g, spriteBatch, spriteSheet, groundPosition, groundFrame, new Vector2(2.4f, 0));

            // name for about page
            Rectangle nameFrame = new Rectangle(293, 515, 286, 510);
            Vector2 namePosition = new Vector2(0,0);
            Background name = new Background(g, spriteBatch, spriteSheet, namePosition, nameFrame, new Vector2(0, 0));

            // Continue Instructions
            Rectangle shiftContinueFrame = new Rectangle(712, 307, 197, 15);
            Vector2 shiftContinuePosition = new Vector2(38, 450);
            Background shiftContinue = new Background(g, spriteBatch, spriteSheet, shiftContinuePosition, shiftContinueFrame, new Vector2(0, 0));

            this.Components.Add(farBackground);
            this.Components.Add(ground);
            this.Components.Add(name);
            this.Components.Add(shiftContinue);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.LeftShift) || ks.IsKeyDown(Keys.RightShift))
            {
                g.hideAllScenes();
                g.startScene.show();
                g.MenuControls();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
