using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BabySitterTimeTracker
{
    class Program
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
                        System.Console.Write("End Time in military time hours:");
                        var endingHours = Int32.Parse(System.Console.ReadLine());
                        System.Console.Write("End Time in military time minutes:");
                        var endingMinutes = Int32.Parse(System.Console.ReadLine());
                        Program.babySittingSession.setStartTime(endingHours, endingMinutes);
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
            //legit hours from 4-8 PM, or 14 thru 18
            int startingHours = 0;
            while (startingHours < 14 || startingHours > 18)
            {
                System.Console.Write("StartTime in military time hours (legit hours from 14 thru 18):");
                startingHours = Int32.Parse(System.Console.ReadLine());
            }
            
            System.Console.Write("StartTime in military time minutes:");
            var startingMinutes = Int32.Parse(System.Console.ReadLine());
            Program.babySittingSession.setStartTime(startingHours, startingMinutes);
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
