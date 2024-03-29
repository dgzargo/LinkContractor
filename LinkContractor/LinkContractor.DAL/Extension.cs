﻿using System;
using LinkContractor.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LinkContractor.DAL
{
    public static class Extension
    {
        public static IServiceCollection ConfigureDalDependencies(this IServiceCollection serviceCollection,
            Action<DbContextOptionsBuilder> optionsAction)
        {
            serviceCollection.AddDbContext<LinkContractorDbContext>(optionsAction);
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            return serviceCollection;
        }
    }
}