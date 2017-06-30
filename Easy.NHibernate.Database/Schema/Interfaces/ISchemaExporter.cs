using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Easy.NHibernate.Database.Schema.Interfaces
{
    public interface ISchemaExporter
    {
        void ExportToFile(string fileName);
        void ExportToConsole();
        string ExportToDatabase(ISession session);
    }
}
