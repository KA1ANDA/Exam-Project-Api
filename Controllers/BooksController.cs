using ExamProjectApi.Models;
using ExamProjectApi.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamProjectApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        IPKG_BOOKS pkg_books;

        public BooksController(IPKG_BOOKS pkg_books)
        {
            this.pkg_books = pkg_books;
        }

        [HttpPost]

        public IActionResult add_book(Book book)
        {
            this.pkg_books.add_book(book);
            return Ok();
        }



        [HttpGet]

        public IActionResult get_books()
        {
            List<Book> books = new List<Book>();
            books = this.pkg_books.get_books();

            return Ok(books);
        }


        [HttpPost]

        public IActionResult delete_book(Book book)
        {
            this.pkg_books.delete_book(book);
            return Ok();
        }



        [HttpPut]

        public IActionResult update_book(Book book)
        {
            this.pkg_books.update_book(book);
            return Ok();
        }




    }
}
