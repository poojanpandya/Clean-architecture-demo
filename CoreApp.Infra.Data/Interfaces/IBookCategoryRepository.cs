using CoreApp.Domain.Models;
using CoreApp.Infra.Data.Dapper;
using CoreApp.Infra.Data.RepositoryAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.Interfaces
{
    public interface IBookCategoryRepository : IRepositoryAccess<BookCategory>
    {

    }
}
