using KonnektuTask.API.ActionFilters;
using KonnektuTask.API.Factories;
using KonnektuTask.API.Tools;
using KonnektuTask.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace KonnektuTask.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opt => 
                opt.UseNpgsql(Configuration.GetConnectionString("PostgresConnectionString")));
            
            services.AddSingleton<IPasswordHasher>(provider => new PasswordHasher());
            services.AddSingleton<ResponseFactory>();

            services.AddControllers(config => config.Filters.Add<ValidationActionFilter>())
                .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}