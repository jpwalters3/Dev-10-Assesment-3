using NUnit.Framework;
using SolarFarm.CORE;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolarFarm.BLL.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        RecordService target = new RecordService();

        [TestCase(2030)]
        [TestCase(0)]

        public void yearTest(int year)
        {
            Panel testPanel = new Panel("unittest", 1, 1, year, Materials.MULTI, true);
            Result<Panel> actual = target.AddPanel(testPanel);
            Assert.AreEqual(actual.Success, false);
        }

        [TestCase(1000)]
        [TestCase(-1)]

        public void BoundaryTest(int row)
        {
            Panel testPanel = new Panel("unittest", row, 1, 2020, Materials.MULTI, true);
            Result<Panel> actual = target.UpdatePanel("unittest", row, 1, testPanel);
            Assert.AreEqual(actual.Success, false);
        }

    }
}
