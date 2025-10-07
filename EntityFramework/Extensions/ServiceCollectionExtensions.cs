namespace EntityFramework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<HypermediaDbContext>(options =>
            options.UseInMemoryDatabase("HypermediaDbContext"));

        return services;
    }
}
