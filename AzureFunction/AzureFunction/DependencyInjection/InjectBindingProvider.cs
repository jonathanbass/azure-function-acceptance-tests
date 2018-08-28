using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using StructureMap;

namespace AzureFunction.DependencyInjection
{
    public class InjectBindingProvider : IBindingProvider
    {
        private readonly IContainer _structureMapContainer;        

        public InjectBindingProvider(IContainer structureMapContainer)
        {
            _structureMapContainer = structureMapContainer;            
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            IBinding binding = new InjectBinding(_structureMapContainer, context.Parameter.ParameterType);
            return Task.FromResult(binding);
        }
    }
}