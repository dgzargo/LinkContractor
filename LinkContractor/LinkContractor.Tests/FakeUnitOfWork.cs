using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinkContractor.DAL;
using LinkContractor.DAL.Entities;
using LinkContractor.DAL.Interfaces;
using LinkContractor.DAL.Repositories;

namespace LinkContractor.Tests
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public static DataContainer Data { get; }

        public FakeUnitOfWork()
        {
            SavedData = new FakeSavedDataRepository(Data);
            ShortCodes = new FakeShortCodeRepository(Data);
        }

        static FakeUnitOfWork()
        {
            Data = new DataContainer();
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        public ISavedDataRepository SavedData { get; }
        public IShortCodeRepository ShortCodes { get; }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }

    public class FakeSavedDataRepository : FakeRepository<SavedData>, ISavedDataRepository
    {
        private DataContainer Data { get; }
        private static Func<object, object, bool> KeysAreEqual { get; }
        private static Func<SavedData, object> GetKey { get; }

        public FakeSavedDataRepository(DataContainer dataContainerParam) : base(dataContainerParam, KeysAreEqual,
            GetKey)
        {
            Data = dataContainerParam;
        }

        static FakeSavedDataRepository()
        {
            KeysAreEqual = (o1, o2) =>
            {
                if (o1 is int i1 && o2 is int i2) return i1 == i2;
                return false;
            };
            GetKey = savedData => savedData.Id;
        }

        public SavedData GetSavedDataWithShortCode(Guid guidParam)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeShortCodeRepository : FakeRepository<ShortCode>, IShortCodeRepository
    {
        private DataContainer Data { get; }
        private static Func<object, object, bool> KeysAreEqual { get; }
        private static Func<ShortCode, object> GetKey { get; }

        public FakeShortCodeRepository(DataContainer dataContainerParam) : base(dataContainerParam, KeysAreEqual,
            GetKey)
        {
            Data = dataContainerParam;
        }

        static FakeShortCodeRepository()
        {
            KeysAreEqual = (o1, o2) =>
            {
                if (o1 is int i1 && o2 is int i2) return i1 == i2;
                return false;
            };
            GetKey = shortCode => shortCode.Code;
        }

        public SavedData GetCorrespondingSavedData(int code)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private Func<object, object, bool> KeysAreEqual { get; }
        private Func<TEntity, object> GetKey { get; }
        private List<TEntity> Entities { get; }

        public FakeRepository(DataContainer dataContainerParam, Func<object, object, bool> keysAreEqual,
            Func<TEntity, object> getKey)
        {
            KeysAreEqual = keysAreEqual;
            GetKey = getKey;
            Entities = dataContainerParam.GetList<TEntity>();
        }

        public TEntity Find(object key)
        {
            return Entities.FirstOrDefault(entity => KeysAreEqual(GetKey(entity), key));
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate.Compile());
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IEnumerable<TEntity> query = Entities;
            if (filter is {}) query = Entities.Where(filter.Compile());
            if (orderBy is {}) query = Entities.OrderBy((Func<TEntity, IOrderedQueryable<object>>) orderBy);
            return query;
        }

        public void Update(TEntity entity)
        {
            var key = GetKey(entity);
            for (var i = 0; i < Entities.Count; i++)
            {
                var e = Entities[i];
                if (KeysAreEqual(GetKey(e), key))
                {
                    Entities[i] = entity;
                    return;
                }
            }
        }

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public void Add(params TEntity[] entities)
        {
            Entities.AddRange(entities);
        }

        public void Remove(object key)
        {
            for (var i = 0; i < Entities.Count; i++)
            {
                var entityKey = GetKey(Entities[i]);
                if (KeysAreEqual(entityKey, key))
                {
                    Entities.RemoveAt(i);
                    return;
                }
            }
        }

        public void Remove(TEntity entity)
        {
            Remove(GetKey(entity));
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities) Remove(entity);
        }

        public void Remove(params TEntity[] entities)
        {
            Remove((IEnumerable<TEntity>) entities);
        }
    }

    public sealed class DataContainer
    {
        public DataContainer(IEnumerable<SavedData> savedDataList = null, IEnumerable<ShortCode> shortCodeList = null)
        {
            SavedDataList = savedDataList is null ? new List<SavedData>() : new List<SavedData>(savedDataList);
            ShortCodeList = shortCodeList is null ? new List<ShortCode>() : new List<ShortCode>(shortCodeList);
        }

        public List<SavedData> SavedDataList { get; }
        public List<ShortCode> ShortCodeList { get; }

        public DataContainer Copy()
        {
            return new DataContainer(SavedDataList, ShortCodeList);
        }

        public void Update(DataContainer newerVersion)
        {
            SavedDataList.Clear();
            ShortCodeList.Clear();

            SavedDataList.AddRange(newerVersion.SavedDataList);
            ShortCodeList.AddRange(newerVersion.ShortCodeList);
        }

        public List<TEntity> GetList<TEntity>()
        {
            var type = typeof(TEntity);
            if (type == typeof(SavedData)) return (List<TEntity>) (object) SavedDataList;
            if (type == typeof(ShortCode)) return (List<TEntity>) (object) ShortCodeList;
            return null;
        }
    }
}