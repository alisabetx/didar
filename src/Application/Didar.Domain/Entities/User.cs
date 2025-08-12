namespace Didar.Domain.Entities;

public class User(int id, int subscriptionLevel)
{
    public int Id { get; } = id;
    public int SubscriptionLevel { get; set; } = subscriptionLevel;
}
