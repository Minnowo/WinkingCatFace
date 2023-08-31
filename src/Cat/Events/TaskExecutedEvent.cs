using System;
using WinkingCat.HelperLibs;

namespace WinkingCat
{
    public class TaskExecutedEvent : EventArgs
    {
        public Function task { get; private set; }
        public TaskExecutedEvent(Function t)
        {
            task = t;
        }
    }
}
