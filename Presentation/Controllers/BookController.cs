using AutoMapper;
using Business.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;
    private readonly ILogger<BookController> _logger;
    
    public BookController(IBookService bookService, IMapper mapper, ILogger<BookController> logger)
    {
        _bookService = bookService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Route("GetBooks")]
    public IActionResult GetBooks()
    {
        var books = _bookService.GetBooks();
        _logger.Log(LogLevel.Information, "GetBooks called, returning {0} books", books.Count);
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetBook(Guid id)
    {
        var book = _bookService.GetBook(id);
        if (book == null)
        {
            _logger.Log(LogLevel.Information, "GetBook called with id {0}, but no book exists with that id", id);
            return NotFound();
        }
        
        _logger.Log(LogLevel.Information, "GetBook called with id {0}, returning book", id);
        return Ok(book);
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] Book book)
    {
        Book newBook;

        try
        {
            newBook = _bookService.CreateBook(book);
        }
        catch (Exception e)
        {
            var message = "CreateBook called, but an exception was thrown";
            _logger.Log(LogLevel.Error, message + ": {0}", e.Message);
            return StatusCode(500, message);
        }
       
        _logger.Log(LogLevel.Information, "CreateBook called, returning new book");
        return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(Guid id)
    {
        if (!_bookService.BookExists(id))
        {
            _logger.Log(LogLevel.Information, "DeleteBook called with id {0}, but no book exists with that id", id);
            return NotFound();
        }

        _bookService.DeleteBook(id);
        _logger.Log(LogLevel.Information, "DeleteBook called with id {0}, book deleted", id);
        return NoContent();
    }

    [HttpGet]
    [Route("GetAvailableBooks")]
    public IActionResult GetAvailableBooks()
    {
        var books = _bookService.GetAvailableBooks();
        _logger.Log(LogLevel.Information, "GetAvailableBooks called, returning {0} books", books.Count);
        return Ok(books);
    }
    
    [HttpPut("availability/{id}")]
    public IActionResult UpdateAvailability(Guid id, [FromBody] BookAvailabilityDto bookAvailabilityDto)
    {
        var book = _bookService.GetBook(id);
        _logger.Log(LogLevel.Information, bookAvailabilityDto.Available.ToString());

        if (book == null)
        {
            _logger.Log(LogLevel.Information, "UpdateAvailability called with id {0}, but no book exists with that id", id);
            return NotFound();
        }
        
        _mapper.Map(bookAvailabilityDto, book);
        var updatedBook = _bookService.UpdateBook(id, book);
        _logger.Log(LogLevel.Information, "UpdateAvailability called with id {0}, returning updated book", id);
        return Ok(updatedBook);
    }
}
