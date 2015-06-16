using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheFloridiansFlaw
{
    class DialogueBox
    {
        private Texture2D texture;
        private Vector2 location;
        private Rectangle textArea;
        private string text;
        private Vector2 zero;
        Game1 game = new Game1();

        List<DialogueBox> enabledBoxes = new List<DialogueBox>();

        public DialogueBox(Texture2D texture, Vector2 location, string text)
        {
            this.texture = texture;
            this.location = location;
            this.text = text;
            this.textArea = texture.Bounds;
        }

        public void Update()
        {
            // Check if the mouse clicks on the box, if it does, progress to the next text window, or close it

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture, location, Color.White);
            // TODO: Draw the text here

            spriteBatch.End();
        }
    }
}
