using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CoreApp.Infra.Data.Dapper.DapperHelpers
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SqlConnection(_connectionString);
                if (string.IsNullOrWhiteSpace(_connection.ConnectionString))
                    _connection.ConnectionString = _connectionString;
                return _connection;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            if (_transaction?.Connection != null)
                return _transaction;

            _transaction = Connection.BeginTransaction();

            return _transaction;
        }

        public T Transaction<T>(Func<IDbTransaction, T> query)
        {
            using (Connection)
            {
                using (var transaction = BeginTransaction())
                {
                    try
                    {
                        var result = query(transaction);
                        Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Rollback();
                        throw new Exception("Error occured on Transaction => " + ex.Message.ToString(), ex);
                    }
                    finally
                    {
                        Dispose();
                    }

                }
            }
        }
        private void Commit()
        {
            try
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
            catch (Exception ex)
            {
                if (_transaction.Connection != null)
                    Rollback();

                throw new NullReferenceException("Tried Commit on closed Transaction", ex);
            }
        }
        private void Rollback()
        {
            try
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
            catch (Exception ex)
            {
                throw new NullReferenceException("Tried Rollback on closed Transaction", ex);
            }
        }
        public IDbConnection GetConnection()
        {
            return _connection;
        }
        private void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            if (_connection == null || _connection.State == ConnectionState.Closed)
                return;
            _connection.Close();
            _connection.Dispose();
            _connection = null;

            if (Connection == null || Connection.State == ConnectionState.Closed)
                return;
            Connection.Close();
            Connection.Dispose();
        }
    }
}
