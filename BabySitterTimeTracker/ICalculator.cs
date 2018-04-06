using System;
using System.Collections.Generic;
using System.Text;

namespace BabySitterTimeTracker
{
    interface ICalculator
    {
        decimal Calculate(BabySittingSession babySittingSession);
    }
}
