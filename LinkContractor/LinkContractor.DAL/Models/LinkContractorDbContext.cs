using Microsoft.EntityFrameworkCore;

namespace LinkContractor.DAL.Models
{
    public class LinkContractorDbContext : DbContext
    {
        public LinkContractorDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<SavedData> SavedData { get; set; }
        public virtual DbSet<ShortCode> ShortCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedData>(entity =>
            {
                entity.HasIndex(e => e.Guid)
                    .HasName("SavedData_Guid_index")
                    .IsUnique();

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasComputedColumnSql("([Created]+[TimeLimit])");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<ShortCode>(entity =>
            {
                entity.Property(e => e.Code).HasColumnName("ShortCode");

                entity.HasKey(e => e.Code)
                    .HasName("ShortCodes_ShortCode_pk");

                entity.HasIndex(e => e.RelatedGuid)
                    .HasName("ShortCodes_RelatedGuid_index")
                    .IsUnique();

                entity.HasOne(d => d.CorrespondingSavedData)
                    .WithOne(p => p.ShortCode)
                    .HasPrincipalKey<SavedData>(p => p.Guid)
                    .HasForeignKey<ShortCode>(d => d.RelatedGuid)
                    .HasConstraintName("ShortCodes_RelatedGuid_fk");
            });
        }
    }
}