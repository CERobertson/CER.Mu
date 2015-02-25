namespace CER.Test.Text
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Text.RegularExpressions;
    using CER.Text;

    [TestClass]
    public class CompilerTests
    {
        [TestMethod]
        public void ScanTests()
        {
            var test = "this is a sentence";
            var compilerToTest = new Compiler();
            var scan_results = compilerToTest.Scan(test).ToList();
            Assert.AreEqual(scan_results.Count, 0);
            compilerToTest.TokenRegex["Any one character"] = ".";
            scan_results = compilerToTest.Scan(test).ToList();
            Assert.AreEqual(scan_results.Count, test.Length);

            test = "123 4354this213123is some cra8sy oar-77";
            compilerToTest = new Compiler();
            compilerToTest.TokenRegex["Number"] = @"\d+";
            scan_results = compilerToTest.Scan(test).ToList();
            Assert.AreEqual(scan_results.Count, 5);
        }
    }
}

