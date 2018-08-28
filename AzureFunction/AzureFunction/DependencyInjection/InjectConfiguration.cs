using AzureFunction.IOC;
using Microsoft.Azure.WebJobs.Host.Config;

namespace AzureFunction.DependencyInjection
{
    public class InjectConfiguration : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var container = StructureMapContainerFactory.Container;

            context
                .AddBindingRule<InjectAttribute>()
                .Bind(new InjectBindingProvider(container));
        }        
    }
}
