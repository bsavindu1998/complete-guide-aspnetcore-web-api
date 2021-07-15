using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        //add book
        [HttpPost("add-author")]
        public IActionResult AddBook([FromBody]AuthorVM author)
        {
            _authorsService.AddAuthor(author);
            return Ok(new { message = "Success" });
        }
        //get author with books
        [HttpGet("get-author-with-books/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var result = _authorsService.GetAuthorWithBooks(id);
            return Ok(result);
        }

    }
}
