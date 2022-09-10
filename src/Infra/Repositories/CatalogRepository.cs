using Domain.Entities;
using Domain.Enums;
using Infra.Contexts;

namespace Infra.Repositories
{
    public class CatalogRepository
    {
        private readonly Context _context;

        public CatalogRepository(Context context) => _context = context;

        public async Task<List<Book>> Get(string? query, BooksOrders? order)
        {
            var books = await _context.Books;

            if (query is not null)
                books = books.FindAll(b => SearchQueryInBookProperties(query, b));

            _ = Enum.TryParse(order.ToString(), out BooksOrders orderValue);

            switch (orderValue)
            {
                case BooksOrders.ByPriceDesc:
                    books = books.OrderByDescending(book => book.Price).ToList();
                    break;
                case BooksOrders.ByPriceAsc:
                    books = books.OrderBy(book => book.Price).ToList();
                    break;
                default:
                    break;
            }

            return books;

            static bool SearchQueryInBookProperties(string searchQuery, Book book)
            {
                return SearchQueryInPropertyValue(searchQuery, book.Name)
                    || SearchQueryInPropertyValue(searchQuery, book.Price)
                    || SearchQueryInPropertyValue(searchQuery, (object)book.Specifications.OriginallyPublished)
                    || SearchQueryInPropertyValue(searchQuery, book.Specifications.Author)
                    || SearchQueryInPropertyValue(searchQuery, book.Specifications.PageCount)
                    || SearchQueryInPropertyValue(searchQuery, book.Specifications.Illustrator)
                    || SearchQueryInPropertyValue(searchQuery, book.Specifications.Genres);
            }
        }

        public async Task<bool> Exist(int id)
        {
            var books = await _context.Books;
            return books.Exists(book => book.Id == id);
        }

        public async Task<Book?> Get(int id)
        {
            var books = await _context.Books;
            return books.Find(book => book.Id == id);
        }

        public async Task<double?> CalculateShipping(int id)
        {
            var book = await Get(id);
            return book is not null ? book.Price * 0.2 : null;
        }

        #region Helper Methods

        private static bool SearchQueryInPropertyValue(string query, string value)
        {
            return value.Contains(query, StringComparison.OrdinalIgnoreCase);
        }

        private static bool SearchQueryInPropertyValue(string query, object value)
        {
            if (value is not null)
            {
                var @string = value.ToString();

                return @string is not null && SearchQueryInPropertyValue(query, @string);
            }

            return false;
        }

        #endregion
    }
}
