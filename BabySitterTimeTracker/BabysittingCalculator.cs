using System;
using System.Collections.Generic;
using System.Text;

namespace BabySitterTimeTracker
{
    public class BabysittingCalculator : ICalculator
    {
        public const int START_TO_BED_RATE = 12;
        public const int BED_TO_MIDNIGHT_RATE = 8;
        public const int MIDNIGHT_TO_END_RATE = 16;


        public decimal Calculate(BabySittingSession babySittingSession)
        {
            decimal cost = 0;
            if (babySittingSession.validateTimes())
            {
                cost = this.calculateStartToBed(babySittingSession.bedTime, babySittingSession.startTime) 
                        + this.calculateBedToMidnight(babySittingSession.bedTime, babySittingSession.endTime) 
                        + this.calculateMidToEnd(babySittingSession.bedTime, babySittingSession.endTime);
            }
            else
            {
                throw new FormatException("Times are incorrect. Make sure start <= bed <= end. Correct and try again.");
            }

            return cost;
        }

        private int calculateStartToBed(int bedtime, int starttime)
        {
            int span = 0;
            if (bedtime > 12)
            {
                span = 12 - starttime;
                span += bedtime - 12;
            }
            else
            {
                span = bedtime - starttime;
            }

            return span * START_TO_BED_RATE;
        }

        /// <summary>
        /// Even if bedtime is after midnight, we use that value as upper bound
        /// </summary>
        /// <param name="bedtime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        private int calculateBedToMidnight(int bedtime, int endtime)
        {
            int span = 0;
            if (bedtime <= 12)
            {
                if (endtime <= 12)
                {
                    span = endtime - bedtime;
                }
                else
                {
                    span = 12 - bedtime;
                }
            }

            return span * BED_TO_MIDNIGHT_RATE;
        }

        private int calculateMidToEnd(int bedtime, int endtime)
        {
            int span = 0;
            
            if (endtime > 12)
            {
                if (bedtime > 12)
                    span = endtime - bedtime;
                else 
                    span = endtime - 12; 
            }

            return span * MIDNIGHT_TO_END_RATE;
        }
    }
}
