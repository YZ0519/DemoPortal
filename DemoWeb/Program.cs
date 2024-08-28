using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Net;
using System.Text;
using SimplePOSWeb.Helper;
using SimplePOSWeb.Middleware;
using SimplePOSWeb.Services.Abstraction;
using SimplePOSWeb.Services.Implementation;
using Serilog;
using SimplePOSWeb.Process.Abstraction;
using SimplePOSWeb.Process.Implementation;
using System.Reflection.Metadata;
using Shared.Helper;

namespace SimplePOSWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, configuration) =>
               configuration.ReadFrom.Configuration(context.Configuration));

            // Add services to the container.
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddSingleton<IHttpServices, HttpServices>();
            builder.Services.AddSingleton<ITokenServices, TokenServices>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<IProfileServices, ProfileServices>();
            builder.Services.AddScoped<IOptionServices, OptionServices>();

            builder.Services.AddScoped<IOptionProcess, OptionProcess>();
            builder.Services.AddScoped<IHelperProcess, HelperProcess>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            int sessionTimeout = builder.Configuration.GetValue<int>("Jwt:TokenExpiryMinutes");
            // Cookie Policy
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeout);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //Middleware
            app.UseMiddleware<TokenAuthMiddleware>();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // to enable Session usage
            app.UseSession();
            app.UseStatusCodePages(async (context) =>
            {
                // Redirect 401 to Login Page
                var response = context.HttpContext.Response;
                if (response.StatusCode is (int)HttpStatusCode.Unauthorized or (int)HttpStatusCode.Forbidden)
                {
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    else
                    {
                        response.Redirect(ConstantHelper.Url.Login);
                    }
                }
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}


