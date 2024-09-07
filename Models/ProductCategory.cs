using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlexonTask.Models
{
    public class ProductCategory
    {
        public int Id { get; set; } 
        public string? CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
