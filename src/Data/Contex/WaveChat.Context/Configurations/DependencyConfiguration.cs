using Microsoft.EntityFrameworkCore;
using WaveChat.Context.Entities.Boards;

namespace WaveChat.Context.Configurations;

public static class DependencyConfiguration
{
    public static void ConfigureDependency(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dependency>(entity =>
        {
            entity.HasKey(e => e.Iddependency).HasName("dependencies_pkey");

            entity.ToTable("dependencies");

            entity.Property(e => e.Iddependency).HasColumnName("iddependency");
            entity.Property(e => e.Idboard).HasColumnName("idboard");
            entity.Property(e => e.Iduser).HasColumnName("iduser");

            entity.HasOne(d => d.IdboardNavigation).WithMany(p => p.Dependencies)
                .HasForeignKey(d => d.Idboard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dependencies_idboard_fkey");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Dependencies)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dependencies_iduser_fkey");
        });

    }
}
