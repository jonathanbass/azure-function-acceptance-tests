using System;
using System.Net.Http;
using System.Web;
using AutoFixture;
using Data.Interfaces;
using Data.Models;
using Data.Queries;
using Moq;
using NUnit.Framework;

namespace AzureFunction.UnitTests
{
    [TestFixture]
    public class GivenAGetMovieFunction
    {
        private HttpResponseMessage _httpResponseMessage;
        private Mock<IQueryHandler<MovieQuery, MovieResponse>> _mockMovieQueryHandler;
        private MovieQuery _movieQuery;

        [SetUp]
        public void WhenInvoked()
        {
            var fixture = new Fixture();
            _movieQuery = fixture.Create<MovieQuery>();

            //construct content to send
            //var query = HttpUtility.ParseQueryString(string.Empty);
            //query["leadactor"] = Guid.NewGuid().ToString();
            //query["bar"] = Guid.NewGuid().ToString();
            string queryString = String.Empty;
            var request = new HttpRequestMessage {
                RequestUri = new Uri($"http://localhost/api/shoppingcart&{queryString}")
            };

            var movieResponse = fixture.Create<MovieResponse>();
            _mockMovieQueryHandler = new Mock<IQueryHandler<MovieQuery, MovieResponse>>();
            _mockMovieQueryHandler.Setup(mqh => mqh.HandleAsync(_movieQuery)).ReturnsAsync(movieResponse);

            _httpResponseMessage = GetMovieFunction.Run(request, _mockMovieQueryHandler.Object).Result;
        }

        [Test]
        public void ThenTheMovieQueryHandlerIsInvoked()
        {
            _mockMovieQueryHandler.Verify(mqh => mqh.HandleAsync(_movieQuery), Times.Once);
        }
    }
}
