using Data.Repository;
using Models;

namespace Business.Service;

public class BorrowerService : IBorrowerService
{
    private IBorrowerRepository _borrowerRepository;
    
    public BorrowerService(IBorrowerRepository borrowerRepository)
    {
        _borrowerRepository = borrowerRepository;
    }

    public IList<Borrower> GetBorrowers()
    {
        return _borrowerRepository.GetBorrowers();
    }

    public Borrower? GetBorrower(Guid id)
    {
        return _borrowerRepository.GetBorrower(id);
    }

    public Borrower CreateBorrower(Borrower borrower)
    {
        return _borrowerRepository.CreateBorrower(borrower);
    }

    public bool BorrowerExists(Guid id)
    {
        return _borrowerRepository.BorrowerExists(id);
    }

    public Borrower? UpdateBorrower(Guid id, Borrower borrower)
    {
        return _borrowerRepository.UpdateBorrower(id, borrower);
    }
    
    public void BorrowBook(Guid borrowerId, Book book)
    {
        _borrowerRepository.BorrowBook(borrowerId, book);
    }

    public void DeleteBorrower(Guid id)
    {
        _borrowerRepository.DeleteBorrower(id);
    }
}