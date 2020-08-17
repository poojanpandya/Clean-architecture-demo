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
        public IBookCategoryRepository _bookcategoryRepository;
        public ApiResponseModel response = new ApiResponseModel();
        public BookService(IBookRepository bookRepository,IBookCategoryRepository bookCategoryRepository)
        {
            _bookRepository = bookRepository;
            _bookcategoryRepository = bookCategoryRepository;
        }

        public Book GetByID(int ID)
        {
            try
            {
                return _bookRepository.Get(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApiResponseModel Add(Book b)
        {
            try
            {
                long res = _bookRepository.Insert(b);
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
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
        public ApiResponseModel Update(Book b)
        {
            try
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
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }
        public ApiResponseModel Delete(Book b)
        {
            try
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
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public IEnumerable<Book> GetBooks()
        {
            try
            {
                return _bookRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Book GetByName(string name)
        {
            try
            {
                return _bookRepository.GetByName(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApiResponseModel AddCategory(BookCategory b)
        {
            try
            {
                _bookcategoryRepository.EnInsert(b);
                response.Status = true;
                response.Message = "Added SuccessFully!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public IEnumerable<BookCategory> GetBookCategory()
        {
            try
            {
                return _bookcategoryRepository.EnGetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookCategory GetBookCategorybyId(object categoryId)
        {
            try
            {
                return _bookcategoryRepository.EnGetById(categoryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApiResponseModel UpdateCategory(BookCategory b)
        {
            try
            {
                _bookcategoryRepository.EnUpdate(b);
                response.Status = true;
                response.Message = "Updated SuccessFully!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public ApiResponseModel DeleteCategory(int Id)
        {
            try
            {
                _bookcategoryRepository.EnDelete(Id);
                response.Status = true;
                response.Message = "Deleted SuccessFully!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }



    }
}
