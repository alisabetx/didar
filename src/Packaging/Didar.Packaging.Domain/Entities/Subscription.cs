namespace Didar.Packaging.Domain.Entities;

public class Subscription
{
    public int UserId { get; }
    public int Level { get; set; }

    public Subscription(int userId, int level)
    {
        UserId = userId;
        Level = level;
    }
}
