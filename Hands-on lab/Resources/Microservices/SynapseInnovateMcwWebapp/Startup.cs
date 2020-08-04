using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SynapseInnovateMcwWebapp.Interfaces;
using SynapseInnovateMcwWebapp.Services;

namespace SynapseInnovateMcwWebapp
{
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
            services.AddControllersWithViews();
            services.AddTransient<IQueryService, QueryService>();
            services.AddTransient<IWriteService, WriteService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            var queryService = app.ApplicationServices.GetService<IQueryService>();
            var metadata = queryService.GetMetadataAsync(1).Result;
            if (metadata.MetadataFactories.Count == 0
                && metadata.MetadataMachines.Count == 0
                && metadata.MetadataMaintenanceLookups.Count == 0)
            {
                var writeService = app.ApplicationServices.GetService<IWriteService>();
                PopulateMetadataAsync(writeService).Wait();
            }
        }

        private async Task PopulateMetadataAsync(IWriteService writeService)
        {
            Console.WriteLine("Populating Metadata.");
            var metadataFactories = Enumerable.Range(0, 100)
                .Select(i => new MetadataFactory
                {
                    Id = Guid.NewGuid(),
                    Name = "Factory 1",
                    Location = new Location
                    {
                        Latitude = -34,
                        Longitude = 151,
                        Address1 = "4122 Broad Bay Way",
                        Address2 = "Suite 400",
                        City = "Aurora",
                        State = "IL",
                        Country = "US",
                        PostalCode = "60502",
                    },
                    DateInService = "4/12/2011",
                }).ToList();

            var metadataMachines = Enumerable.Range(0, 100)
                .Select(i => new MetadataMachine
                {
                    Id = Guid.NewGuid(),
                    SerialNumber = "212X4821BYG",
                    DateInService = "7/23/2017",
                    LastMaintenanceDate = "7/27/2019 13:42:01Z",
                }).ToList();

            var metadataMaintenanceLookups = Enumerable.Range(0, 100)
                .Select(i => new MetadataMaintenanceLookup
                {
                    Id = Guid.NewGuid(),
                    Pressure = "<7475",
                    MachineTemperature = "<70",
                    MaintenanceAdjustmentRequired = "Tighten Adjustment Harness",
                }).ToList();

            await writeService.CreateFactoriesAsync(metadataFactories);
            Console.WriteLine("Created Factory Metadata.");

            await writeService.CreateMachinesAsync(metadataMachines);
            Console.WriteLine("Created Machine Metadata.");

            await writeService.CreateMaintenanceLookupsAsync(metadataMaintenanceLookups);
            Console.WriteLine("Created Maintenance Lookup Metadata.");
        }
    }
}