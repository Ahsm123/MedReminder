using System.Data;

namespace MedReminder.Dal.Interfaces;

public interface IConnectionFactory
{
    IDbConnection CreateConnection();
}
