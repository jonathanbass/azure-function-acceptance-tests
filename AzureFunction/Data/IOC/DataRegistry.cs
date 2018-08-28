using Data.Interfaces;
using Data.Models;
using Data.Queries;
using Data.QueryHandlers;

namespace Data.IOC
{
    public class DataRegistry : StructureMap.Registry
    {
        public DataRegistry()
        {
            For<IQueryHandler<MovieQuery, MovieResponse>>().Use<MovieQueryHandler>();
        }
    }
}
