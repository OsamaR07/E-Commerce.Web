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
        public void DataSeed()
        {
            try
            {
                // Update Database if not updated.
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }

                // ProductBrands Seed.
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // Convert "string" to C# Objects [ProductBrands]
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsData);
                    if (ProductBrands is not null && ProductBrands.Count > 0)
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                }

                // ProductTypes Seed.
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    // Convert "string" to C# Objects [ProductTypes]
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    if (ProductTypes is not null && ProductTypes.Count > 0)
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                }

                // Products Seed.
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    // Convert "string" to C# Objects [Products]
                    var Products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (Products is not null && Products.Count > 0)
                        _dbContext.Products.AddRange(Products);
                }


                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO
            }
        }
    }
}
