using BookingAppApi.Configuration;
using BookingAppApi.Data;
using BookingAppApi.UnitOfWork.IUnitOfWork;
using BookingAppApi.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using BookingAppApi.Helpers;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using BookingAppApi.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookingAppApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connString = builder.Configuration.GetConnectionString("BookingDbConnection");
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });
            // Add services to the container.
            builder.Services.AddDbContext <BookingAppApiDbContext> (options => options.UseSqlServer(connString));
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IUnitOfWorkRepo, UnitOfWorkRepo>();
            builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                        .UTF8.GetBytes(builder.Configuration["TokenKey"]))

                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            builder.Services.AddCors();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}