using System;
using FSLibraryResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarterConsole;

namespace FSResultTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void DivBy0()
        {
            var res = CalculatorFs.Calculate(2, 0, CalculatorFs.Operation.Divide);
            Assert.IsTrue(res.IsError);
            Assert.AreEqual(CalculatorFs.DevByZero, res.ErrorValue);
        }

        [TestMethod]
        public void CalculateInts()
        {
            for (var i = 0; i < 20; ++i)
            for (var j = 1; j < 21; ++j)
            {
                var plusRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Plus);
                var minusRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Minus);
                var multiplicationRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Multiply);
                var divRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Divide);

                Assert.IsTrue(plusRes.IsOk);
                Assert.IsTrue(minusRes.IsOk);
                Assert.IsTrue(multiplicationRes.IsOk);
                Assert.IsTrue(divRes.IsOk);

                Assert.AreEqual(i + j, plusRes.ResultValue);
                Assert.AreEqual(i - j, minusRes.ResultValue);
                Assert.AreEqual(i * j, multiplicationRes.ResultValue);
                Assert.AreEqual(i / j, divRes.ResultValue);
            }
        }

        [TestMethod]
        public void CalculateDoubles()
        {
            for (var i = 0.5; i < 20; ++i)
            for (var j = 1.5; j < 21; ++j)
            {
                var plusRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Plus);
                var minusRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Minus);
                var multiplicationRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Multiply);
                var divRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Divide);

                Assert.IsTrue(plusRes.IsOk);
                Assert.IsTrue(minusRes.IsOk);
                Assert.IsTrue(multiplicationRes.IsOk);
                Assert.IsTrue(divRes.IsOk);

                Assert.AreEqual(i + j, plusRes.ResultValue);
                Assert.AreEqual(i - j, minusRes.ResultValue);
                Assert.AreEqual(i * j, multiplicationRes.ResultValue);
                Assert.AreEqual(i / j, divRes.ResultValue);
            }
        }

        [TestMethod]
        public void CalculateDecimals()
        {
            for (var i = 0.5m; i < 20; ++i)
            for (var j = 1.5m; j < 21; ++j)
            {
                var plusRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Plus);
                var minusRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Minus);
                var multiplicationRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Multiply);
                var divRes =
                    CalculatorFs.Calculate(i, j, CalculatorFs.Operation.Divide);

                Assert.IsTrue(plusRes.IsOk);
                Assert.IsTrue(minusRes.IsOk);
                Assert.IsTrue(multiplicationRes.IsOk);
                Assert.IsTrue(divRes.IsOk);

                Assert.AreEqual(i + j, plusRes.ResultValue);
                Assert.AreEqual(i - j, minusRes.ResultValue);
                Assert.AreEqual(i * j, multiplicationRes.ResultValue);
                Assert.AreEqual(i / j, divRes.ResultValue);
            }
        }
    }

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void WrongOperation()
        {
            var res = ParserFs.ParseCalculatorOperation("ff");
            Assert.IsTrue(res.IsError);
            Assert.AreEqual(ParserFs.WrongOperation, res.ErrorValue);
        }

        [TestMethod]
        public void WrongNumber()
        {
            var res = ParserFs.ParseInt("a");
            Assert.IsTrue(res.IsError);
            Assert.AreEqual(ParserFs.numberError, res.ErrorValue);
        }

        [TestMethod]
        public void AllTypes()
        {
            var res1 = ParserFs.ParseInt("1");
            Assert.IsTrue(res1.IsOk);
            Assert.AreEqual(1, res1.ResultValue);
            var res2 = ParserFs.ParseDouble("1");
            Assert.IsTrue(res2.IsOk);
            Assert.AreEqual(1.0, res2.ResultValue);
            var res3 = ParserFs.ParseDecimal("1");
            Assert.IsTrue(res3.IsOk);
            Assert.AreEqual(1m, res3.ResultValue);
            var res4 = ParserFs.ParseFloat("1");
            Assert.IsTrue(res4.IsOk);
            Assert.AreEqual(1.0f, res4.ResultValue);
        }

        [TestMethod]
        public void Parse()
        {
            var resPlus = ParserFs.ParseCalculatorOperation("+");
            var resMinus = ParserFs.ParseCalculatorOperation("-");
            var resMultiplication = ParserFs.ParseCalculatorOperation("*");
            var resDiv = ParserFs.ParseCalculatorOperation("/");

            Assert.IsTrue(resPlus.IsOk);
            Assert.IsTrue(resMinus.IsOk);
            Assert.IsTrue(resMultiplication.IsOk);
            Assert.IsTrue(resDiv.IsOk);

            Assert.AreEqual(CalculatorFs.Operation.Plus, resPlus.ResultValue);
            Assert.AreEqual(CalculatorFs.Operation.Minus, resMinus.ResultValue);
            Assert.AreEqual(CalculatorFs.Operation.Multiply, resMultiplication.ResultValue);
            Assert.AreEqual(CalculatorFs.Operation.Divide, resDiv.ResultValue);
        }
    }

    [TestClass]
    public class Main
    {
        [TestMethod]
        public void GoodInput() =>
            Assert.AreEqual(0, Program.Main(new[] {"1", "+", "2"}));

        [TestMethod]
        public void NotEnoughArgs()
        {
            try
            {
                Program.Main(new[] {"1"});
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(Program.NotEnoughArgs, e);
            }
        }

        [TestMethod]
        public void NumberErrors()
        {
            try
            {
                Program.Main(new[] {"e", "-", "1"});
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(ParserFs.numberError, e.Message);
            }

            try
            {
                Program.Main(new[] {"3", "-", "t"});
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(ParserFs.numberError, e.Message);
            }
        }

        [TestMethod]
        public void OperationError()
        {
            try
            {
                Program.Main(new[] {"1", "r", "1"});
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(ParserFs.WrongOperation, e.Message);
            }
        }

        [TestMethod]
        public void CalculationError()
        {
            try
            {
                Program.Main(new[] {"1", "/", "0"});
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(CalculatorFs.DevByZero, e.Message);
            }
        }
    }
}