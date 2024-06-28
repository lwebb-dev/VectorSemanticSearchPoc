using Amazon;
using Amazon.BedrockRuntime;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VectorSemanticSearchPoc.Components;
using VectorSemanticSearchPoc.Data;
using VectorSemanticSearchPoc.Services;

namespace VectorSemanticSearchPoc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddLogging();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            string connectionString = config.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextFactory<SemanticSearchPocContext>(options =>
            {
                options.UseNpgsql(connectionString, o => o.UseVector());
            });


            builder.Services.AddSingleton<IAmazonBedrockRuntime>(new AmazonBedrockRuntimeClient(config["AWS:AccessKeyId"], config["AWS:SecretAccessKey"], config["AWS:SessionToken"], RegionEndpoint.USEast1));
            builder.Services.AddScoped<IEmbeddingService, EmbeddingService>();
            builder.Services.AddScoped<ICourseService, CourseService>();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
