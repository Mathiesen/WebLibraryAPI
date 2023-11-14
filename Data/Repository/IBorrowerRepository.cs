using Models;

namespace Data.Repository;

public interface IBorrowerRepository
{
    IList<Borrower> GetBorrowers();
    Borrower GetBorrower(Guid id);
    Borrower CreateBorrower(Borrower borrower);
    bool BorrowerExists(Guid id);
    Borrower UpdateBorrower(Guid id, Borrower borrower);
    void DeleteBorrower(Guid id);
    void BorrowBook(Guid borrowerId, Book book);
}