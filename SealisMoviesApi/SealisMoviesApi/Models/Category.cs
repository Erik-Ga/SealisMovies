using System.ComponentModel.DataAnnotations.Schema;

namespace SealisMoviesApi.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
