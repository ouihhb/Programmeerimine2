using System;
using System.Linq;
using KooliProjekt.Application.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductEntity = KooliProjekt.Application.Data.Product;
using WebProgram = KooliProjekt.WebAPI.Program;

namespace KooliProjekt.IntegrationTests
{
    public class TestBase : WebApplicationFactory<WebProgram>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("IntegrationTestsDb_" + Guid.NewGuid());
                });

                var provider = services.BuildServiceProvider();

                using var scope = provider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customer = new Customer
                {
                    Id = 1,
                    Name = "Test Customer",
                    Email = "test@test.ee"
                };

                var product = new ProductEntity
                {
                    Id = 1,
                    Name = "Test Product",
                    Price = 10
                };

                var order = new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now
                };

                var orderItem = new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 2
                };

                context.Customers.Add(customer);
                context.Products.Add(product);
                context.Orders.Add(order);
                context.OrderItems.Add(orderItem);
                context.SaveChanges();
            });
        }
    }
}