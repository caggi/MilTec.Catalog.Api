using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Specifications
    {
        [JsonPropertyName("Originally published")]
        public string OriginallyPublished { get; set; }
        public string Author { get; set; }

        [JsonPropertyName("Page count")]
        public int PageCount { get; set; }
        public object Illustrator { get; set; }
        public object Genres { get; set; }
    }
}