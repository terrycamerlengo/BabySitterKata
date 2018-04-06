using System;
using System.Runtime.Serialization;

namespace BabySitterTimeTracker
{
    [Serializable()]
    public class BabySittingSession : ISerializable
    {
        public DateTime startTime { get; private set; }
        public DateTime endTime { get; private set; }

        public BabySittingSession()
        {
            this.startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0);
            this.endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 4, 0, 0);
        }

        //Deserialization constructor.
        public BabySittingSession(SerializationInfo info)
        {
            //Get the values from info and assign them to the appropriate properties
            this.startTime = (DateTime)info.GetValue("startTime", typeof(DateTime));
            this.endTime = (DateTime)info.GetValue("endTime", typeof(DateTime));
        }

        protected BabySittingSession(SerializationInfo info, StreamingContext context)
        {
            this.startTime = (DateTime)info.GetValue("startTime", typeof(DateTime));
            this.endTime = (DateTime)info.GetValue("endTime", typeof(DateTime));
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("startTime", startTime);
            info.AddValue("endTime", endTime);
        }

        public void print()
        {
            System.Console.WriteLine($"startTime is {this.startTime}, endTime is {this.endTime}");
        }

        public void setStartTime(int hours, int minutes)
        {
            this.startTime = setTime(hours, minutes);
        }

        public void setEndTime(int hours, int minutes)
        {
            this.endTime = setTime(hours, minutes);
        }

        public DateTime setTime(int hours, int minutes)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, 0);
        }
    }
}
