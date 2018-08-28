using System;
using System.Threading.Tasks;
using Data.Interfaces;
using Data.Models;
using Data.Queries;

namespace Data.QueryHandlers
{
    internal class MovieQueryHandler : IQueryHandler<MovieQuery, MovieResponse>
    {
        public async Task<MovieResponse> HandleAsync(MovieQuery query)
        {
            // NB: We could add any database call here that we wanted
            // the data returned is hard-coded as this still illustrates
            // the correct method for Unit and Acceptance testing

            var response = new MovieResponse
            {
                Year = query.Year,
                LeadActor = query.LeadActor,
                Title = "The Last Jedi",
                Genre = "Comedy"
            };

            Func<Task<MovieResponse>> action = async () => await Task.FromResult(response);
            return await action.Invoke();
        }
    }
}
