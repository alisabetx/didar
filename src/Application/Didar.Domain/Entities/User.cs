namespace Didar.Domain.Entities;

public class User
{
    public int Id { get; }
    public int SubscriptionLevel { get; set; }

    public User(int id, int subscriptionLevel)
    {
        Id = id;
        SubscriptionLevel = subscriptionLevel;
    }
}
