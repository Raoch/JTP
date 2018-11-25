using System;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(string searchField, object value);
        IEnumerable<T> GetAll<T1, T2>(
            Func<T, T1, T2, T> includeFunc, 
            IEnumerable<DapperInclude> includes = null, 
            string splitOn = null) where T1 : class where T2 : class;
        IEnumerable<T> GetAll();
        IEnumerable<T> ExecuteSql(string sql);
        int Count(string whereClause, object parameters);
        int Insert<T1>(T entity);
        //IEnumerable<T> GetPaginatedList();
    }
}
