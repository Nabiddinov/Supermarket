using Bogus;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Models;

namespace Supermarket.Extension
{
    public class Seeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new SupermarketDbContext(serviceProvider.GetRequiredService<DbContextOptions<SupermarketDbContext>>());

            var faker = new Faker();

            if (context.Products.Any())
            {
                return;
            }

            string[] categoriesName = faker.Commerce.Categories(20); ;

            foreach (var category in categoriesName)
            {
                context.Categories.Add(new Category
                {
                    Name = category
                });
            }

            var catergories = context.Categories.ToList();

            foreach (var catergory in catergories)
            {
                int productCount = faker.Random.Int(5, 25);

                for (int i = 0; i < productCount; i++)
                {
                    context.Products.Add(new Product
                    {
                        Name = faker.Commerce.ProductName(),
                        Price = faker.Random.Decimal(15000, 1_000_000),
                        CategoryId = catergory.Id
                    });
                }
            }

            context.Products.ToList();

            context.SaveChanges();
        }
    }
}