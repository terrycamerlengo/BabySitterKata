using BabySitterTimeTracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BabySitterTimeTrackerUnitTests
{
    [TestClass]
    public class CalculationTests
    {
        [TestMethod]
        public void HappyPathTest()
        {
            BabySittingSession bss = new BabySittingSession();
            bss.setStartTime(5); //5 * 12 = 60
            bss.setBedTime(10); //2 * 8 = 16
            bss.setEndTime(4); //4 * 16 = 64

            ICalculator calculator = new BabysittingCalculator();
            decimal result = calculator.Calculate(bss);

            Assert.AreEqual(140, result);
        }

        [TestMethod]
        public void BedTimeAfterMidnightTest()
        {
            BabySittingSession bss = new BabySittingSession();
            bss.setStartTime(5); //8 * 12 = 96
            bss.setBedTime(1); //0
            bss.setEndTime(4); //4 * 16 = 64

            ICalculator calculator = new BabysittingCalculator();
            decimal result = calculator.Calculate(bss);

            Assert.AreEqual(160, result);
        }

        [TestMethod]
        public void EndTimeBeforeMidnightTest()
        {
            BabySittingSession bss = new BabySittingSession();
            bss.setStartTime(5); //7 * 12 = 84
            bss.setBedTime(1); //0
            bss.setEndTime(4); //4 * 16 = 64

            ICalculator calculator = new BabysittingCalculator();
            decimal result = calculator.Calculate(bss);

            Assert.AreEqual(148, result);
        }

        [TestMethod]
        public void CrazyTimesTest()
        {
            try
            {
                BabySittingSession bss = new BabySittingSession();
                bss.setStartTime(8); //7 * 12 = 84
                bss.setBedTime(5); //0
                bss.setEndTime(5); //4 * 16 = 64

                ICalculator calculator = new BabysittingCalculator();
                decimal result = calculator.Calculate(bss);

                Assert.Fail("no exception thrown");
            }
            catch (System.Exception ex)
            {
                Assert.IsTrue(ex is System.FormatException);
            }
        }
    }
}
