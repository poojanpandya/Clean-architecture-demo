using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Domain.Models
{
    public class BookCategory
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }

        public string Category { get; set; }
    }
}
