using Models.Interfaces;
using Models.Models;
using Moq;
using NUnit.Framework;

namespace NUnitTests.Tests
{
    [TestFixture()]
    public class WebCalculatorTest
    {
        private Mock<IWebCollector> _mockWebCollector;
        private WebCalculator _calc;
        private string _keyword = "test";
        private string _htmlCode = "a lot of html code";

        [SetUp]
        public void Setup()
        {
            _calc = new WebCalculator();
            _mockWebCollector = new Mock<IWebCollector>();
            _mockWebCollector.GetType();
        }

        [Test()]
        public void CalculateNumberOfHits_TestSendNullObject()
        {
            Assert.AreEqual(-1, _calc.CalculateNumberOfHits(null, _keyword));
        }

        [Test]
        public void CalculateNumberOfHits_TestSendNullHtmlProperty()
        {
            _htmlCode = null;
            _mockWebCollector.SetupProperty(p => p.HTMLCode, _htmlCode);
            Assert.AreEqual(-1, _calc.CalculateNumberOfHits(_mockWebCollector.Object, _keyword));
        }

        [Test]
        public void CalculateNumberOfHits_TestSendEmptyHtmlProperty()
        {
            _htmlCode = string.Empty;
            _mockWebCollector.SetupProperty(p => p.HTMLCode, _htmlCode);
            Assert.AreEqual(-1, _calc.CalculateNumberOfHits(_mockWebCollector.Object, _keyword));
        }

        [Test]
        public void CalculateNumberOfHits_TestSendNullKeyword()
        {
            _keyword = null;
            _mockWebCollector.SetupProperty(p => p.HTMLCode, _htmlCode);
            Assert.AreEqual(-1, _calc.CalculateNumberOfHits(_mockWebCollector.Object, _keyword));
        }

        [Test]
        public void CalculateNumberOfHits_TestSendEmptyKeyword()
        {
            _keyword = string.Empty;
            _mockWebCollector.SetupProperty(p => p.HTMLCode, _htmlCode);
            Assert.AreEqual(-1, _calc.CalculateNumberOfHits(_mockWebCollector.Object, _keyword));
        }

        [Test]
        public void CalculateNumberOfHits_TestSendValidValues()
        {
            int expectedValue = 5;
            _htmlCode = "I@love@to@Code,COde,@£$€CoDE,[]}[€$£$cOde____$$€{€{€CODE";
            _keyword = "CODE";
            _mockWebCollector.SetupProperty(p => p.HTMLCode, _htmlCode);
            Assert.AreEqual(expectedValue, _calc.CalculateNumberOfHits(_mockWebCollector.Object, _keyword));
        }
    }

}
