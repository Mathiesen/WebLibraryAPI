using AutoMapper;
using Business.Service;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BorrowerController : Controller
{
    private readonly IBorrowerService _borrowerService;
    private readonly IMapper _mapper;
    
    public BorrowerController(IBorrowerService borrowerService, IMapper mapper)
    {
        _borrowerService = borrowerService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("GetBorrowers")]
    public IActionResult GetBorrowers()
    {
        var borrowers = _borrowerService.GetBorrowers();
        return Ok(borrowers);
    }

    [HttpGet("{id}")]
    public IActionResult GetBorrower(Guid id)
    {
        var borrower = _borrowerService.GetBorrower(id);
        if (borrower == null)
        {
            return NotFound();
        }
        return Ok(borrower);
    }

    [HttpPost]
    public IActionResult CreateBorrower([FromBody] CreateBorrowerDTO createBorrowerDto)
    {
        var borrower = new Borrower();
        _mapper.Map(createBorrowerDto, borrower);
        var newBorrower = _borrowerService.CreateBorrower(borrower);
        return CreatedAtAction(nameof(GetBorrower), new { id = newBorrower.Id }, newBorrower);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBorrower(Guid id, [FromBody] Borrower borrower)
    {
        if (!_borrowerService.BorrowerExists(id))
        {
            return NotFound();
        }

        var updatedBook = _borrowerService.UpdateBorrower(id, borrower);
        return Ok(updatedBook);
    }
    
    [HttpPut("{borrowerId}/BorrowBook")]
    public IActionResult BorrowBook(Guid borrowerId, [FromBody] Book book)
    {
        if (!_borrowerService.BorrowerExists(borrowerId))
        {
            return NotFound();
        }

        _borrowerService.BorrowBook(borrowerId, book);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBorrower(Guid id)
    {
        if (!_borrowerService.BorrowerExists(id))
        {
            return NotFound();
        }

        _borrowerService.DeleteBorrower(id);
        return NoContent();
    }
}