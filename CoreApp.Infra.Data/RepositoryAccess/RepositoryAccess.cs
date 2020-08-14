using CoreApp.Infra.Data.Context;
using CoreApp.Infra.Data.Dapper.DapperHelpers;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreApp.Infra.Data.RepositoryAccess
{
    public class RepositoryAccess<TEntity> : IRepositoryAccess<TEntity> where TEntity : class
    {
        IDapperTools dapper;
        private LibraryDbContext _context;

        public RepositoryAccess(LibraryDbContext context)
        {
            _context = context;
        }

        public RepositoryAccess()
        {
            dapper = new DapperTools(Constants.LibraryConnection);
        }

        // Dapper
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

        // Entity Framework
        public IEnumerable<TEntity> EnGetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public TEntity EnGetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void EnInsert(TEntity obj)
        {
             _context.Set<TEntity>().AddAsync(obj);
            _context.SaveChanges();
        }

        public void EnUpdate(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            _context.SaveChanges();
        }

        public void EnDelete(object id)
        {
            var entity = EnGetById(id);
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void EnSave()
        {
            _context.SaveChanges();
        }
    }
}
