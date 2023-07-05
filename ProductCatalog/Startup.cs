using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ProductCatalog.Repositories;
using ProductCatalog.Repositories.Interfaces;
using ProductCatalog.Settings;

namespace ProductCatalog
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

            // Configuration for mongodb undestanted guid type
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            // dependecy injection
            services.AddSingleton<IMongoClient>(ServiceProvider =>
            {
                return new MongoClient(mongoDbSettings.ConnectionString);
            });
            
            services.AddSingleton<ICategoryRepository, MongoDbCategoryRepository>();
            
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductCatalog", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCatalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}