namespace CER.Test
{
    using CER.JudeaPearl;
    using CER.Test.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

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

            (0.0095M).IsEqualTo(oBurglary_Alarm, 4);
            (0.00941M).IsEqualTo(pBurglary_Alarm, 5);
        }

        [TestMethod]
        public void Chaprter2_Example2and3()
        {
            var detector_sensitivity = new decimal[][]
            {
                new decimal[] {0.5M, 0.4M, 0.1M},
                new decimal[] {0.06M, 0.5M, 0.44M},
                new decimal[] {0.5M, 0.1M, 0.4M},
                new decimal[] {1.0M, 0.0M, 0.0M},
            };
            var d1HighSound = detector_sensitivity.LikelihoodVector(2);
            var d2NoSound = detector_sensitivity.LikelihoodVector(0);
            var lVector = d1HighSound.VectorProduct(d2NoSound);

            (0.05M).IsEqualTo(lVector[0], 2);
            (0.0264M).IsEqualTo(lVector[1], 4);
            (0.2M).IsEqualTo(lVector[2], 1);
            (0.0M).IsEqualTo(lVector[3]);

            var prior_pHypothesis = new decimal[] { 0.099M, 0.009M, 0.001M, 0.891M };
            var rpHypothesis_evidence = prior_pHypothesis.VectorProduct(lVector);

            (0.00495M).IsEqualTo(rpHypothesis_evidence[0],5);
            (0.000238M).IsEqualTo(rpHypothesis_evidence[1],6);
            (0.0002M).IsEqualTo(rpHypothesis_evidence[2],4);
            (0.0M).IsEqualTo(rpHypothesis_evidence[3]);

            var pHypothesis = rpHypothesis_evidence.Normalize();
            pHypothesis.SumsToUnity();

            //fails due to slight difference from book. This seems like
            //an artifact from the decimal datatype I have used. I will
            //watch for more example before deciding to change types.
            (0.919M).IsEqualTo(pHypothesis[0],3);
            (0.0439M).IsEqualTo(pHypothesis[1],4);
            (0.0375M).IsEqualTo(pHypothesis[2],4);
            (0.0M).IsEqualTo(pHypothesis[3]);
        }
    }
}
