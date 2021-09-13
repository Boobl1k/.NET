using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorTask;

namespace CalculatorTask.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Sum()
        {
            Assert.AreEqual(2 + 2, Calculator.Calculate(2, 2, Calculator.Operation.Plus));
            Assert.AreEqual(3 + 7, Calculator.Calculate(3, 7, Calculator.Operation.Plus));
            Assert.AreEqual(12947 + 12375123, Calculator.Calculate(12947, 12375123, Calculator.Operation.Plus));
        }

        [TestMethod]
        public void Difference()
        {
            Assert.AreEqual(2 - 2, Calculator.Calculate(2, 2, Calculator.Operation.Minus));
            Assert.AreEqual(235 - 634, Calculator.Calculate(235, 634, Calculator.Operation.Minus));
            Assert.AreEqual(183945 - 129385, Calculator.Calculate(183945, 129385, Calculator.Operation.Minus));
        }

        [TestMethod]
        public void Product()
        {
            Assert.AreEqual(4 * 231, Calculator.Calculate(4, 231, Calculator.Operation.Multiply));
            Assert.AreEqual(43465 * 23421, Calculator.Calculate(43465, 23421, Calculator.Operation.Multiply));
            Assert.AreEqual(12345 * 4623, Calculator.Calculate(12345, 4623, Calculator.Operation.Multiply));
        }

        [TestMethod]
        public void Quotient()
        {
            Assert.AreEqual(8394 / 165, Calculator.Calculate(8394, 165, Calculator.Operation.Divide));
            Assert.AreEqual(235216 / 13453, Calculator.Calculate(235216, 13453, Calculator.Operation.Divide));
            Assert.AreEqual(37659 / 35676613, Calculator.Calculate(37659, 35676613, Calculator.Operation.Divide));
        }
    }
}

