using System;
using System.Threading;
using StructureMap;

namespace AzureFunction.IOC
{
    public static class StructureMapContainerFactory
    {
        private static readonly Lazy<Container> ContainerBuilder = new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container => ContainerBuilder.Value;

        private static Container DefaultContainer()
        {
            return new Container(x =>
            {
                x.AddRegistry(new FunctionsRegistry());            
            });
        }        
    }
}