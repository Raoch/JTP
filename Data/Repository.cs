using Dapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Data.Interfaces;
using Newtonsoft.Json;
using JTPBlog.Models;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly string tableName;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            tableName = typeof(T).Name;
        }

        public virtual T Get(string searchField, object value)
        {
            var sql = string.Format("SELECT * FROM {0} WHERE {1} = @searchValue", tableName, searchField);
            object parameters = new { searchValue = value };
            return unitOfWork.DbConnection.Query<T>(sql, param: parameters, transaction: unitOfWork.DbTransaction).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                var sql = string.Format("SELECT * FROM {0}", tableName);
                var result = unitOfWork.DbConnection.Query<T>(sql, transaction: unitOfWork.DbTransaction);
                return result;
            }
            catch(Exception e)
            {
                var error = e;
                return new List<T>();
            }
        }

        public virtual IEnumerable<T> ExecuteSql(string sql)
        {
            return unitOfWork.DbConnection.Query<T>(sql, transaction: unitOfWork.DbTransaction);
        }

        public virtual IEnumerable<T> GetAll<T1, T2>(Func<T, T1, T2, T> includeFunc = null, IEnumerable<DapperInclude> includes = null, string splitOn = null) where T1 : class where T2 : class
        {
            string innerJoinStr = string.Empty;
            var query = string.Format("SELECT * FROM {0}", tableName);
            if (includes != null)
            {
                foreach (var include in includes)
                    innerJoinStr += string.Format("INNER JOIN {1} ON {1}.{2} = {3}.{2} ", query, include.TableName, include.ID, tableName);
                query = string.Format("{0} {1}", query, innerJoinStr);
            }
            return unitOfWork.DbConnection.Query(query, includeFunc, splitOn: splitOn);
        }

        public virtual int Insert<T1>(T entity)
        {
            string valueNames = "";
            string values = "";

            PropertyInfo[] properties = typeof(T1).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttribute(typeof(DBIgnore)) != null)
                    continue;

                valueNames = string.Format("{0}{1},", valueNames, property.Name);
                values = string.Format("{0}@{1},", values, property.Name);
            }
            var query = string.Format("INSERT INTO {0} ({1}) VALUES ({2}); SELECT LAST_INSERT_ID();", typeof(T1).Name, valueNames.Trim(','), values.Trim(','));
            var newBlogID = unitOfWork.DbConnection.ExecuteScalar<int>(query, param: entity, transaction: unitOfWork.DbTransaction);
            unitOfWork.Commit();
            return newBlogID;
        }

        public int Count(string whereClause, object parameters)
        {
            return unitOfWork.DbConnection.ExecuteScalar<int>(string.Format("Select COUNT(*) from {0} WHERE {1}", tableName, whereClause), parameters, unitOfWork.DbTransaction);
        }

        //public virtual IEnumerable<T> GetPaginatedList(Func<T, T1, T2, T> includeFunc = null, IEnumerable<DapperInclude> includes = null, string splitOn = null) where T1 : class where T2 : class
        //{
        //    string innerJoinStr = string.Empty;
        //    var query = string.Format("SELECT * FROM {0}", tableName);
        //    if (includes != null)
        //    {
        //        foreach (var include in includes)
        //            innerJoinStr += string.Format("INNER JOIN {1} ON {1}.{2} = {3}.{2} ", query, include.TableName, include.ID, tableName);
        //        query = string.Format("{0} {1}", query, innerJoinStr);
        //    }
        //    return unitOfWork.DbConnection.Query(query, includeFunc, splitOn: splitOn);
        //}
    }
}
