using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Funda.Core.Test
{
    public class TopEstateAgentsServiceTests
    {
        private ITopEstateAgentsService topEstateAgentsService;

        [SetUp]
        public void Setup()
        {
           
            topEstateAgentsService = new TopEstateAgentsService();
        }

        [Test]
        public void GetUriStringTest()
        {           
            var expectedResult = @"http://partnerapi.funda.nl/feeds/Aanbod.svc/JSON/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin/&page={0}&pagesize=25";
            var result = topEstateAgentsService.GetUriString(true);
            Assert.AreEqual(expectedResult, result);

            expectedResult = @"http://partnerapi.funda.nl/feeds/Aanbod.svc/JSON/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/&page={0}&pagesize=25";
            result = topEstateAgentsService.GetUriString(false);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetEstateAgentElementsTest()
        {
            var input = new List<EstateElement>();
            input.Add(new EstateElement(Guid.NewGuid(), 12345, "Anna"));
            input.Add(new EstateElement(Guid.NewGuid(), 12345, "Anna Sheremet"));
            input.Add(new EstateElement(Guid.NewGuid(), 7777, "Mickey"));
            input.Add(new EstateElement(Guid.NewGuid(), 333, "Donald Duck"));

            var expected = new List<EstateAgentElement>();
            expected.Add(new EstateAgentElement(12345, "Anna", 2));
            expected.Add(new EstateAgentElement(7777, "Mickey", 1));
            expected.Add(new EstateAgentElement(333, "Donald Duck", 1));

            var result =  topEstateAgentsService.GetEstateAgentElements(input);
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public void GetOrderedEstateAgentElementsTest()
        {
            var input = new List<EstateAgentElement>();
            input.Add(new EstateAgentElement(1, "Anna", 20));
            input.Add(new EstateAgentElement(2, "Mickey", 10));
            input.Add(new EstateAgentElement(3, "Donald Duck", 7));
            input.Add(new EstateAgentElement(4, "Andrew", 8));
            input.Add(new EstateAgentElement(5, "Tessa", 30));
            input.Add(new EstateAgentElement(6, "Mia", 1));
            input.Add(new EstateAgentElement(7, "Arnold", 5));

            var expected = new List<EstateAgentElement>();
            expected.Add(new EstateAgentElement(5, "Tessa", 30));
            expected.Add(new EstateAgentElement(1, "Anna", 20));
            expected.Add(new EstateAgentElement(2, "Mickey", 10));
            expected.Add(new EstateAgentElement(4, "Andrew", 8));
            expected.Add(new EstateAgentElement(3, "Donald Duck", 7));
            expected.Add(new EstateAgentElement(7, "Arnold", 5));
            expected.Add(new EstateAgentElement(6, "Mia", 1));

            var result = topEstateAgentsService.GetOrderedEstateAgentElements(input);

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public void GetTopEstateAgentElementsTest()
        {
            var input = new List<EstateAgentElement>();
            input.Add(new EstateAgentElement(5, "Tessa", 30));
            input.Add(new EstateAgentElement(1, "Anna", 20));
            input.Add(new EstateAgentElement(2, "Mickey", 10));
            input.Add(new EstateAgentElement(4, "Andrew", 8));
            input.Add(new EstateAgentElement(3, "Donald Duck", 7));
            input.Add(new EstateAgentElement(7, "Arnold", 5));
            input.Add(new EstateAgentElement(6, "Mia", 1));

            var expected = new List<EstateAgentElement>();
            expected.Add(new EstateAgentElement(5, "Tessa", 30));
            expected.Add(new EstateAgentElement(1, "Anna", 20));
            expected.Add(new EstateAgentElement(2, "Mickey", 10));
            expected.Add(new EstateAgentElement(4, "Andrew", 8));

            var result = topEstateAgentsService.GetTopEstateAgentElements(input, 4);
            Assert.IsTrue(expected.SequenceEqual(result));


            expected = new List<EstateAgentElement>();
            expected.Add(new EstateAgentElement(5, "Tessa", 30));
            expected.Add(new EstateAgentElement(1, "Anna", 20));
            expected.Add(new EstateAgentElement(2, "Mickey", 10));
            expected.Add(new EstateAgentElement(4, "Andrew", 8));
            expected.Add(new EstateAgentElement(3, "Donald Duck", 7));
            expected.Add(new EstateAgentElement(7, "Arnold", 5));
            expected.Add(new EstateAgentElement(6, "Mia", 1));

            result = topEstateAgentsService.GetTopEstateAgentElements(input, 20);
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [Test]
        public async Task GetEstateElementsAsyncTast()
        {
            var resultWithGarden = await topEstateAgentsService.GetTopTenEstateAgentElements(true);
            Assert.IsTrue(resultWithGarden.Any());
        }
    }
}