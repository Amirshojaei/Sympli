using System.Linq;
using Xunit;
using SearchEngineCrawlerServices.Service;
using System.Threading.Tasks;

namespace SearchEngineCrawlerServices.Tests
{
    public class HttpWebRequestTests
    {

        [Fact]
        public async Task GetPageAsString_EmptyPageUrl_ReturnsError()
        {
            //arrang
            HttpWebRequestService httpWerbRequestService = new HttpWebRequestService();

            //act
            var serviceResult = await httpWerbRequestService.GetPageAsStringAsync("");

            //assert
            Assert.False(serviceResult.Success);
            Assert.True(serviceResult.ErrorMessages.FirstOrDefault() == "Page URL cannot be empty.");
        }

        [Fact]
        public async Task GetPageAsString_InvlidPageUrl_ReturnsError()
        {
            //arrang
            HttpWebRequestService httpWerbRequestService = new HttpWebRequestService();

            //act
            var serviceResult = await httpWerbRequestService.GetPageAsStringAsync("some wrong page url");

            //assert
            Assert.False(serviceResult.Success);
            Assert.True(serviceResult.ErrorMessages.FirstOrDefault() == "Page URL is not valid.");
        }

    }
}
