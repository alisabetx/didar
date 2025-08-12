using Didar.Application.Repositories;

namespace Didar.Application.Services;

public class UserSubscriptionService
{
    private readonly IUserRepository _userRepository;
    private readonly IPackagingService _packagingService;

    public UserSubscriptionService(IUserRepository userRepository, IPackagingService packagingService)
    {
        _userRepository = userRepository;
        _packagingService = packagingService;
    }

    public async Task UpgradeSubscriptionAsync(int userId, int newLevel, bool failLocal = false)
    {
        var user = _userRepository.Get(userId) ?? new Domain.User(userId, 0);
        _userRepository.Upsert(user);
        var previousLevel = user.SubscriptionLevel;

        using var transaction = _userRepository.BeginTransaction(userId);
        bool packagingApplied = false;

        try
        {
            user.SubscriptionLevel = newLevel;
            _userRepository.Upsert(user);

            await _packagingService.UpgradeSubscriptionAsync(userId, newLevel);
            packagingApplied = true;

            if (failLocal)
            {
                throw new InvalidOperationException("Simulated local failure");
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            if (packagingApplied)
            {
                await _packagingService.RollbackSubscriptionAsync(userId, previousLevel);
            }
            throw;
        }
    }

    public Task<bool> CheckPackagingHealthAsync() => _packagingService.CheckHealthAsync();
}
