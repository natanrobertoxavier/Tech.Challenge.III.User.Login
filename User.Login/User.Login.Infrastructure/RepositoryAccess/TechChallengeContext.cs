using Microsoft.EntityFrameworkCore;

namespace User.Login.Infrastructure.RepositoryAccess;
public class TechChallengeContext(DbContextOptions<TechChallengeContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechChallengeContext).Assembly);
    }
}