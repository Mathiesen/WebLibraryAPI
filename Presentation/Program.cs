
using Business.Service;
using Data;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<LibraryContext>(opts =>
            opts.UseSqlServer("Server=EASV-DB4.easv.dk;Database=TEST_IKM;User Id=ikm;Password=Easv1234;TrustServerCertificate=True;"));
        builder.Services.AddAutoMapper(typeof(Program));
        
        // Services
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IBorrowerService, BorrowerService>();
        
        //Repositories
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
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

        app.UseRouting();
        //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}


