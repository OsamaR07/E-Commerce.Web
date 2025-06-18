using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                // Update Database if not updated.
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                // ProductBrands Seed.
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (ProductBrands is not null && ProductBrands.Count > 0)
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }

                // ProductTypes Seed.
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                    if (ProductTypes is not null && ProductTypes.Count > 0)
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }

                // Products Seed.
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (Products is not null && Products.Count > 0)
                        await _dbContext.Products.AddRangeAsync(Products);
                }


                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // TODO
            }
        }
    }
}
