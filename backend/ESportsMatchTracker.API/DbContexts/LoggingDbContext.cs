using ESportsMatchTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ESportsMatchTracker.API.DbContexts;

public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options) { }

    public DbSet<ApiLog> ApiLogs => Set<ApiLog>();
}