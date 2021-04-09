using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyChangFinalProject
{
    public class StartScene : GameScene
    {
        Game1 g;
        public MenuComponent Menu { get; set; }

        SpriteBatch spriteBatch;
        string[] menuItems = {"Start Game",
                                "About",
                                "Help",
                                "Quit"};
        public StartScene(Game game) : base(game)
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
            Background ground = new Background(g, spriteBatch, spriteSheet, groundPosition, groundFrame, new Vector2(2.4f,0));

            // title
            Rectangle titleFrame = new Rectangle(701, 181, 180, 49);
            Vector2 titlePosition = new Vector2(54, 50);
            Background title = new Background(g, spriteBatch, spriteSheet, titlePosition, titleFrame, new Vector2(0, 0));

            // Continue Instructions
            Rectangle enterContinueFrame = new Rectangle(712, 326, 202 ,15);
            Vector2 enterContinuePosition = new Vector2(38, 450);
            Background enterContinue = new Background(g, spriteBatch, spriteSheet, enterContinuePosition, enterContinueFrame, new Vector2(0, 0));

            // menu
            this.spriteBatch = g.spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont highlightFont = game.Content.Load<SpriteFont>("Fonts/hilightFont");


            Menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menuItems);
            this.Components.Add(farBackground);
            this.Components.Add(ground);
            this.Components.Add(title);
            this.Components.Add(Menu);
            this.Components.Add(enterContinue);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
