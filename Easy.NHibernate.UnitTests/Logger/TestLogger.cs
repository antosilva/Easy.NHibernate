using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Easy.NHibernate.UnitTests.Logger
{
    internal class TestLogger
    {
        public ILog Log { get; }

        public TestLogger(string fileName)
        {
            FileAppender appender = new FileAppender
                                    {
                                        AppendToFile = false,
                                        File = fileName,
                                        Layout = new PatternLayout("%date %-5level %message%newline")
                                    };
            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);

            Hierarchy hierarchy = (Hierarchy) LogManager.GetRepository();
            hierarchy.Root.AddAppender(appender);

            Log = LogManager.GetLogger(typeof(TestLogger));
        }
    }
}
