using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microservice.First.Migrations.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Microservice.First
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Create(services, "server=127.0.0.1;userid=postgres;password=mypass;", "bookdb");
            Create(services, "server=127.0.0.1;userid=postgres;password=mypass;", "anotherdb");
        }

        private static void Create(IServiceCollection services, string conn, string dbname)
        {
            MigrationTool.UpsertDb(conn, dbname);

            var serviceProvider = new ServiceCollection().AddFluentMigratorCore()
                .ConfigureRunner(runnerBuilder =>
                    runnerBuilder
                    .AddPostgres()
                    .WithVersionTable(new VersionTable { })
                    .WithGlobalConnectionString($"{conn}database={dbname};")
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(builder => builder.AddFluentMigratorConsole())
                .Configure<RunnerOptions>(options =>
                {
                    options.Tags = new[] { dbname };
                })
                .Configure<FluentMigratorLoggerOptions>(options =>
                {
                    options.ShowElapsedTime = true;
                }).BuildServiceProvider();

            MigrationRun(serviceProvider);
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
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        private static void MigrationRun(ServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator.MigrateUp();
        }
    }
}
