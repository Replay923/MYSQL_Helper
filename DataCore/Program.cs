using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DataCore.Commands;

namespace DataCore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			AppDb.Initialize();
			if (args.Length == 0)
			{
				BuildWebHost(args).Run();
			}
			else
			{
				Environment.Exit(CommandRunner.Run(args));
			}
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseUrls(AppDb.WebPort)
				.UseStartup<Startup>()
				.Build();
		}
	}
}
