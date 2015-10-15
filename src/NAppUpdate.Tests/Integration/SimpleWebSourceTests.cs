using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAppUpdate.Framework.Sources;
using System.IO;

namespace NAppUpdate.Tests.Integration
{
    [TestClass]
    public class SimpleWebSourceTests
    {
        [TestMethod]
        public void can_download_ansi_feed()
        {
            const string expected = "NHibernate.Profiler-Build-";

            var ws = new SimpleWebSource("http://builds.hibernatingrhinos.com/latest/nhprof");

            var reader = new StreamReader(ws.GetUpdatesFeed());
            string str = reader.ReadToEnd();

            Assert.AreEqual(expected, str.Substring(0, expected.Length));
        }
    }
}
