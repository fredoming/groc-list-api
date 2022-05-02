using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GroceryListAPI.Infrastructure;
using GroceryListAPI.Infrastructure.Database;
using GroceryListAPI.Models.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Serilog;

namespace GroceryListAPI
{
  public class Startup
  {
    private IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
      BuildConfig();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication().AddGoogle(googleOptions =>
      {
        googleOptions.ClientId = Configuration["GoogleAuth:ClientId"];
        googleOptions.ClientSecret = Configuration["GoogleAuth:ClientSecret"];
      });

      services.AddControllers();


      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.AllowAnyOrigin();
          builder.AllowAnyMethod();
          builder.AllowAnyHeader();
        });
      });

      services.AddDbContext<GroceryListDbContext>(options =>
      {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        options
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(connectionString);
      });

      services.AddControllers().AddOData(opt =>
      {
        opt.AddRouteComponents("odata", GetEdmModel());
        opt.Filter().Select().Expand().OrderBy().SetMaxTop(null).Count();
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "GroceryListAPI", Version = "v1" });
      });

      services.AddHttpContextAccessor();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GroceryListDbContext dbContext)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GroceryListAPI v1"));
      }
      else
      {
        app.UseExceptionHandler(errorApp =>
        {
          errorApp.Run(async httpContext =>
          {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            httpContext.Response.ContentType = "application/json";

            IExceptionHandlerPathFeature exceptionDetails = httpContext.Features.Get<IExceptionHandlerPathFeature>();
            var standardResponse = new
            {
              Success = false,
              Message = exceptionDetails?.Error.ToString()
            };
            string json = JsonSerializer.Serialize(standardResponse);

            await httpContext.Response.WriteAsync(json);
            Log.Error(exceptionDetails?.Error, $"Exception in {exceptionDetails?.Path}");

          });
        });
      }

      dbContext.Database.Migrate();

      app.UseCors();
      app.UseRouting();

      //app.UseAuthorization();
      //app.UseAuthentication();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    private void BuildConfig()
    {
      string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
      ConfigurationContext.SetEnvironment(env);
      ConfigurationBuilder builder = new();
      if (env == "Development")
      {
        var s = Directory.GetCurrentDirectory();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.AddEnvironmentVariables();
      }
      else
      {
        //Cloud Secrets
      }

      Configuration = builder.Build();

      ConfigurationContext.BindSettings(Configuration);

      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();
    }

    private IEdmModel GetEdmModel()
    {
      var builder = new ODataConventionModelBuilder();

      builder.EntitySet<User>("Users");
      builder.EntitySet<GroceryList>("GroceryLists");
      builder.EntitySet<GroceryItem>("GroceryItems");



      return builder.GetEdmModel();

    }
  }
}
