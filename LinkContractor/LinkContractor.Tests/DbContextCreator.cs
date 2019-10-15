using System;
using System.Collections.Generic;
using EntityFrameworkCoreMock;
using LinkContractor.DAL;
using LinkContractor.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkContractor.Tests
{
    public static class DbContextCreator
    {
        public static LinkContractorDbContext CreateWith(IEnumerable<SavedData> savedDataParam,
            IEnumerable<ShortCode> shortCodeParam)
        {
            var Options = new DbContextOptionsBuilder().Options;
            var dbContextMock = new DbContextMock<LinkContractorDbContext>(Options);
            var savedDataDbSetMock = dbContextMock.CreateDbSetMock(x => x.SavedData, savedDataParam);
            var shortCodesDbSetMock = dbContextMock.CreateDbSetMock(x => x.ShortCodes, shortCodeParam);
            return dbContextMock.Object;
        }
    }
}