using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplaner_Utility;

namespace Tourplaner_Tests
{
    [TestFixture]
    public class Util
    {
        [Test]
        public void TestConfigloader()
        {
            string result = null;
            result = CFGManager.ReadSetting("Username");
            Assert.AreEqual("a", result, "Some useful error message");
        }
    }
}
