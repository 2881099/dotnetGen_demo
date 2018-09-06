using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace cd.WebHost {
	public class Startup {
		public Startup(IHostingEnvironment env) {
			var builder = new ConfigurationBuilder()
				.LoadInstalledModules(Modules, env)
				.AddCustomizedJsonFile(Modules, env, "/var/webos/cd/");

			this.Configuration = builder.AddEnvironmentVariables().Build();
			this.env = env;

			Newtonsoft.Json.JsonConvert.DefaultSettings = () => {
				var st = new Newtonsoft.Json.JsonSerializerSettings();
				st.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
				st.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
				st.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;
				return st;
			};
			//单redis节点模式，如需开启集群负载，请将注释去掉并做相应配置
			RedisHelper.Initialization(
				csredis: new CSRedis.CSRedisClient(//null,
					//Configuration["ConnectionStrings:redis2"],
					Configuration["ConnectionStrings:redis1"]),
				serialize: value => Newtonsoft.Json.JsonConvert.SerializeObject(value),
				deserialize: (data, type) => Newtonsoft.Json.JsonConvert.DeserializeObject(data, type));
		}

		public static IList<ModuleInfo> Modules = new List<ModuleInfo>();
		public IConfiguration Configuration { get; }
		public IHostingEnvironment env { get; }

		public void ConfigureServices(IServiceCollection services) {
			services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
			services.AddSingleton<IConfiguration>(Configuration);
			services.AddSingleton<IHostingEnvironment>(env);
			services.AddScoped<CustomExceptionFilter>();

			services.AddSession(a => {
				a.IdleTimeout = TimeSpan.FromMinutes(30);
				a.Cookie.Name = "Session_cd";
			});
			services.AddCors(options => options.AddPolicy("cors_all", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
			services.AddCustomizedMvc(Modules);
			services.Configure<RazorViewEngineOptions>(options => { options.ViewLocationExpanders.Add(new ModuleViewLocationExpander()); });

			if (env.IsDevelopment())
				services.AddCustomizedSwaggerGen();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime) {
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Console.OutputEncoding = Encoding.GetEncoding("GB2312");
			Console.InputEncoding = Encoding.GetEncoding("GB2312");

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddNLog().AddDebug();
			NLog.LogManager.LoadConfiguration("nlog.config");

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			cd.BLL.SqlHelper.Initialization(app.ApplicationServices.GetService<IDistributedCache>(), Configuration.GetSection("cd_BLL_ITEM_CACHE"),
				Configuration["ConnectionStrings:cd_mysql"], loggerFactory.CreateLogger("cd_DAL_sqlhelper"));

			app.UseSession();
			app.UseCors("cors_all");
			app.UseCustomizedMvc(Modules);
			app.UseCustomizedStaticFiles(Modules);

			if (env.IsDevelopment())
				app.UseCustomizedSwagger(env);
		}
	}
}
