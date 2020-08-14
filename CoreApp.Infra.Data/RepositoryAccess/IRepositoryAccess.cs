using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.RepositoryAccess
{
    public interface IRepositoryAccess<TEntity> where TEntity : class
    {
        //For Dapper
        TEntity Get(object ID);
        IEnumerable<TEntity> GetAll();
        long Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        //For EntityFramework
        IEnumerable<TEntity> EnGetAll();
        TEntity EnGetById(object id);
        void EnInsert(TEntity obj);
        void EnUpdate(TEntity obj);
        void EnDelete(object id);
        void EnSave();
    }
}
