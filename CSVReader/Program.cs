using CSVReader.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CSVReader {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            //////////////////////////////////////////////////////////////////////////////////
            #region ����
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


            var app = builder.Build();

            app.UseCors();

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