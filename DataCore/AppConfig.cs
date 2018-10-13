using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace DataCore
{
	public static class AppConfig
	{
		public static IConfigurationRoot Config => LazyConfig.Value;

		private static readonly Lazy<IConfigurationRoot> LazyConfig = new Lazy<IConfigurationRoot>(() => new ConfigurationBuilder()
			.SetBasePath(Environment.CurrentDirectory+ "/Config")
			.AddJsonFile("appsettings.json")
			.AddJsonFile("config.json")
			.Build());
	}
}
