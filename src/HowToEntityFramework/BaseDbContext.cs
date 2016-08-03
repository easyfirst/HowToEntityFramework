using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using Imperium.Framework.Data;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace HowToEntityFramework
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext() : base(App.ConnectionString)
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ConsoleTarget
            {
                Layout = @"${date:format=HH\:mm\:ss} ${message}"
            };

            config.AddTarget("console", consoleTarget);

            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));

            LogManager.Configuration = config;
            Logger logger = LogManager.GetLogger("App");

            DbInterception.Add(new NLogCommandInterceptor(logger));
        }
    }
}
