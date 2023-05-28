using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SealisMovies.Models
{
    [Table("Categories")]
    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }
    }
}
