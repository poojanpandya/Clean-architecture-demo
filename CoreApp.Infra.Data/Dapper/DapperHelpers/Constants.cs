using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.Dapper.DapperHelpers
{
    public class Constants
    {
        public static string LibraryConnection;
        public IUnitOfWork dapperContext => new UnitOfWork(LibraryConnection);
    }
}
