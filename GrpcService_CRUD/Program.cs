using GrpcService_CRUD.Data;
using GrpcService_CRUD.Services;
using Microsoft.EntityFrameworkCore;


namespace GrpcService_CRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            //Dependency Injection 
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
                //option.UseInMemoryDatabase("InMemory");
            });
            // Add services to the container.
            builder.Services.AddGrpc(option => option.EnableDetailedErrors = true)
                            .AddJsonTranscoding();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<ProductService>();
            app.MapGrpcService<AuthenticationService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}