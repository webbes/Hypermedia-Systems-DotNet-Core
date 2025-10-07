namespace EntityFramework;

public class HypermediaDbContext(DbContextOptions<HypermediaDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();
}
