using NUnit.Framework;
using Tourplaner_Data;

namespace Tourplaner_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Database_Connection_is_valid()
        {
           Database.SimpleQuery("Select * from Tour", "");
        }
    }
}