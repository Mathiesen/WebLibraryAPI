using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class BorrowRecord
{
    [Column(TypeName =  "uniqueidentifier")]
    public Guid Id { get; set; }
    [Column(TypeName =  "uniqueidentifier")]
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    [Column(TypeName =  "uniqueidentifier")]
    public Guid BorrowerId { get; set; }
    public Borrower Borrower { get; set; }
    public DateTime BorrowedOn { get; set; }
}