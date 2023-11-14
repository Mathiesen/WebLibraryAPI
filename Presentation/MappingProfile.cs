using AutoMapper;
using Models;

namespace Presentation;

public class MappingProfile :  Profile
{
    public MappingProfile()
    {
        CreateMap<BookAvailabilityDto, Book>();
        CreateMap<Book, BookAvailabilityDto>();
        CreateMap<CreateBorrowerDTO, Borrower>();
        CreateMap<Borrower, CreateBorrowerDTO>();
    }
}