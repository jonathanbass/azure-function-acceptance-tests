using System.Threading.Tasks;
using AutoFixture;
using Data.Models;
using Data.Queries;
using Data.QueryHandlers;
using FluentAssertions;
using NUnit.Framework;

namespace Data.UnitTests.QueryHandlers
{
    [TestFixture]
    public class GivenAMovieQueryHandler
    {
        private MovieResponse _movieResponse;
        private MovieQuery _movieQuery;

        [SetUp]
        public async Task SetUp()
        {
            var fixture = new Fixture();
            _movieQuery = fixture.Create<MovieQuery>();
            var movieQueryHandler = new MovieQueryHandler();
            _movieResponse = await movieQueryHandler.HandleAsync(_movieQuery);
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectYear()
        {
            _movieResponse.Year.Should().Be(_movieQuery.Year);
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectLeadActor()
        {
            _movieResponse.LeadActor.Should().Be(_movieQuery.LeadActor);
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectTitle()
        {
            _movieResponse.Title.Should().Be("The Last Jedi");
        }

        [Test]
        public void TheTheResponseObjectContainsTheCorrectGenre()
        {
            _movieResponse.Genre.Should().Be("Comedy");
        }
    }
}
