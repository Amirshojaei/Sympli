using System.Linq;
using Xunit;
using SearchEngineCrawlerServices.Contract;
using SearchEngineCrawlerServices.Service;
using SearchEngineCrawlerServices.Dto;
using Moq;
using System.Threading.Tasks;

namespace SearchEngineCrawlerServices.Tests
{
    public class SearchServiceTest
    {
        [Fact]
        public async Task SearchURLInGoogleResult_WithKeywordAndUrl_ReturnsIndexOfOcurrencesAsync()
        {
            //Arrange
            var searchEngineMock = new Mock<ISearchEngine>();
            searchEngineMock.Setup(x => x.SearchResultItemPattern).Returns(@"\<div class=""BNeawe UPmit AP7Wnd"">.+?\<\/div>");
            searchEngineMock.Setup(x => x.Url).Returns(@"https://www.google.com/search");
            searchEngineMock.Setup(x => x.GetQueryString("flower")).Returns($"num=100&q=flower");

            var httpWebRequestServiceMock = new Mock<IHttpWebRequestService>();
            httpWebRequestServiceMock
                .Setup(x => x.GetPageAsStringAsync(searchEngineMock.Object.Url, searchEngineMock.Object.GetQueryString("flower")))
                .ReturnsAsync(new ServiceResult<string>(""));

            var cacheServiceMock = new Mock<ICacheService>();

            SearchService searchService = new SearchService(httpWebRequestServiceMock.Object, cacheServiceMock.Object);

            //Act
            var serviceResult = await searchService.SearchAsync(searchEngineMock.Object, "flower", "Www.somthing.com");

            //Assert

            Assert.True(serviceResult.Success);
            Assert.NotNull(serviceResult.Result);
        }

        [Fact]
        public async Task SearchURLInGoogleResult_NoKeyword_ReturnsErrorMessage()
        {
            //Arrange
            var searchEngineMock = new Mock<ISearchEngine>();
            searchEngineMock.Setup(x => x.SearchResultItemPattern).Returns(@"\<div class=""BNeawe UPmit AP7Wnd"">.+?\<\/div>");
            searchEngineMock.Setup(x => x.Url).Returns(@"https://www.google.com/search");
            searchEngineMock.Setup(x => x.GetQueryString("")).Returns($"num=100&q=flower");

            var httpWebRequestServiceMock = new Mock<IHttpWebRequestService>();
            httpWebRequestServiceMock
                .Setup(x => x.GetPageAsStringAsync(searchEngineMock.Object.Url, searchEngineMock.Object.GetQueryString("")))
                .ReturnsAsync(new ServiceResult<string>(""));

            var cacheServiceMock = new Mock<ICacheService>();

            SearchService searchService = new SearchService(httpWebRequestServiceMock.Object, cacheServiceMock.Object);
            //Act
            var serviceResult = await searchService.SearchAsync(searchEngineMock.Object, "", "Www.somthing.com");

            //Assert

            Assert.False(serviceResult.Success);
            Assert.True(serviceResult.ErrorMessages.FirstOrDefault() == "keyword cannot be empty.");
        }

        [Fact]
        public async Task SearchURLInGoogleResult_NoUrl_ReturnsErrorMessage()
        {
            //Arrange
            var searchEngineMock = new Mock<ISearchEngine>();
            searchEngineMock.Setup(x => x.SearchResultItemPattern).Returns(@"\<div class=""BNeawe UPmit AP7Wnd"">.+?\<\/div>");
            searchEngineMock.Setup(x => x.Url).Returns(@"https://www.google.com/search");
            searchEngineMock.Setup(x => x.GetQueryString("flower")).Returns($"num=100&q=flower");

            var httpWebRequestServiceMock = new Mock<IHttpWebRequestService>();
            httpWebRequestServiceMock
                .Setup(x => x.GetPageAsStringAsync(searchEngineMock.Object.Url, searchEngineMock.Object.GetQueryString("flower")))
                .ReturnsAsync(new ServiceResult<string>(""));

            var cacheServiceMock = new Mock<ICacheService>();

            SearchService searchService = new SearchService(httpWebRequestServiceMock.Object, cacheServiceMock.Object);
            //Act
            var serviceResult = await searchService.SearchAsync(searchEngineMock.Object, "flower", "");

            //Assert

            Assert.False(serviceResult.Success);
            Assert.True(serviceResult.ErrorMessages.FirstOrDefault() == "URL cannot be empty.");
        }

        [Fact]
        public async Task SearchURLInGoogleResult_WithWrongUrl_ReturnsErrorMessage()
        {
            //Arrange
            var searchEngineMock = new Mock<ISearchEngine>();
            searchEngineMock.Setup(x => x.SearchResultItemPattern).Returns(@"\<div class=""BNeawe UPmit AP7Wnd"">.+?\<\/div>");
            searchEngineMock.Setup(x => x.Url).Returns(@"https://www.google.com/search");
            searchEngineMock.Setup(x => x.GetQueryString("flower")).Returns($"num=100&q=flower");

            var httpWebRequestServiceMock = new Mock<IHttpWebRequestService>();
            httpWebRequestServiceMock
                .Setup(x => x.GetPageAsStringAsync(searchEngineMock.Object.Url, searchEngineMock.Object.GetQueryString("flower")))
                .ReturnsAsync(new ServiceResult<string>(""));

            var cacheServiceMock = new Mock<ICacheService>();

            SearchService searchService = new SearchService(httpWebRequestServiceMock.Object, cacheServiceMock.Object);

            //Act
            var serviceResult = await searchService.SearchAsync(searchEngineMock.Object, "flower", "mysite//.com");

            //Assert

            Assert.False(serviceResult.Success);
            Assert.True(serviceResult.ErrorMessages.FirstOrDefault() == "URL is not valid.");
        }
    }
}
