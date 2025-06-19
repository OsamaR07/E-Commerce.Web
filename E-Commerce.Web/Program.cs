using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service.MappingProfiles;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(o => o.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            #endregion

            var app = builder.Build();

            #region Seeding

            using var scope = app.Services.CreateScope();
            var seeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await seeding.DataSeedAsync();

            #endregion

            #region Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}