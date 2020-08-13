using AutoMapper;
using CoreApp.Application.Entities;
using CoreApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAppDemo.MVC.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<BookDTO, Book>();
        }
    }
}
