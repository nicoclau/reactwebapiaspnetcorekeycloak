using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyWebApi.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi
{
    public class Startup
    {        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _currentEnvironment = env;
        }

        private readonly IWebHostEnvironment _currentEnvironment;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://10.7.7.11:3000")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.ConfigureJWT(_currentEnvironment.IsDevelopment(), "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAi6G5wRCfqjXJFBhvK+UwAUFU9LDcT3aet0gGZk8hMMPfF5SEBaPqTDuLMh85VXtm0I0KBpUtlgNzMqcmWVoSFTNSSJnmBBmD/26xcidu/wuo4m3wTIca2kOLBtMP/3sjIDOXAQYaCOXjbbDbNB1S49VD6wUyJy8gGwiTsDzuZcNsS5c+hnAiI+WHqUnSll/EGZcKp1Yv7BZH9fYRXdTGYRGcH6ZRH8Nhl9w6QL+gSRA2wZLjS9r5NdZ5Ey8iSezs1Htdtg2sj0mA1QlvdOkPQVzD5hW80it5sHMY0l1W1XJSPnkdGNaTJXyn5Fto15uJhj5nE6MwkwiTfGlwxmZQLwIDAQAB");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApi", Version = "v1" });

                //First we define the security scheme
                c.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
