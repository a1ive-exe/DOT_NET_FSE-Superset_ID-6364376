using System.Collections.Generic;

namespace RetailInventory.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property - one category can have many products
        public List<Product> Products { get; set; }
    }
}
