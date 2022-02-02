
namespace WinkingCat.HelperLibs
{
    public class TIMER : System.Windows.Forms.Timer
    {
        public TIMER() : base()
        { base.Enabled = true; }

        public TIMER(System.ComponentModel.IContainer container) : base(container)
        { base.Enabled = true; }

        private bool _Enabled;
        public override bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public void SetInterval(int i)
        {
            base.Enabled = false;
            base.Interval = i;
            base.Enabled = true;
        }

        protected override void OnTick(System.EventArgs e)
        { if (this.Enabled) base.OnTick(e); }
    }
}
