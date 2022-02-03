using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace WinkingCat.HelperLibs
{
    public static class ClipManager
    {
        public static Dictionary<string, ClipForm> Clips { get; private set; } = new Dictionary<string, ClipForm> { };

        /// <summary>
        /// Creates a new clip at the cursor.
        /// </summary>
        /// <param name="img">The image to display. </param>
        /// <param name="cloneImage">If the image should be cloned to avoid disposing issues.</param>
        /// <returns></returns>
        public static string CreateClipAtCursor(Image img, bool cloneImage = true)
        {
            Point p = ScreenHelper.GetCursorPosition();

            ClipOptions ops = new ClipOptions(new Point(p.X - img.Width / 2, p.Y - img.Height / 2));

            return CreateClip(img, ops, cloneImage);
        }

        /// <summary>
        /// Creates a new clip from the given image with the given options.
        /// </summary>
        /// <param name="clipImg">The image to display. </param>
        /// <param name="options">The options to use.</param>
        /// <param name="cloneImage">If the image should be cloned to avoid disposing issues.</param>
        /// <returns></returns>
        public static string CreateClip(Image clipImg, ClipOptions options, bool cloneImage = true)
        {
            if(cloneImage)
                Clips[options.Name] = new ClipForm(options, clipImg.CloneSafe());
            else
                Clips[options.Name] = new ClipForm(options, clipImg);

            return options.Name;
        }

        /// <summary>
        /// Destroys the given clip.
        /// </summary>
        /// <param name="clipName">The name of the clip to destroy.</param>
        public static void DestroyClip(string clipName)
        {
            if (Clips.ContainsKey(clipName))
            {
                Clips[clipName]?.Dispose();
                Clips.Remove(clipName); 
            }

            if(InternalSettings.Garbage_Collect_After_Clip_Destroyed)
                GC.Collect();
        }

        /// <summary>
        /// Destroys all the clips.
        /// </summary>
        public static void DestroyAllClips()
        {
            string[] names = Clips.Keys.ToArray();
            foreach(string clipName in names)
            {
                Clips[clipName]?.Dispose();
                Clips.Remove(clipName);
            }

            if(InternalSettings.Garbage_Collect_After_All_Clips_Destroyed)
                GC.Collect();
        }
    }
}
