using System;
using System.Collections.Generic;
using System.Text;
using Matcha.BackgroundService;

namespace SMS_Sender2
{
    public class PeriodicReadDB : PeriodicCall
    {
        public PeriodicReadDB(int seconds) : base(seconds)
        {
        }
    }
}
