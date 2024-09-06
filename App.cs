using System;

public class Class1
{
	public Class1()
	{
	}
}
//using DataLayer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System.Threading;
//using System.Threading.Tasks;

//public class DataInitializerService : IHostedService
//{
//    private readonly IServiceProvider _serviceProvider;
//    private readonly ILogger<DataInitializerService> _logger;

//    public DataInitializerService(IServiceProvider serviceProvider, ILogger<DataInitializerService> logger)
//    {
//        _serviceProvider = serviceProvider;
//        _logger = logger;
//    }

//    public async Task StartAsync(CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Starting data initialization...");

//        using (var scope = _serviceProvider.CreateScope())
//        {
//            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//            // Ensure the database is created (if it does not exist)
//            await context.Database.EnsureCreatedAsync(cancellationToken);

//            // Apply any pending migrations (if migrations exist)
//            await context.Database.MigrateAsync(cancellationToken);

//            await SeedDataAsync(context);
//        }

//        _logger.LogInformation("Data initialization completed.");
//    }

//    public Task StopAsync(CancellationToken cancellationToken)
//    {
//        // Optionally handle application shutdown logic
//        return Task.CompletedTask;
//    }

//    private async Task SeedDataAsync(AppDbContext context)
//    {
//        // Check if the database has been seeded already
//        if (!await context.Products.AnyAsync())
//        {
//            // Seed categories and products here
//            var electronicsCategory = new Category { Name = "Electronics" };
//            var furnitureCategory = new Category { Name = "Furniture" };

//            context.Categories.AddRange(electronicsCategory, furnitureCategory);

//            var laptop = new Product { Name = "Laptop", Price = 999.99m, Stock = 10, Category = electronicsCategory };
//            var chair = new Product { Name = "Chair", Price = 59.99m, Stock = 20, Category = furnitureCategory };

//            context.Products.AddRange(laptop, chair);

//            await context.SaveChangesAsync();
//        }
//    }
//}
