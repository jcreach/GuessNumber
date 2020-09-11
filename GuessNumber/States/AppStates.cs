using System;
using System.Collections.Generic;
using System.Text;

namespace GuessNumber.States
{
    public enum AppStates
    {
        Idle,
        Init,
        Guessing,
        Exiting, 
        Terminated
    }
}
