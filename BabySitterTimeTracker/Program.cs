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
                Console.WriteLine("1.) List Current Session\n2.) Enter Start Time\n3.) Enter Bed Time\n4.) Enter End Time\n5.)Amount Owed\n6.) Quit\n7.) Help");
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
                        SetBedTime();
                        break;
                    case (4):
                        SetEndTime();
                        break;
                    case (5):
                        PrintAmountOwed();
                        break;
                    case (6):
                        Quit();
                        keepGoing = false;
                        break;
                    default:
                        System.Console.WriteLine("An example of a valid time would be any of the following: 5, 6, 7, 8, 9, 10, 11, 12, 1, 2, 3, 4");
                        System.Console.WriteLine("The program will remember your last response, so you quit after entering each time - start, bed, and end.");
                        System.Console.WriteLine("Once all three times (start, bed, and ending) are entered, the parents will be able to calculate your fee. You can also calculate your fee by selecting option 5.");
                        System.Console.WriteLine("Please select a valid response. Try again.");
                        break;
                }
            }
        }

        private static void SetStartTime()
        {
            var startingHours = SetTime("Starting Time Hour");
            Program.babySittingSession.setStartTime(startingHours);
        }

        private static void SetBedTime()
        {
            var bedTimeHour = SetTime("Bed Time Hour");
            Program.babySittingSession.setBedTime(bedTimeHour);
        }

        private static void SetEndTime()
        {
            var endingTime = SetTime("End Time Hour");
            Program.babySittingSession.setEndTime(endingTime);
        }

        private static int SetTime(string timeTypeDescription)
        {
            int timeHour = -1;  //just make this greater than 4 AM
            while ((timeHour < 1))
            {
                System.Console.Write($"Enter {timeTypeDescription} time from 5 (PM) to 4 (AM) (don't worry about PM/AM and round to nearest hour - we trust you!):");
                timeHour = Int32.Parse(System.Console.ReadLine());
            }

            return timeHour;
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
            try
            {
                ICalculator calculator = new BabysittingCalculator();
                var cost = calculator.Calculate(Program.babySittingSession);
                System.Console.WriteLine("The cost of today's services are: " + cost);
            }
            catch(Exception e)
            {
                System.Console.WriteLine("Error with input format. Correct inputs. Details are: " + e.Message);
            }
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
