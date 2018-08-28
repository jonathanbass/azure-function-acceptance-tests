using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IQueryHandler<in TQuery, TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}
