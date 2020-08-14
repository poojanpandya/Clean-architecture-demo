
using CoreApp.Domain.Models;
using CoreApp.Infra.Data.Context;
using CoreApp.Infra.Data.Dapper;
using CoreApp.Infra.Data.Dapper.DapperHelpers;
using CoreApp.Infra.Data.Interfaces;
using CoreApp.Infra.Data.RepositoryAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CoreApp.Infra.Data.Repositories
{
    public class BookRepository : RepositoryAccess<Book>, IBookRepository
    {
        IDapperTools dapperTools;

        public BookRepository()
        {
            dapperTools = new DapperTools(Constants.LibraryConnection);
        }

        public Book GetByName(string name)
        {
            return dapperTools.Query<Book>(@"Select * from Books where Name=@name", new { name }).First();
        }

        public bool UpdateBooks(Book b)
        {
            string procName = "spUpdateBook";
            var param = new DynamicParameters();
            param.Add("@Id", b.Id);
            param.Add("@Name", b.Name);
            param.Add("@ISBN", b.ISBN);
            param.Add("@AuthorName", b.AuthorName);
            var rowsAffected = ExecuteStoredProcedure(procName, param: param);
            if (rowsAffected < 0)
            {
                return false;
            }
            return true;
        }
     
    }
}
