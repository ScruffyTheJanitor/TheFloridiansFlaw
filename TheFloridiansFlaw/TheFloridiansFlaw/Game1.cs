using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheFloridiansFlaw
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int groundLevel;
        public SpriteFont devFont;

        // AnimatedSprite
        private Texture2D annaTexture;
        private Texture2D jumpTexture;
        private AnimatedSprite anna;
        private Vector2 annaLocation = Vector2.Zero;
        public List<AnimatedSprite> spriteList = new List<AnimatedSprite>();
        
        // Dialogue Box Variables
        private Texture2D dialogueTexture;
        private DialogueBox DialogueBox1;

        // Sound/Music
        protected Song song;
        private Sound MusicMaker;

        // Splash Texture
        private Texture2D splash;
        private Rectangle splashRect;

        // Camera
        private Viewport viewport;

        // Environment
        private Environment splashObject;
        private Environment groundObject;
        private Texture2D groundTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            // Set to 7.5 frames per second (1 second cut into 7.5 sections)
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 7.5);

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            viewport = GraphicsDevice.Viewport;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // TODO: use this.Content to load your game content here
            spriteBatch = new SpriteBatch(GraphicsDevice);

            devFont = Content.Load<SpriteFont>("fonts/DevFont");
            annaTexture = Content.Load<Texture2D>("spritesheets/sheet_anna");
            jumpTexture = Content.Load<Texture2D>("spritesheets/anna_jump");
            splash = Content.Load<Texture2D>("environment/splash");
            dialogueTexture = Content.Load<Texture2D>("environment/dialoguebox");
            song = Content.Load<Song>("sound/song");
            groundTexture = Content.Load<Texture2D>("environment/ground/grassblock_single");
            groundObject = new Environment(groundTexture, new Rectangle(360, 640, groundTexture.Width, groundTexture.Height));

            anna = new AnimatedSprite(annaTexture, jumpTexture, 1, 4, new Vector2(0, 0), devFont, 0, viewport);
            MusicMaker = new Sound(song);
            splashRect = new Rectangle(1280-splash.Width, 0, splash.Width, splash.Height);
            splashObject = new Environment(splash, splashRect);

            DialogueBox1 = new DialogueBox(dialogueTexture, new Vector2(100, 100), "Test!");
            
            spriteList.Add(anna);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                MusicMaker.VolUp();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                MusicMaker.VolDown();
            }

            // TODO: Add your update logic here
            anna.Update();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            //spriteBatch.Draw(splash, , Color.White);
            splashObject.Draw(spriteBatch);
            //groundObject.Draw(spriteBatch);
            anna.Draw(spriteBatch);
            
            base.Draw(gameTime);
        }
    }
}