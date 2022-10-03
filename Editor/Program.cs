using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEmuTool.Forms;
using EVEmuTool.LogServer;
using EVESharp.Common.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Templates;

namespace EVEmuTool
{
    static class Program
    {
        public static ILogger logger = SetupLogger();
        private static ILogger SetupLogger()
        {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration().MinimumLevel.Verbose();

            // create a default expression template to ensure the text has the correct format
            ExpressionTemplate template = new ExpressionTemplate(
                "{UtcDateTime(@t):yyyy-MM-dd HH:mm:ss} {@l:u1} {Coalesce(Coalesce(Name, Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)), 'Program')}: {@m:lj}\n{@x}"
            );

            // setup channels to be ignored if they're hidden (this tools primary use is packet capturing, so having logs for everything is too intensive)
            loggerConfiguration.Filter.ByExcluding(logEvent =>
            {
                // check if it should be hidden by default
                if (logEvent.Properties.TryGetValue(LoggingExtensions.HIDDEN_PROPERTY_NAME, out LogEventPropertyValue value) == true)
                {
                    // now check if the name is in the allowed list
                    string name = "";

                    if (logEvent.Properties.TryGetValue("Name", out LogEventPropertyValue nameProp) == true)
                        name = nameProp.ToString();
                    else if (logEvent.Properties.TryGetValue("SourceContext", out LogEventPropertyValue sourceContext) == true)
                        name = sourceContext.ToString();

                    return true;
                }

                return false;
            });

            // log to console by default
            loggerConfiguration.WriteTo.Console(template);
            loggerConfiguration.WriteTo.File("output.log");

            Serilog.Log.Logger = loggerConfiguration.CreateLogger();

            return Serilog.Log.Logger;
        }
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WorkspaceForm());
        }
    }
}