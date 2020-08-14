using CoreApp.Domain.Models;
using CoreApp.Infra.Data.Context;
using CoreApp.Infra.Data.Interfaces;
using CoreApp.Infra.Data.RepositoryAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.Repositories
{
    public class BookCategoryRepository : RepositoryAccess<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(LibraryDbContext dbContext)
        : base(dbContext)
        {

        }
    }
}
