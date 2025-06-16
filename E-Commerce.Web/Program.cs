
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(
                o => o.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    )
                );

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var seeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            seeding.DataSeed();

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
