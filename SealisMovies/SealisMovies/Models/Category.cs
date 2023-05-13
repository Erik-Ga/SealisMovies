using System.ComponentModel.DataAnnotations.Schema;

namespace SealisMovies.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
