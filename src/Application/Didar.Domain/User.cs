namespace Didar.Domain;

public class User(int id, int subscriptionLevel)
{
    public int Id { get; } = id;
    public int SubscriptionLevel { get; set; } = subscriptionLevel;
}
