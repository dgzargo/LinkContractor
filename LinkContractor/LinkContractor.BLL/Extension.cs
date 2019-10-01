using System;
using LinkContractor.BLL.DTO;
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
            serviceCollection.AddTransient<BlMain>();
            return serviceCollection;
        }

        public static SavedDataDTO CreateSavedDataDTO(string message, bool isLink, int? timeLimit,
            int? clickLimit = null, Guid? user = null)
        {
            var a = new SavedDataDTO
            {
                Guid = Guid.NewGuid(),
                Message = message,
                IsLink = isLink,
                User = user,
                TimeLimit = timeLimit,
                ClickLimit = clickLimit
            };
            return a;
        }
    }
}