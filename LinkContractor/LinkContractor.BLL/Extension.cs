using System;
using LinkContractor.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LinkContractor.BLL
{
    public static class Extension
    {
        public static IServiceCollection ConfigureBllDependencies(this IServiceCollection serviceCollection,
            Action<DbContextOptionsBuilder> optionsAction)
        {
            serviceCollection.ConfigureDalDependencies(optionsAction);
            return serviceCollection;
        }
    }
}