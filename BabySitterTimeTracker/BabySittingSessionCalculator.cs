using System;
using System.Collections.Generic;
using System.Text;

namespace BabySitterTimeTracker
{
    public class BabySittingSessionCalculator : ICalculator
    {
        public decimal Calculate(BabySittingSession babySittingSession)
        {
            var diff = babySittingSession.endTime.Subtract(babySittingSession.startTime);

        }
    }
}
