using Thoughtful.Dal;

namespace Thoughtful.Api
{
    public class Program
    {
        public static async Task Main(string[] args)

        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            //try
            //{
            //    var context = services.GetRequiredService<ThoughtfulDbContext>();
            //    await Seed.SeedData(context);
            //}
            //catch (Exception e)
            //{
            //    var logger = services.GetRequiredService<ILogger<Program>>();
            //    logger.LogError(e, "An error occured during migration");
            //}

            await host.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseDefaultServiceProvider((context, options) =>
                    {
                        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                        options.ValidateOnBuild = false;
                    });
                });
    }
}