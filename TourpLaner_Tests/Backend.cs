
using NUnit.Framework;
using TourPlaner;

namespace TourPLanerTests
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