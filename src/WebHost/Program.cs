﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace cd.WebHost {
	public class Program {
		public static void Main(string[] args) {
			var config = new ConfigurationBuilder()
				.AddCommandLine(args)
				.Build();

			var host = new WebHostBuilder()
				.UseConfiguration(config)
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
