using Data.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection DbConnection { get; private set; }
        public IDbTransaction DbTransaction { get; private set; }
        private bool disposed;

        public UnitOfWork(string connectionString)
        {
            DbConnection = new MySqlConnection(connectionString);
            DbConnection.Open();
            DbTransaction = DbConnection.BeginTransaction();
        }

        public virtual void Commit()
        {
            try
            {
                DbTransaction.Commit();
            }
            catch (Exception ex)
            {
                DbTransaction.Rollback();
            }
            finally
            {
                DbTransaction.Dispose();
                DbTransaction = DbConnection.BeginTransaction();
            }
        }

        public virtual void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (DbTransaction != null)
                    {
                        DbTransaction.Dispose();
                        DbTransaction = null;
                    }
                    if (DbConnection != null)
                    {
                        DbConnection.Dispose();
                        DbConnection = null;
                    }
                }
                disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}