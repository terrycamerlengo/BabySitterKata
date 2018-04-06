using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BabySitterTimeTracker
{
    /*
     ·    starts no earlier than 5:00PM
            leaves no later than 4:00AM
            gets paid $12/hour from start-time to bedtime
            gets paid $8/hour from bedtime to midnight
            gets paid $16/hour from midnight to end of job
            gets paid for full hours (no fractional hours)
    */
    public static class Program
    {
        private static BabySittingSession babySittingSession;
        static void Main(string[] args)
        {
            loadBabySittingSession();
            bool keepGoing = true;

            while (keepGoing)
            {
                Console.WriteLine("1.) List Current Session\n2.) Enter Start Time\n3.) Enter End Time\n4.) Amount Owed\n5.) Quit");
                var command = Console.ReadLine();
                switch (Int32.Parse(command))
                {
                    case (1):
                        Program.babySittingSession.print();
                        break;
                    case (2):
                        SetStartTime();
                        break;
                    case (3):
                        SetEndTime();
                        break;
                    case (4):
                        PrintAmountOwed();
                        break;
                    case (5):
                        Quit();
                        keepGoing = false;
                        break;
                    default:
                        System.Console.Write("Please select a valid response. Try again.");
                        break;
                }
            }
        }

        private static void SetStartTime()
        {
            //legit hours from 5-10 PM, or 17 thru 22
            int startingHours = 0;
            while (startingHours < 17 || startingHours > 22)
            {
                System.Console.Write("Enter Start Time from 5 to 4 (round to nearest hour - we trust you):");
                startingHours = Int32.Parse(System.Console.ReadLine());
            }
                       
            Program.babySittingSession.setStartTime(startingHours);
        }

        private static void SetEndTime()
        {
            int endingHours = 5;  //just make this greater than 4 AM
            while ((endingHours > 4))
            {
                System.Console.Write("Enter end time in military hours (legit hours anything 4 or less):");
                endingHours = Int32.Parse(System.Console.ReadLine());
            }

            System.Console.Write("End Time in military time hours:");
            endingHours = Int32.Parse(System.Console.ReadLine());
            System.Console.Write("End Time in military time minutes:");
            var endingMinutes = System.Console.ReadLine();

            Program.babySittingSession.setStartTime(endingHours);
        }

        private static void loadBabySittingSession()
        {
            //Clear mp for further usage.
            Program.babySittingSession = null;

            if (File.Exists("BabySitSession.obj"))
            {
                //Open the file written above and read values from it.
                var stream = File.Open("BabySitSession.obj", FileMode.Open);
                var bformatter = new BinaryFormatter();

                Console.WriteLine("Reading Baby Sitting Information");
                Program.babySittingSession = (BabySittingSession)bformatter.Deserialize(stream);
                stream.Close();

                Program.babySittingSession.print();
            }
            else
            {
                Program.babySittingSession = new BabySittingSession();
            }
        }

        static void PrintAmountOwed()
        {

        }

        static void Quit()
        {
            Stream stream = File.Open("BabySitSession.obj", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();

            Console.WriteLine("Writing Babysitter Times");
            bformatter.Serialize(stream, Program.babySittingSession);
            stream.Close();
        }
    }
}
