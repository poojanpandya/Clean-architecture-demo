using CoreApp.Infra.Data.Dapper.DapperHelpers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.Dapper
{
    public class RepositoryAccess<TEntity> : IRepositoryAccess<TEntity> where TEntity : class
    {
        IDapperTools dapper;

        public RepositoryAccess()
        {

            dapper = new DapperTools(Constants.LibraryConnection);
        }

        public TEntity Get(object ID)
        {
            return dapper.Get<TEntity>(ID);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dapper.GetAll<TEntity>();
        }

        public long Insert(TEntity entity)
        {
            return dapper.Insert<TEntity>(entity);
        }

        public bool Update(TEntity entity)
        {
            return dapper.Update<TEntity>(entity);
        }
        public bool Delete(TEntity entity)
        {
            return dapper.Delete<TEntity>(entity);
        }
        public int ExecuteStoredProcedure(string StoreProcedure, DynamicParameters param)
        {
            return dapper.StoredProcedureQuery<TEntity>(StoreProcedure, param);
        }
    }
}
