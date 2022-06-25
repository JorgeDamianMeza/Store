using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using Store.Book.Application;
using Store.Book.Model;
using Store.Book.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Store.Book.Test
{
    public class BookServiceTest
    {

        private IEnumerable<LibraryMaterial> DataTest()
        {
            //This method is fulled with Genfu Data
            A.Configure<LibraryMaterial>()
                .Fill(x => x.Title).AsArticleTitle()
                .Fill(x => x.LibraryMaterialId, () => { return Guid.NewGuid(); });

            var list = A.ListOf<LibraryMaterial>(30);
            list[0].LibraryMaterialId = Guid.Empty;

            return list;
        }

        private Mock<LibraryContext> CreateContext()
        {
            var dataTest = DataTest().AsQueryable();
            var dbSet = new Mock<DbSet<LibraryMaterial>>();
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(X => X.Provider).Returns(dataTest.Provider);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(X => X.Expression).Returns(dataTest.Expression);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(X => X.ElementType).Returns(dataTest.ElementType);
            dbSet.As<IQueryable<LibraryMaterial>>().Setup(X => X.GetEnumerator()).Returns(dataTest.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibraryMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibraryMaterial>(dataTest.GetEnumerator()));

            dbSet.As<IQueryable<LibraryMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibraryMaterial>(dataTest.Provider));

            var context = new Mock<LibraryContext>();
            context.Setup(x=> x.LibraryMaterials).Returns(dbSet.Object);
            return context;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mokContext = CreateContext();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new QueryFilter.UniqueBook();
            request.BookId = Guid.Empty;

            var hanlder = new QueryFilter.Handler(mokContext.Object, mapper);

            var book = await hanlder.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(book);
            Assert.True(book.LibraryMaterialId == Guid.Empty);
        }


        [Fact]
        public async void GetLibros()
        {          
            var mokContext = CreateContext();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            Query.Handler handler = new Query.Handler(mokContext.Object, mapper);
            Query.Execute request = new Query.Execute();
            var list = await handler.Handle(request, new System.Threading.CancellationToken());
            Assert.True(list.Any());
        }

        [Fact]
        public async void SaveBook()
        {          
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: "DataBaseBook")
                .Options;

            var context = new LibraryContext(options);

            var request = new New.Execute();
            request.Title = "Microservice Book";
            request.BookAuthor = Guid.Empty;
            request.PublicacionDate = DateTime.Now;

            var handler = new New.Handler(context,null);
            var book = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(book != null);
        }

    }
}
