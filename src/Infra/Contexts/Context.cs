using Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infra.Contexts
{
    public class Context
    {
        internal Task<List<Book>> Books { get; }

        public Context(string booksConnectionString)
        {
            Books = JsonFileReader.ReadAsync<List<Book>>(booksConnectionString);
        }
    }

    internal static class JsonFileReader
    {
        internal static async Task<T> ReadAsync<T>(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            using FileStream stream = File.OpenRead(filePath);
            T? result = await JsonSerializer.DeserializeAsync<T>(stream, options);

            return result is not null ? result : throw new FileNotFoundException();
        }
    }
}
