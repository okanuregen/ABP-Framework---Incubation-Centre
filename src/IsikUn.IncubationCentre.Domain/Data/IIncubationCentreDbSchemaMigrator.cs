using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Data;

public interface IIncubationCentreDbSchemaMigrator
{
    Task MigrateAsync();
}
