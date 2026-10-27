using CommBank.Services;

namespace CommBank.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddSingleton<IAccountsService, AccountsService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IGoalsService, GoalsService>();
        services.AddSingleton<ITagsService, TagsService>();
        services.AddSingleton<ITransactionsService, TransactionsService>();
        services.AddSingleton<IUsersService, UsersService>();

        return services;
    }
}
