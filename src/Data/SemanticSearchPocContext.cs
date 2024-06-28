using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VectorSemanticSearchPoc.Data.Models;

namespace VectorSemanticSearchPoc.Data
{
    public class SemanticSearchPocContext : DbContext
    {
        protected readonly ILogger<SemanticSearchPocContext> logger;
        public DbSet<Course> Courses { get; set; }

        public SemanticSearchPocContext(DbContextOptions<SemanticSearchPocContext> options, ILogger<SemanticSearchPocContext> logger)
            : base(options)
        {
            this.logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("vector");
            base.OnModelCreating(modelBuilder);
            this.OnModelCreatingPartial(modelBuilder);
        }

        public virtual void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

        }
    }
}
