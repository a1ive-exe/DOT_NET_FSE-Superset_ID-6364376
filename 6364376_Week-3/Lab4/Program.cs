using System;
using System.Threading.Tasks;
using RetailInventory.Models;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("📦 Inserting initial data...");

        using var context = new AppDbContext();

        // Define categories
        var electronics = new Category
        {
            Name = "Electronics",
            Products = new List<Product>() // Empty list for now
        };

        var groceries = new Category
        {
            Name = "Groceries",
            Products = new List<Product>()
        };

        // Add categories
        await context.Categories.AddRangeAsync(electronics, groceries);
        await context.SaveChangesAsync(); // Save to generate category IDs

        // Define products linked to the above categories
        var product1 = new Product
        {
            Name = "Laptop",
            Price = 75000,
            CategoryId = electronics.Id
        };

        var product2 = new Product
        {
            Name = "Rice Bag",
            Price = 1200,
            CategoryId = groceries.Id
        };

        // Add products
        await context.Products.AddRangeAsync(product1, product2);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Data inserted successfully!");
    }
}
