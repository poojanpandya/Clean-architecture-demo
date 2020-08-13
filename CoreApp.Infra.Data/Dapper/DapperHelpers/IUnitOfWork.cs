using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CoreApp.Infra.Data.Dapper.DapperHelpers
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        T Transaction<T>(Func<IDbTransaction, T> query);
    }
}
