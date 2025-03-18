using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Types { get; set; }
        public int Product_quantiy { get; set; }
    }
}
