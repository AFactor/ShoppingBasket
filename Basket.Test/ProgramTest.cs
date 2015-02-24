using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Basket.Entities;
using Basket.Repositories;
using Basket.Service;
using Moq;
using System.Collections.Generic;
using Basket;
namespace Basket.Test
{
    [TestClass]
    public class ProgramTest
    {


        [TestInitialize]
        public void TestInitialize()
        {

        }
        [TestMethod]
        public void Check_Console_output_Number_Of_Times()
        {
            var writer = new Mock<IOutputWriter>();
            Program.WriteConsoleOutput(new string[] { "Milk,2 |Butter,4" }, writer.Object,
                MockDataSetup.SetMockPrices().Object, MockDataSetup.SetMockPromotions().Object);
            writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Exactly(2));

        }
        [TestMethod]
        public void Check_Console_output_Number_Of_Times_When_NoArgs()
        {
            var writer = new Mock<IOutputWriter>();
            Program.WriteConsoleOutput(new string[] {}, writer.Object,
                MockDataSetup.SetMockPrices().Object, MockDataSetup.SetMockPromotions().Object);
            writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Exactly(1));
            writer.Verify(w=>w.ReadLine(),Times.Exactly(1));

        }

    }
}