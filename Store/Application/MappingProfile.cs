using AutoMapper;
using Store.Model;

namespace Store.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookAuthor, AuthorDto>();
        }
    }
}
