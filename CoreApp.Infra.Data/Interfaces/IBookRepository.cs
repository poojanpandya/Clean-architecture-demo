using CoreApp.Domain.Models;
using CoreApp.Infra.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.Interfaces
{
    public interface IBookRepository : IRepositoryAccess<Book>
    {
        Book GetByName(string name);
        bool UpdateBooks(Book b);
    }
}
