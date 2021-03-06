﻿using System;
using System.Runtime.Serialization;

namespace BabySitterTimeTracker
{
    [Serializable()]
    public class BabySittingSession : ISerializable
    {
        public int startTime { get; private set; }
        public int endTime { get; private set; }
        public int bedTime { get; private set; }


        public BabySittingSession()
        {
            this.startTime = -1;
            this.bedTime = -1;
            this.endTime = -1;
        }

        //Deserialization constructor.
        public BabySittingSession(SerializationInfo info)
        {
            //Get the values from info and assign them to the appropriate properties
            this.startTime = (int)info.GetValue("startTime", typeof(int));
            this.bedTime = (int)info.GetValue("bedTime", typeof(int));
            this.endTime = (int)info.GetValue("endTime", typeof(int));
        }

        protected BabySittingSession(SerializationInfo info, StreamingContext context)
        {
            this.startTime = (int)info.GetValue("startTime", typeof(int));
            this.bedTime = (int)info.GetValue("bedTime", typeof(int));
            this.endTime = (int)info.GetValue("endTime", typeof(int));
        }
        
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("startTime", startTime);
            info.AddValue("bedTime", bedTime);
            info.AddValue("endTime", endTime);
        }
        
        public void print()
        {
            System.Console.WriteLine($"startTime is {this.displayTimeEntry(this.startTime)}, bedtime is {this.displayTimeEntry(this.bedTime)},  endTime is {this.displayTimeEntry(this.endTime)}");
        }

        public void setStartTime(int hour)
        {
            this.startTime = setTime(hour);
        }

        public void setEndTime(int hour)
        {
            this.endTime = setTime(hour);
        }

        public void setBedTime(int hour)
        {
            this.bedTime = setTime(hour);
        }

        public int setTime(int hour)
        {
            if (hour < 5)
            {
                return 12 + hour;
            }

            return hour;
        }

        public string displayTimeEntry(int hour)
        {
            return (hour > 12) ? $"{(hour - 12)} AM" : $"{hour} PM";
        }

        /// <summary>
        /// EndTime must be the same (left when kid went to sleep or kid never got to sleep) or later than bedTime.
        /// BedTime must be the same (kid asleep when arrived) or later than Starttime. 
        /// </summary>
        /// <returns></returns>
        public bool validateTimes()
        {
            bool validate = false;
            if ((this.bedTime > 0) && (this.endTime > 0) && (this.startTime > 0))
            {
                if ((this.endTime >= this.bedTime) && (this.bedTime > this.startTime))
                {
                    validate = true;
                }
            }

            return validate;
        }
    }
}
