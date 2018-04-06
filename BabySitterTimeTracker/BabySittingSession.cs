using System;
using System.Runtime.Serialization;

namespace BabySitterTimeTracker
{
    [Serializable()]
    public class BabySittingSession : ISerializable
    {
        public int startTime { get; private set; }
        public int endTime { get; private set; }

        public BabySittingSession()
        {
            this.startTime = -1;
            this.endTime = -1;
        }

        //Deserialization constructor.
        public BabySittingSession(SerializationInfo info)
        {
            //Get the values from info and assign them to the appropriate properties
            this.startTime = (int)info.GetValue("startTime", typeof(int));
            this.endTime = (int)info.GetValue("endTime", typeof(int));
        }

        protected BabySittingSession(SerializationInfo info, StreamingContext context)
        {
            this.startTime = (int)info.GetValue("startTime", typeof(int));
            this.endTime = (int)info.GetValue("endTime", typeof(int));
        }
        
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("startTime", startTime);
            info.AddValue("endTime", endTime);
        }
        
        public void print()
        {
            System.Console.WriteLine($"startTime is {this.displayTimeEntry(this.startTime)}, endTime is {this.displayTimeEntry(this.endTime)}");
        }

        public void setStartTime(int hour)
        {
            this.startTime = setTime(hour);
        }

        public void setEndTime(int hour)
        {
            this.endTime = setTime(hour);
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
            return (this.endTime > 12) ? $"{(hour - 12)} AM" : $"{hour} PM";
        }
    }
}
