using CoreApp.Application.Interfaces;
using CoreApp.Application.ViewModels;
using CoreApp.Domain.Models;
using CoreApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Application.Services
{
    public class BookService : IBookService
    {
        public IBookRepository _bookRepository;
        public ApiResponseModel response = new ApiResponseModel();
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book GetByID(int ID)
        {
            return _bookRepository.Get(ID);
        }

        public ApiResponseModel Add(Book b)
        {
            long res =  _bookRepository.Insert(b);
            if (res < 0)
            {
                response.Status = false;
                response.Message = "Something Went Wrong!";
                return response;
            }
            response.Status = true;
            response.Message = "Added SuccessFully!";
            return response;
        }
        public ApiResponseModel Update(Book b)
        {
            var res = _bookRepository.UpdateBooks(b);
            if (res == false)
            {
                response.Status = false;
                response.Message = "Something Went Wrong!";
                return response;
            }
            response.Status = true;
            response.Message = "Updated SuccessFully!";
            return response;
        }
        public ApiResponseModel Delete(Book b)
        {
            var res = _bookRepository.Delete(b);
            if (res == false)
            {
                response.Status = false;
                response.Message = "Something Went Wrong!";
                return response;
            }
            response.Status = true;
            response.Message = "Deleted SuccessFully!";
            return response;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _bookRepository.GetAll();
        }
        public Book GetByName(string name)
        {
            return _bookRepository.GetByName(name);
        }
    }
}
