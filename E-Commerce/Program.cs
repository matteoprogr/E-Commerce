using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DbContesto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cefi")));
            builder.Services.AddTransient<IRepositoryCliente, ServiceClienti>();
            builder.Services.AddTransient<IRepositoryCategorie, ServiceCategorie>();
            builder.Services.AddTransient<IRepositoryOrdini, ServiceOrdini>();
            builder.Services.AddTransient<IRepositoryProdotti, ServiceProdotti>();

            builder.Services.AddDistributedMemoryCache();

            /*
             agiunge i servizi richiesti allo stato della sessione 
            optins rappresente un oggetto della classe sessionOptions per
            configurare lo stato della sessione
             */
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.IOTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.Name = ".E-Commerce.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Path = "/";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Use(async (context, next) =>
            {
            var lista_chiave = context.Session.Keys;
            if (!lista_chiave.Contains("carrello"))
            {
                context.Session.SetString("carrello", JsonConvert.SerializeObject(new Carrello()));
            }
                await next(context);
            });

            app.Run();
        }
    }
}
