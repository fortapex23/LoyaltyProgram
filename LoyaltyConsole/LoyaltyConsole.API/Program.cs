using FluentValidation.AspNetCore;
using LoyaltyConsole.Business;
using LoyaltyConsole.Business.DTOs.TransactionDtos;
using LoyaltyConsole.Business.DTOs.UserDtos;
using LoyaltyConsole.Business.Mappings;
using LoyaltyConsole.Core.Models;
using LoyaltyConsole.Core.Settings;
using LoyaltyConsole.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoyaltyConsole.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            //var firebaseKeyPath = Path.Combine(builder.Environment.ContentRootPath, "Configs", "firebase-adminsdk.json");

            //FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile(firebaseKeyPath)
            //});

            builder.Services.AddControllers().AddFluentValidation(op =>
            {
                op.RegisterValidatorsFromAssembly(typeof(TransactionCreateDtoValidator).Assembly);
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddAutoMapper(op =>
            {
                op.AddProfile<MapProfile>();
            });

            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredUniqueChars = 1;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireDigit = true;

                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration.GetSection("JWT:audience").Value,
                    ValidAudience = builder.Configuration.GetSection("JWT:issuer").Value,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:secretKey").Value))
                };
            });

            builder.Services.AddRepositories(builder.Configuration.GetConnectionString("default"));
            builder.Services.AddServices();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Admin/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
