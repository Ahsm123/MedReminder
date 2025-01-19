using Dapper;
using MedReminder.Dal.Interfaces;
using MedReminder.Dal.Models;

namespace MedReminder.Dal.Dao;

public class SubscriptionDao : ISubscriptionDao
{
    private const string AddSubscriptionSql = @"
        INSERT INTO Subscription (UserId, PhoneNumber, SubscriptionType, SubscribedAt, IsActive)
        VALUES (@UserId, @PhoneNumber, @SubscriptionType, @SubscribedAt, @IsActive)";

    private readonly IConnectionFactory _connectionFactory;
    public SubscriptionDao(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public Task AddSubscriptionAsync(Subscription subscription)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.ExecuteAsync(AddSubscriptionSql, subscription);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to add subscription", e);
        }
    }
}
