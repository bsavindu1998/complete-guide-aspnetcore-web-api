using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublisherService
    {
        private AppDbContext _context;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }
        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(books => new BookAuthorVM()
                    {
                        BookName = books.Title,
                        BookAuthors = books.Book_Authors.Select(ba => ba.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _publisherData;
        }
        public bool DeletePublisherById(int id)
        {
            var _publisherData = _context.Publishers.FirstOrDefault(p => p.Id == id);
            if (_publisherData != null)
            {
                _context.Publishers.Remove(_publisherData);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
