using Dapper;
using KebabMenuApplication.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace KebabMenuApplication.Extensions;

public static class SetUpExtension
{
    public static void RunSetUpScript(this WebApplication app)
    {
        var logger = app.Services.GetService<ILogger<Program>>();

        try
        {
        
            logger.LogInformation("Running Set Up script... ");
            var options = app.Services.GetService<IOptions<ConnectionOptions>>();
            
            CreateDatabase(options.Value.MasterDb, logger);
            CreateTables(options.Value.MenuDB, logger);

            logger.LogInformation("Finish running Set Up script... ");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Problem during running set up script!");
            throw;
        }


    }

    private static void CreateTables(string menuConnectionString, ILogger<Program> logger)
    {
        logger.LogInformation("Creating tables");

        using (var conn = new SqlConnection(menuConnectionString))
        {
            var query = File.ReadAllText("./SetUpScript/SetUpQuery.sql");    
            conn.Execute(query);
        }
            
        logger.LogInformation("Finish creating tables");

    }
    
    private static void CreateDatabase(string masterConnectionString, ILogger<Program> logger)
    {
        logger.LogInformation("Creating database");

        using (var conn = new SqlConnection(masterConnectionString))
        {
            var query = File.ReadAllText("./SetUpScript/SetUpDb.sql");    
            conn.Execute(query);

        }
        logger.LogInformation("Finish creating database");

    }
}