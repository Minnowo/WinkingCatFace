﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.HelperLibs;

namespace WinkingCat
{
    public class TaskExecutedEvent : EventArgs
    {
        public Tasks task { get; private set; }
        public TaskExecutedEvent(Tasks t)
        {
            task = t;
        }
    }
}