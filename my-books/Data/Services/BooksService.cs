using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }
        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : default,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverURL = book.CoverURL,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var authorID in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = authorID
                };
                _context.Book_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }
        public List<Book> GetAllBooks()
        {
            var allBooks = _context.Books.ToList();
            return allBooks;
        }
        public BookWithAuthorsVM GetBookById(int bookID) {
            var _booksWithAuthors = _context.Books.Where(n => n.Id == bookID).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : default,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverURL = book.CoverURL,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();
            return _booksWithAuthors;
        }
        public Book UpdateBookById(int bookID, BookVM bookVM) {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookID);
            if (_book != null) {
                _book.Title = bookVM.Title;
                _book.Description = bookVM.Description;
                _book.IsRead = bookVM.IsRead;
                _book.DateRead = bookVM.DateRead;
                _book.Rate = bookVM.Rate;
                _book.Genre = bookVM.Genre;
                _book.CoverURL = bookVM.CoverURL;
                _context.SaveChanges();
            }
            return _book;
        }
        public void DeleteBookById(int bookID) {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookID);
            if (_book != null) {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
