using HW1.Models;
using HW1.Services;
using Microsoft.EntityFrameworkCore;

namespace HW1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSql")));

            builder.Services.AddSingleton<ItemService>();
            builder.Services.AddSingleton<BlobService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient<TranslateService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

            app.Run();
        }
    }
}
