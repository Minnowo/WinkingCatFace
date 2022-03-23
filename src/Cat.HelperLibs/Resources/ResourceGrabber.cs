using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs.Resources
{
    public static class ResourceGrabber
    {
        public static object GetResource(string name)
        {
            return Properties.Resources.resourceMan.GetObject(name, Properties.Resources.resourceCulture);
        }
    }
}
