using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunction.DependencyInjection;
using Data.Interfaces;
using Data.Models;
using Data.Queries;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace AzureFunction
{
    public static class GetMovieFunction
    {
        private static HttpRequestMessage _request;

        [FunctionName("GetMovieFunction")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage request, 
            [Inject]IQueryHandler<MovieQuery, MovieResponse> movieQueryHandler)
        {
            _request = request;

            var leadActor = GetQueryStringValue("leadactor");

            var year = Convert.ToInt32(GetQueryStringValue("year"));

            var response = await movieQueryHandler.HandleAsync(new MovieQuery {Year = year, LeadActor = leadActor});

            return leadActor == null || year == 0
                ? request.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : request.CreateResponse(HttpStatusCode.OK, response);
        }

        private static string GetQueryStringValue(string parameterName)
        {
            return _request.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, parameterName, StringComparison.OrdinalIgnoreCase) == 0)
                .Value;
        }
    }
}
