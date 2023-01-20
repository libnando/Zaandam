using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zaandam.Domain.Models;

namespace Zaandam.Infrastructure.Contexts.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable($"{nameof(Document)}s");
        
        builder.HasKey(doc => doc.Id);
        builder.Property(doc => doc.Key).HasColumnType("varchar").IsRequired().HasMaxLength(50);
        builder.Property(doc => doc.Data).HasColumnType("text").IsRequired();
        builder.Property(doc => doc.Position).IsRequired();
        builder.Property(doc => doc.CreationDate).IsRequired();        
        
        builder.HasIndex(doc => new { doc.Key, doc.Position }).IsUnique(true);
    }
}