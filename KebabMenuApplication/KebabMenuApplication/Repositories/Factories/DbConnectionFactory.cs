using System.Data;
using KebabMenuApplication.Options;
using KebabMenuApplication.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace KebabMenuApplication.Repositories.Factories;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IOptions<ConnectionOptions> _options;

    public DbConnectionFactory(
        IOptions<ConnectionOptions> options) =>
        _options = options;

    public IDbConnection GetConnection()
    {
        return new SqlConnection(_options.Value.MenuDB);
    }
}