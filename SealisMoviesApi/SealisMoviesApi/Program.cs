
using Microsoft.EntityFrameworkCore;
using SealisMoviesApi.Data;
using SealisMoviesApi.Models;

namespace SealisMoviesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MyConnection");
            Console.WriteLine("Connectionsträngen: " + connectionString);
            builder.Services.AddDbContext<Data.MyDBContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddTransient<CategoryManager>();





            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}