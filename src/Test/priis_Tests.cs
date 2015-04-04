namespace CER.Test
{
    using CER.JudeaPearl;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Computations of the examples contained in Judea Pearl's book, 
    /// "Probabilistic Reasoning in Intelligent Systems: Networks of Plausible Inference" 
    /// 1988 by Morgan Kaufmann Publishers, Inc.
    /// </summary>
    [TestClass]
    public class priis_Tests
    {
        [TestMethod]
        public void Chapter2_Example1()
        {
            var pAlarm_Burglary = 0.95M;
            var pAlarm_NoBurglary = 0.01M;
            var pBurglary = 0.0001M;

            var lAlarm_Burglary = pAlarm_Burglary.LikelihoodRatio(pAlarm_NoBurglary);
            var oBurglary = pBurglary.PriorOdds();
            var oBurglary_Alarm = lAlarm_Burglary.PosteriorOdds(oBurglary);
            var pBurglary_Alarm = oBurglary_Alarm.Probablity();

            Assert.AreEqual(0.0095M, oBurglary_Alarm);
            Assert.AreEqual(0.00941M, pBurglary_Alarm);
        }
    }
}
