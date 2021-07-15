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
    public class PublisherController : ControllerBase
    {
        private PublisherService _publisherService;

        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            _publisherService.AddPublisher(publisher);
            return Ok(new { message = "Success" });
        }
        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _result = _publisherService.GetPublisherData(id);
            return Ok(_result);
        }
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
           var _result = _publisherService.DeletePublisherById(id);
            if (_result)
            {
                return Ok(new { message = "Success" });
            }
            else
            {
                return Ok(new { message = "Failed" });
            }

        }
    }
}
