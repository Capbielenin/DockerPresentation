using System.Data;

namespace KebabMenuApplication.Repositories.Factories;

public interface IDbConnectionFactory
{
    public IDbConnection GetConnection();
}