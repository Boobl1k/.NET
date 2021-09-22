using System;
using CalculatorTask;
using IlLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorIlTask.Tests
{
    [TestClass]
    public class CalculatorIlTests
    {
        [TestMethod]
        public void Sum()
        {
            Assert.AreEqual(2 + 2, CalculatorIl.Calculate(2, 2, CalculatorIl.Operation.Plus));
            Assert.AreEqual(3 + 7, CalculatorIl.Calculate(3, 7, CalculatorIl.Operation.Plus));
            Assert.AreEqual(12947 + 12375123, CalculatorIl.Calculate(12947, 12375123, CalculatorIl.Operation.Plus));
        }

        [TestMethod]
        public void Difference()
        {
            Assert.AreEqual(2 - 2, CalculatorIl.Calculate(2, 2, CalculatorIl.Operation.Minus));
            Assert.AreEqual(235 - 634, CalculatorIl.Calculate(235, 634, CalculatorIl.Operation.Minus));
            Assert.AreEqual(183945 - 129385, CalculatorIl.Calculate(183945, 129385, CalculatorIl.Operation.Minus));
        }

        [TestMethod]
        public void Product()
        {
            Assert.AreEqual(4 * 231, CalculatorIl.Calculate(4, 231, CalculatorIl.Operation.Multiply));
            Assert.AreEqual(43465 * 23421, CalculatorIl.Calculate(43465, 23421, CalculatorIl.Operation.Multiply));
            Assert.AreEqual(12345 * 4623, CalculatorIl.Calculate(12345, 4623, CalculatorIl.Operation.Multiply));
        }

        [TestMethod]
        public void Quotient()
        {
            try
            {
                CalculatorIl.Calculate(1, 0, CalculatorIl.Operation.Divide);
            }
            catch (Exception e)
            {
                Assert.AreEqual(Calculator.DevByZero.Message, e.Message);
            }

            Assert.AreEqual(8394 / 165, CalculatorIl.Calculate(8394, 165, CalculatorIl.Operation.Divide));
            Assert.AreEqual(235216 / 13453, CalculatorIl.Calculate(235216, 13453, CalculatorIl.Operation.Divide));
            Assert.AreEqual(37659 / 35676613, CalculatorIl.Calculate(37659, 35676613, CalculatorIl.Operation.Divide));
        }
        
        [TestMethod]
        public void UnassignedOperationCalculatorIl()
        {
            try
            {
                CalculatorIl.Calculate(1, 1, default);
            }
            catch (Exception e)
            {
                Assert.AreEqual(Calculator.WrongOperation.Message, e.Message);
            }

            try
            {
                CalculatorIl.Calculate(1, 1, (CalculatorIl.Operation) (10));
            }
            catch (Exception e)
            {
                Assert.AreEqual(Calculator.OutOfRange.Message, e.Message);
            }
        }

        [TestMethod]
        public void Main()
        {
            Assert.AreEqual(0, Program.Main(new [] {"1","+","1"}));
            Assert.AreEqual(1, Program.Main(new [] {""}));
            Assert.AreEqual(2, Program.Main(new [] {"r","+","t"}));
            Assert.AreEqual(3, Program.Main(new [] {"1","t","1"}));
        }

        [TestMethod]
        public void Parser()
        {
            Assert.AreEqual(false, ParserIl.TryParsOrQuit("a", out _));
            
            Assert.AreEqual(true, ParserIl.TryParsOrQuit("1", out var numParsResult));
            Assert.AreEqual(1, numParsResult);
            
            Assert.AreEqual(CalculatorIl.Operation.Divide, ParserIl.ParseCalculatorOperation("/"));
            Assert.AreEqual(CalculatorIl.Operation.Minus, ParserIl.ParseCalculatorOperation("-"));
            Assert.AreEqual(CalculatorIl.Operation.Multiply, ParserIl.ParseCalculatorOperation("*"));
        }
        
        
    }
}
