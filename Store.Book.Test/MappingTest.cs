using AutoMapper;
using Store.Book.Application;
using Store.Book.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Book.Test
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibraryMaterial, LibraryMaterialDto>();
        }
    }
}
