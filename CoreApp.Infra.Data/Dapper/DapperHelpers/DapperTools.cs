using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CoreApp.Infra.Data.Dapper.DapperHelpers
{
    public class DapperTools : IDapperTools
    {
        private readonly IUnitOfWork _context;

        public DapperTools(string connectionString)
        {
            _context = new UnitOfWork(connectionString);
        }

        public DapperTools(IUnitOfWork context)
        {
            _context = context;
        }

        public T Get<T>(object ID) where T : class
        {
            return _context.Connection.Get<T>(ID);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Connection.GetAll<T>();
        }

        public long Insert<T>(T obj) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Insert(obj, transaction, 0);
                return result;
            });
        }

        public long Insert<T>(IEnumerable<T> list) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Insert(list, transaction);
                return result;
            });
        }

        public bool Update<T>(T obj) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Update(obj, transaction);
                return result;
            });
        }

        public bool Update<T>(IEnumerable<T> list) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Update(list, transaction);
                return result;
            });
        }

        public bool Delete<T>(T obj) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Delete(obj, transaction);
                return result;
            });
        }

        public bool Delete<T>(IEnumerable<T> list) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Delete(list, transaction);
                return result;
            });
        }

        public bool DeleteAll<T>() where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.DeleteAll<T>();
                return result;
            });
        }

        public void Execute(string sql, object param = null)
        {
            _context.Transaction(transaction =>
              _context.Connection.Execute(sql, param, transaction)
           );
        }

        public object ExecuteScalar(string sql, object param = null)
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.ExecuteScalar(sql, param, transaction);
                return result;
            });
        }
        public IEnumerable<T> Query<T>(string sql, object param = null) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Query<T>(sql, param, transaction);
                return result;
            });
        }
        public int StoredProcedureQuery<T>(string StoreProcedure, DynamicParameters param) where T : class
        {
            return _context.Transaction(transaction =>
            {
                var result = _context.Connection.Execute(StoreProcedure, param, transaction, commandType: CommandType.StoredProcedure);
                return result;
            });
        }
    }
}
