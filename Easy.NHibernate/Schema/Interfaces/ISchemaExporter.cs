using NHibernate;

namespace Easy.NHibernate.Schema.Interfaces
{
    public interface ISchemaExporter
    {
        void ExportToFile(string fileName);
        void ExportToConsole();
        string ExportToDatabase();
        string ExportToString();
    }
}
