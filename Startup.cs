using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Repository;
using LanchesMac.Repository.Interface;
using LanchesMac.Models;
using Microsoft.AspNetCore.Identity;

namespace LanchesMac;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddIdentity<IdentityUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddTransient<ILanchesRepository,LancheRepository>();
        services.AddTransient<ICategoriaRepository,CategoriaRepository>();
        services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
        services.AddTransient<IPedidosRepository,PedidosRepository>();
        services.AddScoped(sp=> CarrinhoCompra.GetCarrinho(sp));
        services.AddControllersWithViews();
        services.AddMemoryCache();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
             name: "CategoriaFiltro",
             pattern: "Lanche/{action}/{categoria?}",
             defaults: new { Controller = "Lanche", action = "List" }
            );
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}