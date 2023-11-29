using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EPE3_PUNTONET.Repositorio;



namespace EPE3_PUNTONET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configuración de la cadena de conexión a la base de datos
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            // Configuración de Entity Framework para MySQL
            services.AddDbContext<YourDbContext>(options =>
                options.UseMySQL(connectionString));

            // Registro de los repositorios
            services.AddScoped<MedicoRepository>();

            // Otros servicios y configuraciones pueden agregarse aquí
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

  
