namespace Didar.Packaging.Domain;

public class Subscription(int userId, int level)
{
    public int UserId { get; } = userId;
    public int Level { get; set; } = level;
}