using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApp.Application.Entities;
using CoreApp.Application.Interfaces;
using CoreApp.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAppDemo.MVC.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService bookService;
        private IMapper mapper;
        public BookController(IBookService bookService, IMapper mapper)
        {
            this.bookService = bookService;
            this.mapper = mapper;
        }

        [Route("api/getBooks")]
        public IActionResult getBooks()
        {
            var authResponse = bookService.GetBooks();
            return Ok(authResponse);
        }

        [HttpPost("api/AddBooks")]
        public IActionResult AddBook(BookDTO b)
        {
            var book = mapper.Map<Book>(b);
            var response = bookService.Add(book);
            return Ok(response);
        }

        [HttpPut("api/UpdateBook")]
        public IActionResult UpdateBook(BookDTO b)
        {
            var book = mapper.Map<Book>(b);
            var modifiedBook = bookService.Update(book);
            return Ok(modifiedBook);
        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int Id)
        {
            if (ModelState.IsValid)
            {
                var deletedBook = bookService.GetByID(Id);
                if (deletedBook != null)
                {
                    var response = bookService.Delete(deletedBook);
                    return Ok(response);
                }
                else
                    return NotFound();
            }
            return BadRequest();
        }
    }
}