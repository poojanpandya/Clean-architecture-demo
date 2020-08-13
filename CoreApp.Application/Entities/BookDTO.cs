using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Application.Entities
{
   public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
    }
}
