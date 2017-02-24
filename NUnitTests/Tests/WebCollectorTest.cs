using Models.Models;
using NUnit.Framework;

namespace NUnitTests.Tests
{
    [TestFixture()]
    public class WebCollectorTest
    {
        private WebCollector _coll;
        private string _url;

        [SetUp]
        public void Setup()
        {
            _coll = new WebCollector();
            _url = @"http://www.aftonbladet.se";
        }

        [Test()]
        public void GetHTMLFromUrl_TestSendValidUrl()
        {
            _coll.GetHTMLFromUrl(_url);
            Assert.IsNotNullOrEmpty(_coll.HTMLCode);
        }

        [Test()]
        public void GetHTMLFromUrl_TestSendNull()
        {
            _url = null;
            _coll.GetHTMLFromUrl(_url);
            Assert.IsEmpty(_coll.HTMLCode);
        }

        [Test()]
        public void GetHTMLFromUrl_TestSendEmptyString()
        {
            _url = string.Empty;
            _coll.GetHTMLFromUrl(_url);
            Assert.IsEmpty(_coll.HTMLCode);
        }

        [Test()]
        public void GetHTMLFromUrl_TestSendUrlWithoutHttp()
        {
            _url = "www.aftonbladet.se";
            _coll.GetHTMLFromUrl(_url);
            Assert.IsEmpty(_coll.HTMLCode);
        }
    }
}
