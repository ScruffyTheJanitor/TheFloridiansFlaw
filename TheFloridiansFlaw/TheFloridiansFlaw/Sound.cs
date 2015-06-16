using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace TheFloridiansFlaw
{
    public class Sound
    {
        /// <summary>
        ///  This class contains methods that can be called
        ///  to play a certain sound/song at certain
        ///  parts of the game.
        /// </summary>

        public Sound(Song song)
        {
            MediaPlayer.IsRepeating = true;
            Play(song);   
        }
        public void Play(Song song)
        {
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.2f;
        }
        public void VolUp()
        {
            MediaPlayer.Volume += 0.1f;
        }
        public void VolDown()
        {
            MediaPlayer.Volume -= 0.1f;
        }
    }
}