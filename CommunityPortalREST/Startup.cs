using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CommunityPortalREST.Models.Data;
using CommunityPortalREST.Models.Handlers;
using CommunityPortalREST.Models.Repositories;
using CommunityPortalREST.Models.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CommunityPortalREST
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CorsPolicyName = "AllowAllOrigins";
        }

        public IConfiguration Configuration { get; }
        private string CorsPolicyName { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PortalContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("Default")) 
                );
            
            services.AddScoped<PostRepository>();
            services.AddScoped<PostService>();

            services.AddScoped<CategoryRepository>();
            services.AddScoped<CategoryService>();

            services.AddScoped<AccountRepository>();
            services.AddScoped<AccountService>();

            services.AddScoped<RoleRepository>();

            services.AddScoped<TokenRepository>();

            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, AuthorizationHandler>
                    ("BasicAuthentication", null);
            
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommunityPortalREST", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommunityPortalREST v1"));
            }

            app.UseCors(CorsPolicyName);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
