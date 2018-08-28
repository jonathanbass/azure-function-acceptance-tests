using System;
using Microsoft.Azure.WebJobs.Description;

namespace AzureFunction.DependencyInjection
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
    }
}
