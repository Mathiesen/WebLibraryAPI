using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repository;

public class BorrowerRepository : IBorrowerRepository
{
    private readonly LibraryContext _context;
    
    public BorrowerRepository(LibraryContext context)
    {
        _context = context;
    }
    
    public IList<Borrower> GetBorrowers()
    {
        return _context
            .Borrowers
            .Include(x => x.BorrowedBooks)
            .ToList();
    }

    public Borrower GetBorrower(Guid id)
    {
        return _context
                   .Borrowers
                   .Include(x => x.BorrowedBooks)
                   .FirstOrDefault(x => x.Id == id) 
               ?? throw new InvalidOperationException();
    }

    public Borrower CreateBorrower(Borrower borrower)
    {
        _context.Borrowers.Add(borrower);
        _context.SaveChanges();
        return borrower;
    }

    public bool BorrowerExists(Guid id)
    {
        return _context.Borrowers.Any(x => x.Id == id);
    }

    public Borrower UpdateBorrower(Guid id, Borrower borrower)
    {
        var borrowerToUpdate = GetBorrower(id);
        borrowerToUpdate.Name = borrowerToUpdate.Name;
        _context.SaveChanges();
        return borrowerToUpdate;
    }

    public void BorrowBook(Guid borrowerId, Book book)
    {
        var borrower = GetBorrower(borrowerId);
        borrower.BorrowedBooks.Add(book);
        _context.SaveChanges();
    }
    
    public void DeleteBorrower(Guid id)
    {
        var borrower = GetBorrower(id);

        foreach (var book in borrower.BorrowedBooks)
        {
            book.BorrowerId = null;
        }
        
        _context.Borrowers.Remove(borrower);
        _context.SaveChanges();
    }
}