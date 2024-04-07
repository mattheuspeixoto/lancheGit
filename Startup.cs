using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Repository;
using LanchesMac.Repository.Interface;
using LanchesMac.Models;
using Microsoft.AspNetCore.Identity;
using LanchesMac.Services;
using ReflectionIT.Mvc.Paging;
using LanchesMac.Areas.Admin.Servicos;

namespace LanchesMac;
public class Startup{
    public Startup(IConfiguration configuration){
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services){
        services.AddIdentity<IdentityUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

                    // Alterar padrao de senha
        // services.Configure<IdentityOptions>(options=> {
        //     options.Password.RequireDigit = true;
        //     options.Password.RequireLowercase= true;
        //     options.Password.RequireNonAlphanumeric=true;
        //     options.Password.RequireUppercase=true;
        //     options.Password.RequiredLength = 8;
        //     options.Password.RequiredUniqueChars =1;
        // }); 

        /*string dataSource = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
        string connectionString = $"Server={dataSource};" + Configuration.GetConnectionString("DefaultConnection"); ;
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString)); */
        services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        
        services.AddTransient<ILanchesRepository,LancheRepository>();
        services.AddTransient<ICategoriaRepository,CategoriaRepository>();
        services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
        services.AddTransient<IPedidosRepository,PedidosRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<RelatorioVendasService>();

        // Registra a Politica de Perfis 
        services.AddAuthorization(options => {
            options.AddPolicy("Admin",
                politica => {
                    politica.RequireRole("Admin");
                });
        });
        services.AddScoped(sp=> CarrinhoCompra.GetCarrinho(sp));
        services.AddControllersWithViews();

        services.AddPaging(options => {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";

        });
        services.AddMemoryCache();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context, ISeedUserRoleInitial seedUserRoleInitial) {
        context.Database.Migrate();
        if (env.IsDevelopment()){
            app.UseDeveloperExceptionPage();
        }
        else{
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        //Cria os perfis
        seedUserRoleInitial.SeedRoles();

        //Cria os Usuarios e coloca-os nos perfis
        seedUserRoleInitial.SeedUsers();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
             name: "areas",
             pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
            );
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