using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using TEST.Server.Data;
using TEST.Server.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System;
using System.IO;
using Microsoft.AspNetCore.DataProtection;

namespace TEST.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string cs = Configuration.GetConnectionString("DefaultConnection");
            // pour tester :
            try
            {
                Console.WriteLine("     ******** testing connection");
                using (SqlConnection cn = new SqlConnection(cs))
                {
                    cn.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace + "  " + ex.InnerException);

            }

            services.AddDbContext<MonConciergeContext>(options => options.UseSqlServer(cs));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cs));
            services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();
            var identityServerBuilder = services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            //  identityServerBuilder.AddDeveloperSigningCredential();


            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Console.WriteLine("Environemnt is   " + env);


            if (env == "Development")
            {
                try
                {
                    Console.WriteLine("Size:" + new System.IO.FileInfo("IdentityServer.pfx").Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    Console.WriteLine("Content:" + File.ReadAllText("IdentityServer.pfx"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                Console.WriteLine("Environemnt is DEVELOPMENT " + env);
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                try
                {
                    Console.WriteLine("Instanciating X509Certificate2, reading all text from identityserver");
                    Console.WriteLine("Content:" + File.ReadAllText("IdentityServer.pfx"));
                    var certificate = new X509Certificate2(@"IdentityServer.pfx", "abc");
                    Console.WriteLine("AddSigningCredential, cert friendlyname " + certificate.Issuer);
                    identityServerBuilder.AddSigningCredential(certificate);
                    Console.WriteLine("AddSigningCredential() done");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("******* EXCEPTION: " + ex.Message + "\n\r" + ex.StackTrace + "\n\r" + ex.InnerException);
                }
            }

            // .LoadSigningCredentialFrom(Configuration["certificates:signing"]);
            Console.WriteLine("******* services.AddAuthentication().AddIdentityServerJwt();");
            services.AddAuthentication().AddIdentityServerJwt();
            Console.WriteLine("******* AddControllers().AddNewtonsoftJson(options => options.Set");
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddDbContext<MonConciergeContext>();
            services.AddControllersWithViews();
            services.AddAuthorization();
            services.AddRazorPages();

            //services.AddDataProtection()
            // .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"/var/my-af-keys/"));
            //services.AddDataProtection()
            //    .SetApplicationName("fow-customer-portal")
            //    .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"/var/dpkeys/"));


            services.AddScoped<IQuery, Query>();
            Console.WriteLine("******* ConfigureServices done");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("*******  Configure method, starting ., env + " + env.EnvironmentName);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            Console.WriteLine("*******  Configure method, starting UseHttpsRedirection");
            app.UseHttpsRedirection();
            Console.WriteLine("*******  Configure method, starting UseBlazorFrameworkFiles");
            app.UseBlazorFrameworkFiles();
            Console.WriteLine("*******  Configure method, starting UseStaticFiles");
            app.UseStaticFiles();
            app.UseRouting();
            Console.WriteLine("****** Configure method, calling  app.UseIdentityServer(); ...");

            //    string s = File.ReadAllText(@"C:\OLE\Projecten\BlazorTestApp\TEST\Server\appsettings.json");
            //            Rootobject account = JsonConvert.DeserializeObject<Rootobject>(s);


            app.UseIdentityServer();
            Console.WriteLine("****** Configure method, calling  app.UseAuthentication(); ...");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
