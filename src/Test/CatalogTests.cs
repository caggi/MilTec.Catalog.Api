using Application.Core.Controllers;
using Domain.Enums;
using Infra.Contexts;
using Infra.Repositories;

namespace Test
{
    public class CatalogTests
    {
        private readonly Context _context;
        private readonly CatalogRepository _repository;
        private readonly CatalogController _controller;

        public CatalogTests()
        {
            _context = new Context(@"../../../../Infra/Contexts/books.json");
            _repository = new CatalogRepository(_context);
            _controller = new CatalogController(_repository);
        }

        #region Repository Get

        [Test]
        public async Task GetWithoutFilterAsync()
        {
            var books = await _repository.Get(null, null);

            Assert.That(books, Has.Count.EqualTo(5));
            Assert.Multiple(() =>
            {
                Assert.That(books[0].Id, Is.EqualTo(1));
                Assert.That(books[1].Id, Is.EqualTo(2));
                Assert.That(books[2].Id, Is.EqualTo(3));
                Assert.That(books[3].Id, Is.EqualTo(4));
                Assert.That(books[4].Id, Is.EqualTo(5));
            });
        }

        [Test]
        public async Task GetOrderByPriceDescAsync()
        {
            var books = await _repository.Get(null, BooksOrders.ByPriceDesc);

            Assert.That(books, Has.Count.EqualTo(5));
            Assert.Multiple(() =>
            {
                Assert.That(books[0].Id, Is.EqualTo(4));
                Assert.That(books[1].Id, Is.EqualTo(2));
                Assert.That(books[2].Id, Is.EqualTo(1));
                Assert.That(books[3].Id, Is.EqualTo(3));
                Assert.That(books[4].Id, Is.EqualTo(5));
            });
        }

        [Test]
        public async Task GetOrderByPriceAscAsync()
        {
            var books = await _repository.Get(null, BooksOrders.ByPriceAsc);

            Assert.That(books, Has.Count.EqualTo(5));
            Assert.Multiple(() =>
            {
                Assert.That(books[0].Id, Is.EqualTo(5));
                Assert.That(books[1].Id, Is.EqualTo(3));
                Assert.That(books[2].Id, Is.EqualTo(1));
                Assert.That(books[3].Id, Is.EqualTo(2));
                Assert.That(books[4].Id, Is.EqualTo(4));
            });
        }

        [Test]
        public async Task GetQueryAsync()
        {
            var books = await _repository.Get("Find", null);

            Assert.That(books, Has.Count.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(books[0].Id, Is.EqualTo(4));
            });
        }

        [Test]
        public async Task GetQueryAndOrderByPriceAscAsync()
        {
            var books = await _repository.Get("10", BooksOrders.ByPriceAsc);

            Assert.That(books, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(books[0].Id, Is.EqualTo(1));
                Assert.That(books[1].Id, Is.EqualTo(2));
            });
        }

        #endregion

        #region Repository GetById

        [Test]
        [TestCase(0, null)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, null)]
        public async Task GetByIdAsync(int id, int? result)
        {
            var book = await _repository.Get(id);

            if (book is not null)
                Assert.That(book.Id, Is.EqualTo(result));
            else
                Assert.That(book, Is.Null);
        }

        #endregion

    }
}