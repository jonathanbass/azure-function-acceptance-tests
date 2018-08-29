using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AzureFunction.IOC;
using Data.Interfaces;
using Data.Models;
using Data.Queries;
using FluentAssertions;
using Newtonsoft.Json;
using StructureMap;
using TechTalk.SpecFlow;

namespace AzureFunction.AcceptanceTests.Steps
{
    [Binding]
    public class GetMovieFunctionSteps
    {
        private static MovieQuery _movieQuery;
        private MovieResponse _movieReponse;
        private Container _container;

        [Given(@"a Get Movie Function exists")]
        public void GivenAGetMovieFunctionExists()
        {
            _movieQuery = new MovieQuery();
            _container = new Container(new FunctionsRegistry());
        }

        [When(@"I specify '(.*)' as the lead actor")]
        public void WhenISpecifyAsTheLeadActor(string leadActor)
        {
            _movieQuery.LeadActor = leadActor;
        }

        [When(@"I specify '(.*)' as the year")]
        public void WhenISpecifyAsTheYear(int year)
        {
            _movieQuery.Year = year;
        }

        [When(@"I execute the Get Movie function")]
        public async Task WhenIExecuteTheGetMovieFunction()
        {
            var request = GetRequest();
            var response = await GetMovieFunction.Run(request,
                _container.GetInstance<IQueryHandler<MovieQuery, MovieResponse>>());
            var responseContent = await response.Content.ReadAsStringAsync();
            _movieReponse = JsonConvert.DeserializeObject<MovieResponse>(responseContent);
        }

        [Then(@"the result should have lead actor '(.*)'")]
        public void ThenTheResultShouldHaveLeadActor(string leadActor)
        {
            _movieReponse.LeadActor.Should().Be(leadActor);
        }

        [Then(@"the result should have year '(.*)'")]
        public void ThenTheResultShouldHaveYear(int year)
        {
            _movieReponse.Year.Should().Be(year);
        }

        [Then(@"the result should have Title '(.*)'")]
        public void ThenTheResultShouldHaveTitle(string title)
        {
            _movieReponse.Title.Should().Be(title);
        }

        [Then(@"the result should have Genre '(.*)'")]
        public void ThenTheResultShouldHaveGenre(string genre)
        {
            _movieReponse.Genre.Should().Be(genre);
        }

        private static HttpRequestMessage GetRequest()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["leadactor"] = _movieQuery.LeadActor;
            query["year"] = _movieQuery.Year.ToString();

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
