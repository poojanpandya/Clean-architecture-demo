using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Domain.Models
{
    public class Book
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
    }
}
