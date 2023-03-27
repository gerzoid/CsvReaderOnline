using Contracts;
using Contracts.Repository;
using CSVReader.Extensions;
using CsvService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Serilog;
using Utils;

namespace CSVReader {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            //////////////////////////////////////////////////////////////////////////////////
            #region Логи
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            #endregion

            //////////////////////////////////////////////////////////////////////////////////
            #region CORS
            builder.Services.AddCors(options => {
                options.AddPolicy("Policy1", policy => {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    //.WithMethods("POST", "GET", "PUT", "DELETE")
                    .AllowAnyHeader();
                });
            });
            #endregion

            //////////////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////////////////
            #region DATABASE
            builder.Services.AddDbContext<DatabaseContext>(options => {
                options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"), b => b.MigrationsAssembly("CSVReader"));
            });
            #endregion

            builder.Services.AddScoped<ICsvService, CsvService.CsvService>();
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

            Helper.UploadFolder = builder.Configuration["UploadFolder"] ?? "wwwroot/upload";


            var app = builder.Build();

            app.ConfigureExceptionHandler(app.Logger);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseCookiePolicy();
            
            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }
    }
}