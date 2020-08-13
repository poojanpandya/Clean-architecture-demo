using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.Dapper
{
    public interface IRepositoryAccess<TEntity> where TEntity : class
    {
        TEntity Get(object ID);
        IEnumerable<TEntity> GetAll();
        long Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
