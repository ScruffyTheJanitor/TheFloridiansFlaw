using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
            
namespace TheFloridiansFlaw
{
    public class AnimatedSprite
    {
        Game1 game = new Game1();
        // The running spritesheet and the jumping texture for Anna.
        private Texture2D Texture { get; set; }
        private Texture2D jumpTexture { get; set; }
        // Number of rows in the spritesheet.
        private int Rows { get; set; }
        // Number of columns in the spritesheet.
        private int Columns { get; set; }
        // The current frame being drawn.
        private int currentFrame;
        // The total number of frames in the spritesheet.
        private int totalFrames;
        // The very left side of the screen. Increases incrementally.
        private int backBound;
        // Deadzones that the Camera will not pick up on
        private int deadZone1 = 480;
        private int deadZone2 = 520;
        // Ground level, screen height - texture height
        public int groundLevel = (720 - 130);
        // Player's health (def. 100)
        private int health = 100;
        // The ID of the object. Anna = 0
        private int _ID;

        // The position of the player.
        private Vector2 Position;
        // The velocity of the player.
        private Vector2 Velocity = new Vector2(0f, 0f);

        // Checks if the player is flipped
        private bool flipped;
        // Checks if the player is frozen (def. false).
        private bool isFrozen = false;
        // Checks if the developer hud is enabled (def. false)
        private bool devEnabled = false;
        // Checks if the player has jumped (def. false)
        private bool hasJumped = false;
        // Checks if the introduction has finished playing (def. false).
        private bool finishedIntro = false;

        // The font to draw with.
        private SpriteFont devFont;
        // The speed at which the player will move.
        private float speed = 15f;

        // The rectangle of the player.
        private Rectangle annaRect;

        // The camera of the scene.
        private Camera cam;

        // The viewport of the camera
        private Viewport viewport;

        // Constructor of the AnimatedSprite object.
        public AnimatedSprite(
            Texture2D texture,
            Texture2D jumpTexture,
            int rows, 
            int columns, 
            Vector2 position,
            SpriteFont devFont,
            int _ID,
            Viewport _viewport)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            this.Position = position;
            this.devFont = devFont;
            this._ID = _ID;
            this.jumpTexture = jumpTexture;
            this.viewport = _viewport;
            cam = new Camera(viewport);
        }

        public void Update()
        {

            /*
             * TODO:
             * Make the camera move right with Anna IF AND ONLY IF
             * she is within the deadzone (480 to 520).
             * DO NOT move the camera left.
             */

            if ((Keyboard.GetState().IsKeyDown(Keys.D)) && (!isFrozen))
            {
                StartAnimation();
                flipped = false;
                Position.X += speed;
            }
            else if ((Keyboard.GetState().IsKeyDown(Keys.A)) && (!isFrozen))
            {
                flipped = true;
                StartAnimation();
                Position.X -= speed;
            }
            else
            {
                StopAnimation();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (devEnabled)
                {
                    devEnabled = false;
                }
                else if (!devEnabled)
                {
                    devEnabled = true;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                cam._pos.X = 0;
                finishedIntro = true;
            }

            if (Position.X < backBound)
            {
                Position.X = backBound + 1;
                isFrozen = false;
            }

            if (cam._pos.X != 0)
            {
                cam._pos.X -= 6;
                isFrozen = true;
            }
            else
            {
                isFrozen = false;
            }

            if (cam._pos.X < 0)
            {
                cam._pos.X = 0;
                finishedIntro = true;
            }

            Position += Velocity;
            cam.Update();

            annaRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        }

        private void DuplicationCheck()
        {
            List<int> enumerator = new List<int>();

            foreach (var i in game.spriteList)
            {
                enumerator.Add(i._ID);
                foreach (var t in enumerator)
                {
                    if (i._ID == t)
                    {
                        Console.WriteLine("Duplication found! Object ID #" + t);
                        System.Environment.Exit(0);   
                    }
                    else
                    {
                        Console.WriteLine("No duplications found! Continuing task...");
                    }
                } 
            }
        }

        public void StartAnimation()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }

        public void StopAnimation()
        {
            while (currentFrame > 0)
            {
                currentFrame--;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y + (720 - 130), width, height);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, cam.Transform);

            if (flipped)
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
            if (devEnabled)
            {
                spriteBatch.DrawString(devFont, "x: " + Position.X + " y: " + Position.Y * -1, new Vector2(Position.X, Position.Y + 720 - Texture.Height - 50), Color.Black);
            }
            spriteBatch.DrawString(devFont, "Welcome to The Anna Schneider Chronicles: Chapter 1\nThe Floridian's Flaw!", new Vector2(-640, 360), Color.Black);
            spriteBatch.DrawString(devFont, "Press X to enable developer HUD \n(disabled until intro sequence complete)", new Vector2(-640, 420), Color.Black);
            spriteBatch.End();

            spriteBatch.Begin();

            if (finishedIntro)
            {
                if (devEnabled)
                {
                    spriteBatch.DrawString(devFont, "bool hasJumped = " + hasJumped, Vector2.Zero, Color.Black);
                    spriteBatch.DrawString(devFont, "bool flipped = " + flipped, new Vector2(0, 30), Color.Black);
                    spriteBatch.DrawString(devFont, "annaRect: (" + annaRect.X + ", " + annaRect.Y * -1 + ")", new Vector2(0, 60), Color.Black);
                    spriteBatch.DrawString(devFont, "Cam x: " + cam.Pos.X + " Cam y: " + cam.Pos.Y, new Vector2(0, 240), Color.Black);
                }
                spriteBatch.DrawString(devFont, "Press X to enable the developer HUD", new Vector2(0, 120), Color.Black);
                spriteBatch.DrawString(devFont, "Volume Up: Page Up", new Vector2(0, 150), Color.Black);
                spriteBatch.DrawString(devFont, "Volume Down: Page Down", new Vector2(0, 210), Color.Black);
            }
            spriteBatch.End();
        }
    }
}