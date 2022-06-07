using AutoMapper;
using Store.Book.Model;

namespace Store.Book.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibraryMaterial, LibraryMaterialDto>();
        }
    }
}
