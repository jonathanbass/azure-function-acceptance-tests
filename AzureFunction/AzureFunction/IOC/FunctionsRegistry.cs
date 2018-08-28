using Data.IOC;
using StructureMap;

namespace AzureFunction.IOC
{
    public class FunctionsRegistry : Registry
    {
        public FunctionsRegistry()
        {
            ScanRegistries();

            IncludeRegistry<DataRegistry>();
        }

        private void ScanRegistries()
        {
            Scan(scanner =>
            {
                scanner.Assembly("Data");
                scanner.SingleImplementationsOfInterface();
                scanner.LookForRegistries();
            });
        }
    }
}