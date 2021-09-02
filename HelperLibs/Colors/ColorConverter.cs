using System.ComponentModel;
using System.Drawing;


namespace WinkingCat.HelperLibs
{
    public class _ColorConverter : ColorConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
