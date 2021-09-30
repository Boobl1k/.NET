using System;
using CalculatorTask;
using FSLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorFsTask.Tests
{
    [TestClass]
    public class CalculatorFsTests
    {
        [TestMethod]
        public void Sum()
        {
            Assert.AreEqual(2 + 2, CalculatorFs.Calculate(2, 2, CalculatorFs.Operation.Plus));
            Assert.AreEqual(3 + 7, CalculatorFs.Calculate(3, 7, CalculatorFs.Operation.Plus));
            Assert.AreEqual(12947 + 12375123, CalculatorFs.Calculate(12947, 12375123, CalculatorFs.Operation.Plus));
        }

        [TestMethod]
        public void Difference()
        {
            Assert.AreEqual(2 - 2, CalculatorFs.Calculate(2, 2, CalculatorFs.Operation.Minus));
            Assert.AreEqual(235 - 634, CalculatorFs.Calculate(235, 634, CalculatorFs.Operation.Minus));
            Assert.AreEqual(183945 - 129385, CalculatorFs.Calculate(183945, 129385, CalculatorFs.Operation.Minus));
        }

        [TestMethod]
        public void Product()
        {
            Assert.AreEqual(4 * 231, CalculatorFs.Calculate(4, 231, CalculatorFs.Operation.Multiply));
            Assert.AreEqual(43465 * 23421, CalculatorFs.Calculate(43465, 23421, CalculatorFs.Operation.Multiply));
            Assert.AreEqual(12345 * 4623, CalculatorFs.Calculate(12345, 4623, CalculatorFs.Operation.Multiply));
        }

        [TestMethod]
        public void Quotient()
        {
            try
            {
                CalculatorFs.Calculate(1, 0, CalculatorFs.Operation.Divide);
            }
            catch (Exception e)
            {
                Assert.AreEqual(Calculator.DevByZero.Message, e.Message);
            }

            Assert.AreEqual(8394 / 165, CalculatorFs.Calculate(8394, 165, CalculatorFs.Operation.Divide));
            Assert.AreEqual(235216 / 13453, CalculatorFs.Calculate(235216, 13453, CalculatorFs.Operation.Divide));
            Assert.AreEqual(37659 / 35676613, CalculatorFs.Calculate(37659, 35676613, CalculatorFs.Operation.Divide));
        }

        [TestMethod]
        public void UnassignedOperationCalculatorFs()
        {
            try
            {
                CalculatorFs.Calculate(1, 1, CalculatorFs.Operation.Unassigned);
            }
            catch (Exception e)
            {
                Assert.AreEqual(Calculator.WrongOperation.Message, e.Message);
            }

            //unusable for f#s enum analog
            /*try
            {
                CalculatorFs.Calculate(1, 1, (CalculatorFs.Operation) (10));
            }
            catch (Exception e)
            {
                Assert.AreEqual(Calculator.OutOfRange.Message, e.Message);
            }*/
        }

        [TestMethod]
        public void Main1() => Assert.AreEqual(0, Program.Main(new[] {"1", "+", "1"}));
        [TestMethod]
        public void Main2() => Assert.AreEqual(1, Program.Main(new[] {""}));
        [TestMethod]
        public void Main3() => Assert.AreEqual(2, Program.Main(new[] {"r", "+", "t"}));
        [TestMethod]
        public void Main4() => Assert.AreEqual(3, Program.Main(new[] {"1", "t", "1"}));

        [TestMethod]
        public void Parser()
        {
            Assert.AreEqual(false, ParserFs.TryParsOrQuit("a", out _));

            Assert.AreEqual(true, ParserFs.TryParsOrQuit("1", out var numParsResult));
            Assert.AreEqual(1, numParsResult);

            Assert.AreEqual(CalculatorFs.Operation.Divide, ParserFs.ParseCalculatorOperation("/"));
            Assert.AreEqual(CalculatorFs.Operation.Minus, ParserFs.ParseCalculatorOperation("-"));
            Assert.AreEqual(CalculatorFs.Operation.Multiply, ParserFs.ParseCalculatorOperation("*"));
        }
    }
}