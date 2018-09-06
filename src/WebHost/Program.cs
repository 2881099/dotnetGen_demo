using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace cd.WebHost {
	public class Program {
		public static void Main(string[] args) {
			var host = new WebHostBuilder()
				.UseUrls("http://*:5000", "http://*:5001")
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
