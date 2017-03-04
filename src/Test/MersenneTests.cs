using CER.Wikipedia;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CER.Test {
    /// <summary>
    /// Summary description for MersenneTests
    /// </summary>
    [TestClass]
    public class MersenneTests {
        [TestMethod]
        public void ExploreNumbers() {
            int primes_found = 0;
            int length = 8;
            int[][] merseen_primes = new int[length][];
            for (int i = 3; i < length; i++) {
                int M = Mersenne.Number(i);
                if (Mersenne.Lucas_LehmerPrimalityTest(i, M)) {
                    merseen_primes[primes_found] = new[] { i, M };
                    primes_found++;
                }
            }
        }
    }
}
