using Microsoft.AspNetCore.Identity;
using MyAnnuaireModel.Context;
using MyAnnuaireModel.Entities;

namespace CaveManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddIdentityCookies();
            builder.Services.AddAuthorizationBuilder();

            builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<MyAnnuaireContext>()
                .AddApiEndpoints();

            // Add services to the container.
            builder.Services.AddDbContext<MyAnnuaireContext>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.MapIdentityApi<User>();

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
