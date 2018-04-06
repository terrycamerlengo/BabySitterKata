using System;
using System.Collections.Generic;
using System.Text;

namespace BabySitterTimeTracker
{
    public interface ICalculator
    {
        decimal Calculate(BabySittingSession babySittingSession);
    }
}
