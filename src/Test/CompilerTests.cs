namespace CER.Test.Text
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Text.RegularExpressions;
    using CER.Text;
    using System.Collections.Generic;

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
            compilerToTest.Regex[Tokens.Any_one_character] = ".";
            scan_results = compilerToTest.Scan(test).ToList();
            Assert.AreEqual(scan_results.Count, test.Length);

            test = "123 4354this213123is some cra8sy oar-77";
            compilerToTest = new Compiler();
            compilerToTest.Regex[Tokens.Number] = @"\d+";
            scan_results = compilerToTest.Scan(test).ToList();
            Assert.AreEqual(scan_results.Count, 5);
        }
        [TestMethod]
        public void CalcExp()
        {
            this.print(Compiler.CalculateExponents(0, 155));
            this.print(Compiler.CalculateExponents(1, 155));
            this.print(Compiler.CalculateExponents(123, 155));
            this.print(Compiler.CalculateExponents(154, 155));
            this.print(Compiler.CalculateExponents(155, 155));
            this.print(Compiler.CalculateExponents(156, 155));
            this.print(Compiler.CalculateExponents(3723875, 155));
            this.print(Compiler.CalculateExponents(3723874, 155));
            this.print(Compiler.CalculateExponents(3851372, 155));
            this.print(Compiler.CalculateExponents(3723874, 155));
        }
        [TestMethod]
        public void EnumerateOrdinals155()
        {
            this.print(Compiler.EnumerateOrdinals(33874));
        }

        private void print<T>(IEnumerable<T> a)
        {
            foreach(T i in a)
            {
                Console.Write(i + Environment.NewLine);
            }
            Console.WriteLine();
        }
    }
    public enum Tokens
    {
        Any_one_character,
        Number
    }
}

