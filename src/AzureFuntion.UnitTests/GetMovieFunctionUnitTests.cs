using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoFixture;
using Data.Interfaces;
using Data.Models;
using Data.Queries;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace AzureFunction.UnitTests
{
    [TestFixture]
    public class GivenAGetMovieFunction
    {
        private HttpResponseMessage _httpResponseMessage;
        private Mock<IQueryHandler<MovieQuery, MovieResponse>> _mockMovieQueryHandler;
        private MovieResponse _responseObject;
        private MovieResponse _movieResponse;

        [SetUp]
        public async Task WhenInvoked()
        {
            var fixture = new Fixture();
            var movieQuery = fixture.Create<MovieQuery>();

            var request = GetRequest(movieQuery);

            _movieResponse = fixture.Build<MovieResponse>()
                .With(mr => mr.LeadActor, movieQuery.LeadActor)
                .With(mr => mr.Year, movieQuery.Year).Create();

            _mockMovieQueryHandler = new Mock<IQueryHandler<MovieQuery, MovieResponse>>();
            _mockMovieQueryHandler.Setup(mqh => mqh.HandleAsync(It.Is<MovieQuery>(mq => 
                mq.LeadActor == movieQuery.LeadActor &&
                mq.Year == movieQuery.Year))).ReturnsAsync(_movieResponse);

            _httpResponseMessage = await GetMovieFunction.Run(request, _mockMovieQueryHandler.Object);
            var textContent = await _httpResponseMessage.Content.ReadAsStringAsync();
            _responseObject = JsonConvert.DeserializeObject<MovieResponse>(textContent);
        }

        [Test]
        public void ThenTheMovieQueryHandlerIsInvoked()
        {
            _mockMovieQueryHandler.Verify(mqh => mqh.HandleAsync(It.Is<MovieQuery>(mq => 
                mq.LeadActor == _movieResponse.LeadActor &&
                mq.Year == _movieResponse.Year)), Times.Once);
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectLeadActor()
        {
            _responseObject.LeadActor.Should().Be(_movieResponse.LeadActor);
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectYear()
        {
            _responseObject.Year.Should().Be(_movieResponse.Year);
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectTitle()
        {
            _responseObject.Title.Should().Be(_movieResponse.Title);
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectGenre()
        {
            _responseObject.Genre.Should().Be(_movieResponse.Genre);
        }

        private static HttpRequestMessage GetRequest(MovieQuery movieQuery)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["leadactor"] = movieQuery.LeadActor;
            query["year"] = movieQuery.Year.ToString();

            var queryString = query.ToString();
            var request = new HttpRequestMessage {
                RequestUri = new Uri($"http://localhost/api/shoppingcart?{queryString}")
            };

            var configuration = new HttpConfiguration();
            request.SetConfiguration(configuration);

            return request;
        }
    }
}
