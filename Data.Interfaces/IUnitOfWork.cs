using System;
using System.Data;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection DbConnection { get; }
        IDbTransaction DbTransaction { get; }
        void Commit();
        void Dispose();
    }
}