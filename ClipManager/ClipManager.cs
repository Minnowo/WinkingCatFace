using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

namespace WinkingCat.ClipHelper
{
    public static class ClipManager
    {
        public static Dictionary<string, ClipForm> Clips { get; private set; } = new Dictionary<string, ClipForm> { };
        public static ClipOptions Options { get; private set; } = new ClipOptions();

        public static void Init(ClipOptions options)
        {
            Options = options;
        }

        public static string CreateClip(Image clipImg, ClipOptions options)
        {
            string clipName = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ffff");
            Clips[clipName] = new ClipForm(options, clipImg, clipName);
            return clipName;
        }

        public static void DestroyClip(string clipName)
        {
            if (Clips.ContainsKey(clipName))
            {
                Clips[clipName]?.Destroy();
                Clips.Remove(clipName); 
            }
            

        }

        public static void DestroyAllClips()
        {
            string[] names = Clips.Keys.ToArray();
            foreach(string clipName in names)
            {
                Clips[clipName]?.Destroy();
                Clips.Remove(clipName);
            }
        }
    }
}
