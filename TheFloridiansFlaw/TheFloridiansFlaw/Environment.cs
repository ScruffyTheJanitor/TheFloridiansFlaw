using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheFloridiansFlaw
{
    class Environment
    {
        /// <summary>
        /// This class will be used for
        /// drawing the any environmental features
        /// that the game will have. This includes grounds,
        /// backgrounds, trees, clouds, etc.
        /// </summary>


        private Texture2D elementTexture;
        private Rectangle elementRect;
        private Viewport viewport;
        public Environment(Texture2D _elementTexture, Rectangle _elementRect)
        {
            this.elementTexture = _elementTexture;
            this.elementRect = _elementRect;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(elementTexture, elementRect, Color.White);
            spriteBatch.End();
        }
    }
}