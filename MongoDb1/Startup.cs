using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDb.Filter;
using MongoDb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MongoDb
{
    public class Startup
    {
        public IHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conStr = ((ConfigurationSection)Configuration.GetSection("ConnectionStrings:ConnectionString")).Value;
            var dbName = ((ConfigurationSection)Configuration.GetSection("ConnectionStrings:DatabaseName")).Value;
            services.AddControllers(o =>
            {
                // 全局异常过滤
                o.Filters.Add(typeof(CustomerExceptionFilter));
            });

            services.AddControllersWithViews()
             .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = false;
                // json传参时日期格式转换
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            })
              .AddRazorRuntimeCompilation();
            services.AddSingleton<IMongoDatabase>(x =>
            {
                var client = new MongoClient(conStr);
                var dbcontext = client.GetDatabase(dbName);
                return dbcontext;
            });
            services.AddTransient(typeof(DbContext<>));
#if DEBUG
            //InitData(services);
#else
#endif
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            ServiceLocator.Instance = app.ApplicationServices;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services">ServiceCollection</param>
        /// <returns>模板ID</returns>
        private async Task InitData(IServiceCollection services)
        {
            await Task.Run(async () =>
            {
                var setting = services.BuildServiceProvider().GetService<DbContext<Province>>();
                List<Province> list = new List<Province>();
                for (int i = 1; i <= 1000000; i++)
                {
                    list.Add(new Province { Name = $"MongoDb{i}", Age = i, AddTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc) });
                }

                await setting.CreateMany(list);
            });

        }

        /// <summary>
        /// 所有的注入对象集合
        /// </summary>
        public class ServiceLocator
        {
            public static IServiceProvider Instance { get; set; }
        }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.TryParse(reader.GetString(), out var dateTime) ? dateTime : default(DateTime);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
