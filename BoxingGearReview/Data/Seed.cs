using BoxingGearReview.Data;
using BoxingGearReview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public class Seed
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DataContext(
            serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
        {
            // Verifica se a base de dados já está populada
            if (context.Users.Any() || context.Brands.Any() || context.Categories.Any() || context.Equipments.Any())
            {
                return;   // DB já está populado
            }

            // Seed para Brand
            var brands = new[]
            {
                new Brand { Name = "Everlast" },
                new Brand { Name = "Adidas" },
                new Brand { Name = "Nike" },
                new Brand { Name = "Reebok" },
                new Brand { Name = "Under Armour" }
            };
            context.Brands.AddRange(brands);
            context.SaveChanges();

            // Seed para User
            var users = new[]
            {
                new User { Name = "John Doe", Email = "john@example.com", Password = "password123" },
                new User { Name = "Jane Smith", Email = "jane@example.com", Password = "password456" },
                new User { Name = "Tom Hardy", Email = "tom@example.com", Password = "password789" },
                new User { Name = "Alice Johnson", Email = "alice@example.com", Password = "password321" },
                new User { Name = "Bob Brown", Email = "bob@example.com", Password = "password654" }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Seed para Category
            var categories = new[]
            {
                new Category { Name = "Gloves" },
                new Category { Name = "Punching Bags" },
                new Category { Name = "Protective Gear" },
                new Category { Name = "Shoes" },
                new Category { Name = "Clothing" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Verificar e obter as entidades necessárias
            var everlastBrand = context.Brands.SingleOrDefault(b => b.Name == "Everlast");
            var adidasBrand = context.Brands.SingleOrDefault(b => b.Name == "Adidas");
            var nikeBrand = context.Brands.SingleOrDefault(b => b.Name == "Nike");
            var glovesCategory = context.Categories.SingleOrDefault(c => c.Name == "Gloves");
            var punchingBagsCategory = context.Categories.SingleOrDefault(c => c.Name == "Punching Bags");
            var protectiveGearCategory = context.Categories.SingleOrDefault(c => c.Name == "Protective Gear");

            if (everlastBrand == null || adidasBrand == null || nikeBrand == null || glovesCategory == null || punchingBagsCategory == null || protectiveGearCategory == null)
            {
                throw new InvalidOperationException("Algumas das marcas ou categorias necessárias não foram encontradas.");
            }

            // Seed para Equipment
            var equipments = new[]
            {
                new Equipment
                {
                    Name = "Boxing Gloves",
                    Description = "High-quality boxing gloves.",
                    Material = "Leather",
                    Price = 49.99M,
                    Image = Array.Empty<byte>(),  // Substituir por imagem real mais tarde
                    Brand = everlastBrand,
                    Category = glovesCategory
                },
                new Equipment
                {
                    Name = "Punching Bag",
                    Description = "Heavy-duty punching bag.",
                    Material = "Synthetic Leather",
                    Price = 129.99M,
                    Image = Array.Empty<byte>(), // Substituir por imagem real mais tarde
                    Brand = adidasBrand,
                    Category = punchingBagsCategory
                },
                new Equipment
                {
                    Name = "Mouth Guard",
                    Description = "Essential protective gear.",
                    Material = "Rubber",
                    Price = 14.99M,
                    Image = Array.Empty<byte>(), // Substituir por imagem real mais tarde
                    Brand = nikeBrand,
                    Category = protectiveGearCategory
                },
                new Equipment
                {
                    Name = "Boxing Shoes",
                    Description = "Lightweight boxing shoes for agility.",
                    Material = "Mesh",
                    Price = 89.99M,
                    Image = Array.Empty<byte>(), // Substituir por imagem real mais tarde
                    Brand = everlastBrand,
                    Category = glovesCategory
                },
                new Equipment
                {
                    Name = "Headgear",
                    Description = "Protective headgear for sparring.",
                    Material = "Foam",
                    Price = 59.99M,
                    Image = Array.Empty<byte>(), // Substituir por imagem real mais tarde
                    Brand = adidasBrand,
                    Category = protectiveGearCategory
                }
            };
            context.Equipments.AddRange(equipments);
            context.SaveChanges();

            // Verificar e obter as entidades necessárias para Review
            var johnDoe = context.Users.SingleOrDefault(u => u.Name == "John Doe");
            var janeSmith = context.Users.SingleOrDefault(u => u.Name == "Jane Smith");
            var tomHardy = context.Users.SingleOrDefault(u => u.Name == "Tom Hardy");
            var aliceJohnson = context.Users.SingleOrDefault(u => u.Name == "Alice Johnson");
            var bobBrown = context.Users.SingleOrDefault(u => u.Name == "Bob Brown");
            var boxingGloves = context.Equipments.SingleOrDefault(e => e.Name == "Boxing Gloves");
            var punchingBag = context.Equipments.SingleOrDefault(e => e.Name == "Punching Bag");
            var mouthGuard = context.Equipments.SingleOrDefault(e => e.Name == "Mouth Guard");
            var boxingShoes = context.Equipments.SingleOrDefault(e => e.Name == "Boxing Shoes");
            var headgear = context.Equipments.SingleOrDefault(e => e.Name == "Headgear");

            if (johnDoe == null || janeSmith == null || tomHardy == null || aliceJohnson == null || bobBrown == null || boxingGloves == null || punchingBag == null || mouthGuard == null || boxingShoes == null || headgear == null)
            {
                throw new InvalidOperationException("Alguns dos usuários ou equipamentos necessários não foram encontrados.");
            }

            // Seed para Review
            var reviews = new[]
            {
                new Review
                {
                    Rating = 4.3M,
                    Comment = "Amazing gloves, very durable!",
                    Date = DateTime.Now,
                    User = johnDoe,
                    Equipment = boxingGloves
                },
                new Review
                {
                    Rating = 4.1M,
                    Comment = "Good bag, but could be heavier.",
                    Date = DateTime.Now,
                    User = janeSmith,
                    Equipment = punchingBag
                },
                new Review
                {
                    Rating = 3.8M,
                    Comment = "Decent mouth guard, but a bit uncomfortable.",
                    Date = DateTime.Now,
                    User = tomHardy,
                    Equipment = mouthGuard
                },
                new Review
                {
                    Rating = 4.5M,
                    Comment = "Very comfortable shoes, great for movement.",
                    Date = DateTime.Now,
                    User = aliceJohnson,
                    Equipment = boxingShoes
                },
                new Review
                {
                    Rating = 4.2M,
                    Comment = "Headgear provides good protection.",
                    Date = DateTime.Now,
                    User = bobBrown,
                    Equipment = headgear
                }
            };
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }
    }
}
